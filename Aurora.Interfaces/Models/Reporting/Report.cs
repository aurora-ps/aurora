namespace Aurora.Interfaces.Models.Reporting;

public class Report : IReport
{
    public Report()
    {
        Location = new Location { LocationType = LocationTypeEnum.Incident };
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public DateTime? Date { get; set; } = DateTime.Now.Date;

    public TimeSpan? Time { get; set; }

    public Agency Agency { get; set; }

    public IncidentType IncidentType { get; set; }

    public double? Miles { get; set; }

    public Location Location { get; set; }

    public string Narrative { get; set; }

    public virtual ICollection<ReportPerson> People { get; set; } = new List<ReportPerson>();

    public string AuroraUserId { get; set; }

    public AuroraUser User { get; set; }

    public string AgencyId { get; set; }

    public string IncidentTypeId { get; set; }

    public DateTime CreatedOnUtc { get; set; } = DateTime.Now;

    public DateTime? DeletedOnUtc { get; set; }
}
