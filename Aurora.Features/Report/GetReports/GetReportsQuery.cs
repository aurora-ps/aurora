using MediatR;

namespace Aurora.Features.Report.GetReports;

public class GetReportsQuery : IRequest<GetReportsQueryResult>
{

    private GetReportsQuery(bool showHidden = false)
    {
        this.ShowHidden = showHidden;
    }

    private GetReportsQuery(string userId, bool showHidden = false)
    {
        this.UserId = userId;
        this.ShowHidden = showHidden;
    }

    public string? UserId { get; set; }

    public bool ShowHidden { get; set; }

    public static GetReportsQuery Create(string userId, bool showHidden)
    {
        return new GetReportsQuery(userId, showHidden);
    }
    public static GetReportsQuery Create(bool showHidden)
    {
        return new GetReportsQuery(showHidden);
    }
}