using Application.Abstractions.Authentication;
using Domain.Joins;
using Domain.Roles;
using Domain.Users;
using Infrastructure.Seed.Abstractions;
using Infrastructure.Seed.Core.Ids;
using SharedKernel.Time;

namespace Infrastructure.Seed.Core.Data;

internal sealed class UsersData(IPasswordHasher passwordHasher, IDateTimeProvider timeProvider)
    : SeedEntity<User>(DbPriority.Three)
{
    protected override IEnumerable<User> GetData()
    {
        return
        [
            new User
            {
                Id = UsersId.Admin,
                Email = "admin@admin.com",
                FirstName = "admin",
                LastName = "admin",
                PasswordHash = passwordHasher.Hash("admin"),
                UserRoles =
                [
                    new UserRole { RoleId = Role.AdminId, CreatedOnUtc = timeProvider.UtcNow },
                    new UserRole { RoleId = Role.UserId, CreatedOnUtc = timeProvider.UtcNow },
                ],
                CreatedOnUtc = timeProvider.UtcNow,
            },
            new User
            {
                Id = UsersId.One,
                Email = "user1@user.com",
                FirstName = "user",
                LastName = "user",
                PasswordHash = passwordHasher.Hash("user"),
                UserRoles =
                [
                    new UserRole { RoleId = Role.UserId, CreatedOnUtc = timeProvider.UtcNow },
                ],
                CreatedOnUtc = timeProvider.UtcNow,
            },
            new User
            {
                Id = UsersId.Two,
                Email = "user2@user.com",
                FirstName = "user",
                LastName = "user",
                PasswordHash = passwordHasher.Hash("user"),
                UserRoles =
                [
                    new UserRole { RoleId = Role.UserId, CreatedOnUtc = timeProvider.UtcNow },
                ],
                CreatedOnUtc = timeProvider.UtcNow,
            },
            new User
            {
                Id = UsersId.Three,
                Email = "user3@user.com",
                FirstName = "user",
                LastName = "user",
                PasswordHash = passwordHasher.Hash("user"),
                UserRoles =
                [
                    new UserRole { RoleId = Role.UserId, CreatedOnUtc = timeProvider.UtcNow },
                ],
                CreatedOnUtc = timeProvider.UtcNow,
            },
            new User
            {
                Id = UsersId.Four,
                Email = "user4@user.com",
                FirstName = "user",
                LastName = "user",
                PasswordHash = passwordHasher.Hash("user"),
                UserRoles =
                [
                    new UserRole { RoleId = Role.UserId, CreatedOnUtc = timeProvider.UtcNow },
                ],
                CreatedOnUtc = timeProvider.UtcNow,
            },
            new User
            {
                Id = UsersId.Five,
                Email = "user5@user.com",
                FirstName = "user",
                LastName = "user",
                PasswordHash = passwordHasher.Hash("user"),
                UserRoles =
                [
                    new UserRole { RoleId = Role.UserId, CreatedOnUtc = timeProvider.UtcNow },
                ],
                CreatedOnUtc = timeProvider.UtcNow,
            },
            new User
            {
                Id = UsersId.Six,
                Email = "user6@user.com",
                FirstName = "user",
                LastName = "user",
                PasswordHash = passwordHasher.Hash("user"),
                UserRoles =
                [
                    new UserRole { RoleId = Role.UserId, CreatedOnUtc = timeProvider.UtcNow },
                ],
                CreatedOnUtc = timeProvider.UtcNow,
            },
            new User
            {
                Id = UsersId.Seven,
                Email = "user7@user.com",
                FirstName = "user",
                LastName = "user",
                PasswordHash = passwordHasher.Hash("user"),
                UserRoles =
                [
                    new UserRole { RoleId = Role.UserId, CreatedOnUtc = timeProvider.UtcNow },
                ],
                CreatedOnUtc = timeProvider.UtcNow,
            },
            new User
            {
                Id = UsersId.Eight,
                Email = "user8@user.com",
                FirstName = "user",
                LastName = "user",
                PasswordHash = passwordHasher.Hash("user"),
                UserRoles =
                [
                    new UserRole { RoleId = Role.UserId, CreatedOnUtc = timeProvider.UtcNow },
                ],
                CreatedOnUtc = timeProvider.UtcNow,
            },
            new User
            {
                Id = UsersId.Nine,
                Email = "user9@user.com",
                FirstName = "user",
                LastName = "user",
                PasswordHash = passwordHasher.Hash("user"),
                UserRoles =
                [
                    new UserRole { RoleId = Role.UserId, CreatedOnUtc = timeProvider.UtcNow },
                ],
                CreatedOnUtc = timeProvider.UtcNow,
            },
            new User
            {
                Id = UsersId.Ten,
                Email = "user10@user.com",
                FirstName = "user",
                LastName = "user",
                PasswordHash = passwordHasher.Hash("user"),
                UserRoles =
                [
                    new UserRole { RoleId = Role.UserId, CreatedOnUtc = timeProvider.UtcNow },
                ],
                CreatedOnUtc = timeProvider.UtcNow,
            },
            new User
            {
                Id = UsersId.Eleven,
                Email = "user11@user.com",
                FirstName = "user",
                LastName = "user",
                PasswordHash = passwordHasher.Hash("user"),
                UserRoles =
                [
                    new UserRole { RoleId = Role.UserId, CreatedOnUtc = timeProvider.UtcNow },
                ],
                CreatedOnUtc = timeProvider.UtcNow,
            },
        ];
    }
}
