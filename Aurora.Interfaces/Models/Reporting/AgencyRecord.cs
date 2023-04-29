namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public sealed record AgencyRecord
{
    [Id(0)]public string Id { get; init; }
    [Id(1)]public string Name { get; init; }
}