namespace Aurora.Interfaces;

[GenerateSerializer]
[Immutable]
public abstract record OrganizationRecordBase
{
    [Id(0)] public string Id { get; set; } = null!;
    [Id(1)] public string Name { get; set; } = null!;
}