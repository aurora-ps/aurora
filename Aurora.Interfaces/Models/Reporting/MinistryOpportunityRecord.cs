namespace Aurora.Interfaces.Models.Reporting;

public interface IMinistryOpportunityRecord
{
    int? GospelPresentations { get; set; }
    int? ProfessionsOfFaith { get; set; }
    int? Baptisms { get; set; }
    int? BibleStudies { get; set; }
    int? CounselingOpportunities { get; set; }
    string ToString();
}

[GenerateSerializer]
[Immutable]
public record MinistryOpportunityRecord : IMinistryOpportunityRecord
{
    public int? GospelPresentations { get; set; }

    public int? ProfessionsOfFaith { get; set; }

    public int? Baptisms { get; set; }

    public int? BibleStudies { get; set; }

    public int? CounselingOpportunities { get; set; }
}