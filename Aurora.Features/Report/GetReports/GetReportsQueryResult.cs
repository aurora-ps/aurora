﻿using Aurora.Interfaces.Models.Reporting;

namespace Aurora.Features.Report.GetReports;

public class GetReportsQueryResult
{
    private GetReportsQueryResult(bool success)
    {
        Success = success;
    }

    public bool Success { get; set; }

    public IList<ReportSummaryRecord> Reports { get; set; }

    public static GetReportsQueryResult CreateSuccess(IList<ReportSummaryRecord> reports)
    {
        return new GetReportsQueryResult(true)
        {
            Reports = reports
        };
    }
}