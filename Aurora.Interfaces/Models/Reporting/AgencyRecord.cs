namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public sealed record AgencyRecord
{
    [Id(0)]public string Id { get; set; }
    public string Name { get; set; }
    public IList<IncidentTypeRecord> IncidentTypes { get; set; }
    public DateTime? DeletedOnUtc { get; set; }

    public override string ToString() => Name;
}