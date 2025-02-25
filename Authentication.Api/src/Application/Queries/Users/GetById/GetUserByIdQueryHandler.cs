using Application.Abstractions.Authorization;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Application.Repositories;
using Domain.Authorization;
using Domain.Identifiers;
using Domain.UserPhotos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Results;

namespace Application.Queries.Users.GetById;

internal sealed class GetUserByIdQueryHandler(
    IApplicationDbContext context,
    ICacheService cacheService,
    IAuthorizationHandler authorizationHandler,
    IUserPhotoRepository userPhotoRepository
) : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private async Task<Result<UserResponse>> GetUserByIdAsync(
        GetUserByIdQuery query,
        CancellationToken cancellationToken
    )
    {
        User? user = await context
            .Users.AsNoTracking()
            .AsSplitQuery()
            .Where(u => u.Id == query.UserId)
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId));
        }

        UserPhoto? userPhoto = await userPhotoRepository.GetByUserIdAsync(
            user.Id,
            cancellationToken
        );

        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Photo = userPhoto?.Photo,
            PhotoPath = userPhoto?.PhotoPath,
            Roles = user.Roles.Select(r => r.Name).ToList(),
            Permissions = user.Roles.SelectMany(r => r.Permissions).Select(p => p.Name).ToHashSet(),
        };
    }

    public async Task<Result<UserResponse>> Handle(
        GetUserByIdQuery query,
        CancellationToken cancellationToken
    )
    {
        Result authorizationResult = await authorizationHandler.HandleAsync(
            query.UserId,
            new PermissionAccess(Resources.Users, CrudOperations.Read),
            cancellationToken
        );

        if (authorizationResult.IsFailure)
        {
            return Result.Failure<UserResponse>(authorizationResult.Error);
        }

        return await cacheService.GetOrCreateAsync(
            $"users:{query.UserId}",
            async ct => await GetUserByIdAsync(query, ct),
            [Tags.Users],
            cancellationToken
        );
    }
}
