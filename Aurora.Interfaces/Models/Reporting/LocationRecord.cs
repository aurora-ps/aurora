namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public record LocationRecord
{
    [Id(0)] public string Address { get; set; } = string.Empty;
    [Id(1)] public string City { get; set; } = string.Empty;
    [Id(2)] public string State { get; set; } = string.Empty;
    [Id(3)] public string Zip { get; set; } = string.Empty;
    [Id(4)] public LocationTypeEnum LocationType { get; set; } = LocationTypeEnum.Default;
}