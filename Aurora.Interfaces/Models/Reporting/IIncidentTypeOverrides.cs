namespace Aurora.Interfaces.Models.Reporting;

public interface IIncidentTypeOverrides
{
    bool CollectTime { get; set; }

    bool RequiresTime { get; set; }

    bool CollectLocation { get; set; }

    bool CollectPerson { get; set; }

    bool ShowGospelPresentations { get; set; }
    
    bool ShowProfessionsOfFaith { get; set; }
    
    bool ShowBaptisms { get; set; }
    
    bool ShowBibleStudies { get; set; }
    
    bool ShowCounselingOpportunities { get; set; }
}