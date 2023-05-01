namespace Aurora.Interfaces;

[GenerateSerializer]
[Immutable]
public sealed record UserRecord
{
    [Id(0)] public string Id { get; set; } = string.Empty;

    [Id(1)] public string Name { get; set; } = string.Empty;

    [Id(2)] public string Email { get; set; } = string.Empty;
}