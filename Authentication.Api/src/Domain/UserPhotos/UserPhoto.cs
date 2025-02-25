using SharedKernel.Domain;

namespace Domain.UserPhotos;

public sealed class UserPhoto : Entity
{
    public required string? Photo { get; init; }

    public required string? PhotoPath { get; init; }

    public required Guid UserId { get; init; }
}
