using Domain.Joins;
using Domain.Roles;
using Domain.UserPhotos;
using SharedKernel.Domain;

namespace Domain.Users;

public sealed class User : Entity
{
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string PasswordHash { get; init; }
    public UserPhoto? UserPhoto { get; init; }
    public List<Role> Roles { get; init; } = [];
    public List<UserRole> UserRoles { get; init; } = [];
}
