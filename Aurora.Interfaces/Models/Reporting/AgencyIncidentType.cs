namespace Aurora.Interfaces.Models.Reporting;

public class AgencyIncidentType
{
    public string AgencyId { get; set; }

    public string IncidentTypeId { get; set; }

    public Agency Agency { get; set; }

    public IncidentType IncidentType { get; set; }
    

    #region Report Option Overrides

    public bool? CollectTime { get; set; }

    public bool? RequiresTime { get; set; }

    public bool? CollectLocation { get; set; }

    public bool? CollectPerson { get; set; }

    public bool? ShowGospelPresentations { get; set; }
    
    public bool? ShowProfessionsOfFaith { get; set; }
    
    public bool? ShowBaptisms { get; set; }
    
    public bool? ShowBibleStudies { get; set; }
    
    public bool? ShowCounselingOpportunities { get; set; }


    #endregion
}