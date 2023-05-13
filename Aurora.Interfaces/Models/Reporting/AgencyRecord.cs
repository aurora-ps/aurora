namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public sealed record AgencyRecord
{
    [Id(0)]public string Id { get; set; }
    [Id(1)]public string Name { get; set; }
    [Id(2)]public IList<IncidentTypeRecord> IncidentTypes { get; set; }
    [Id(3)]public DateTime? DeletedOnUtc { get; set; }

    public override string ToString() => Name;
}