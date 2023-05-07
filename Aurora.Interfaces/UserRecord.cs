namespace Aurora.Interfaces;

[GenerateSerializer]
[Immutable]
public sealed record UserRecord
{
    [Id(0)] public string Id { get; set; } = string.Empty;

    [Id(1)] public string Name { get; set; } = string.Empty;

    [Id(2)] public string Email { get; set; } = string.Empty;

    [Id(3)] public string FirstName { get; set; } = string.Empty;

    [Id(4)] public string LastName { get; set; } = string.Empty;

    [Id(5)] public DateTime? LastLoginUtc { get; set; }
}