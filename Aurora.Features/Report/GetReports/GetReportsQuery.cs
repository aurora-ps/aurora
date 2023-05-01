using MediatR;

namespace Aurora.Features.Report.GetReports;

public class GetReportsQuery : IRequest<GetReportsQueryResult>
{
    public GetReportsQuery(bool showHidden = false)
    {
        this.ShowHidden = showHidden;
    }

    public bool ShowHidden { get; set; }
}