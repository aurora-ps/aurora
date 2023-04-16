namespace Aurora.Interfaces;

[GenerateSerializer, Immutable]
public sealed record ServerState
{
    [Id(0)] public bool IsInitialized { get; set; }
    [Id(1)] public List<OrganizationRecordBase> Organizations { get; set; } = new();
}