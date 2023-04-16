namespace Aurora.Interfaces;

[GenerateSerializer]
[Immutable]
public sealed record UserRecord
{
    [Id(0)] public string Id { get; init; } = string.Empty;

    [Id(1)] public string Name { get; init; } = string.Empty;

    [Id(2)] public string Email { get; init; } = string.Empty;
}