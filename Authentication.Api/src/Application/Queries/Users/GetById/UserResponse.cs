namespace Application.Queries.Users.GetById;

public sealed record UserResponse
{
    public required Guid Id { get; init; }

    public required string Email { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string? Photo { get; init; }

    public required string? PhotoPath { get; init; }

    public required List<string> Roles { get; init; }

    public required HashSet<string> Permissions { get; init; }
}
