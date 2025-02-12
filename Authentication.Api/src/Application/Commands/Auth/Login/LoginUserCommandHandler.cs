using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Tokens;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Results;
using SharedKernel.Time;

namespace Application.Commands.Auth.Login;

internal sealed class LoginUserCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider,
    IDateTimeProvider timeProvider
) : ICommandHandler<LoginUserCommand, LoginUserResponse>
{
    public async Task<Result<LoginUserResponse>> Handle(
        LoginUserCommand command,
        CancellationToken cancellationToken
    )
    {
        User? user = await context
            .Users.AsNoTracking()
            .AsSplitQuery()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LoginUserResponse>(UserErrors.InvalidCredentials);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<LoginUserResponse>(UserErrors.InvalidCredentials);
        }

        string accessToken = tokenProvider.Create(user);

        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = tokenProvider.GenerateRefreshToken(),
            ExpiredOnUtc = timeProvider.UtcNow.AddDays(7),
            CreatedOnUtc = timeProvider.UtcNow,
        };

        context.RefreshTokens.Add(refreshToken);

        await context.SaveChangesAsync(cancellationToken);

        var response = new LoginUserResponse
        {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
        };

        return response;
    }
}
