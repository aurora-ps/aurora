using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Features.Report.GetReport;

public class GetReportQueryResult
{
    public GetReportQueryResult(bool success, ReportRecord? record)
    {
        Success = success;
        Report = record;
    }

    public ReportRecord? Report { get; set; }

    public bool Success { get; set; }

    public static GetReportQueryResult CreateSuccess(ReportRecord report)
    {
        return new GetReportQueryResult(true, report);
    }

    public static GetReportQueryResult CreateNotFound()
    {
        return new GetReportQueryResult(false, null);
    }
}