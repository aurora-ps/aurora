using Aurora.Interfaces.Models.Reporting;
using MediatR;

namespace Aurora.Features.Report.SaveReport;

public class SaveReportCommand : IRequest<SaveReportCommandResult>, IReportRecord
{
    public string Id { get; init; }
    public DateTime? Date { get; init; }
    public TimeSpan? Time { get; init; }
    public AgencyRecord Agency { get; init; }
    public IncidentTypeRecord IncidentType { get; init; }
    public double? Miles { get; init; }
    public LocationRecord Location { get; init; }
    public string Narrative { get; init; }
    public IList<ReportPersonRecord> People { get; init; }
    public string UserId { get; set; }
}