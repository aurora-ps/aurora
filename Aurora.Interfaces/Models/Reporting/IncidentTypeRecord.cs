namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public sealed record IncidentTypeRecord
{
    [Id(0)] public string Name { get; set; }

    [Id(1)] public string Id { get; set; }
    [Id(2)] public bool CollectTime { get; set; } = false;
    [Id(3)] public bool RequiresTime { get; set; } = false;
    [Id(4)] public bool CollectLocation { get; set; } = false;
    [Id(5)] public bool CollectPerson { get; set; } = false;

    public override string ToString() => Name;
}