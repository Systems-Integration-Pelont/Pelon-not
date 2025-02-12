using Application.Repositories;
using Domain.UserPhotos;
using Microsoft.Extensions.Logging;
using SharedKernel.Time;

namespace Infrastructure.Repositories;

internal sealed class UserPhotoRepository(
    IDateTimeProvider timeProvider,
    ILogger<UserPhotoRepository> logger
) : IUserPhotoRepository
{
    private const string SharedDirectory = "/app_data";

    private static async Task<string?> ImageToBase64(string imagePath)
    {
        try
        {
            byte[] imageArray = await File.ReadAllBytesAsync(imagePath);

            return Convert.ToBase64String(imageArray);
        }
        catch
        {
            return null;
        }
    }

    public async Task<UserPhoto?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        string facesDirectory = $"{SharedDirectory}/faces";

        string imagePath = $"{facesDirectory}/{userId}.png";

        string? photo = await ImageToBase64(imagePath);

        logger.LogInformation(
            "User photo for user with ID {UserId} was retrieved from {ImagePath}",
            userId,
            imagePath
        );

        if (photo is null)
        {
            return null;
        }

        return new UserPhoto
        {
            Photo = photo,
            PhotoPath = imagePath,
            UserId = userId,
            CreatedOnUtc = timeProvider.UtcNow,
        };
    }
}
