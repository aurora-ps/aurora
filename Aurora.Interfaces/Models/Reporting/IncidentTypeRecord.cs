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
    [Id(6)] public bool ShowGospelPresentations { get; set; } = false;
    [Id(7)] public bool ShowProfessionsOfFaith { get; set; } = false;
    [Id(8)] public bool ShowBaptisms { get; set; } = false;
    [Id(9)] public bool ShowBibleStudies { get; set; } = false;
    [Id(10)] public bool ShowCounselingOpportunities { get; set; } = false;
    
    public override string ToString() => Name;
}