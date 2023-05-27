using MediatR;

namespace Aurora.Features.Report.GetReport;

public class GetReportQuery : IRequest<GetReportQueryResult>
{
    public GetReportQuery(string reportId)
    {
        this.ReportId = reportId;
    }

    public string ReportId { get; set; }
}