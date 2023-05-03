using MediatR;

namespace Aurora.Features.Report.GetReports;

public class GetReportsQuery : IRequest<GetReportsQueryResult>
{
    private GetReportsQuery(string userId, bool showHidden = false, bool showAll = false)
    {
        this.UserId = userId;
        this.ShowHidden = showHidden;
        this.ShowAll = showAll;
    }

    public string? UserId { get; }

    public bool ShowHidden { get; }

    public bool ShowAll { get; }

    public static GetReportsQuery Create(string userId, bool showHidden, bool showAll)
    {
        return new GetReportsQuery(userId, showHidden, showAll);
    }
}