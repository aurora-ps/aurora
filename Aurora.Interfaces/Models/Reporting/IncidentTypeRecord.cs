namespace Aurora.Interfaces.Models.Reporting;

[GenerateSerializer]
[Immutable]
public sealed record IncidentTypeRecord : IIncidentTypeOverrides
{
    [Id(0)] public string Name { get; set; }
    [Id(1)] public string Id { get; set; }
    [Id(2)] public bool CollectTime { get; set; }
    [Id(3)] public bool RequiresTime { get; set; }
    [Id(4)] public bool CollectLocation { get; set; }
    [Id(5)] public bool CollectPerson { get; set; }
    [Id(6)] public bool ShowGospelPresentations { get; set; }
    [Id(7)] public bool ShowProfessionsOfFaith { get; set; }
    [Id(8)] public bool ShowBaptisms { get; set; }
    [Id(9)] public bool ShowBibleStudies { get; set; }
    [Id(10)] public bool ShowCounselingOpportunities { get; set; }
    
    public override string ToString() => Name;
}