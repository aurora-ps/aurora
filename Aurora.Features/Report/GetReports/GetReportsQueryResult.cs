using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Features.Report.GetReports;

public class GetReportsQueryResult
{
    private GetReportsQueryResult(bool success)
    {
        this.Success = success;
    }

    public bool Success { get; set; }

    public static GetReportsQueryResult CreateSuccess(IList<ReportRecord> reports)
    {
        return new GetReportsQueryResult(true)
        {
            Reports = reports
        };
    }

    public IList<ReportRecord> Reports { get; set; }
}