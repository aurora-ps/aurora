namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public record LocationRecord
{
    [Id(0)] public string Address { get; init; } = string.Empty;
    [Id(1)] public string City { get; init; } = string.Empty;
    [Id(2)] public string State { get; init; } = string.Empty;
    [Id(3)] public string Zip { get; init; } = string.Empty;
    [Id(4)] public LocationTypeEnum LocationType { get; init; } = LocationTypeEnum.Default;
}