namespace Aurora.Interfaces;

[GenerateSerializer, Immutable]
public sealed record UserServiceState
{
    [Id(0)] public Dictionary<string, UserRecord> Users { get; set; } = new();
}