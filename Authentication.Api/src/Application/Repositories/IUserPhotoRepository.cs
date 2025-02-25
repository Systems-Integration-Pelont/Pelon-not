using Domain.UserPhotos;

namespace Application.Repositories;

public interface IUserPhotoRepository
{
    Task<UserPhoto?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
