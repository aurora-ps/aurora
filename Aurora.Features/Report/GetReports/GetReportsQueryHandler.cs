using Aurora.Infrastructure.Data;
using Aurora.Interfaces.Models.Reporting;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.Report.GetReports;

public class GetReportsQueryHandler : IRequestHandler<GetReportsQuery, GetReportsQueryResult>
{
    private readonly IReportDbContext _context;
    private readonly IMapper _mapper;

    public GetReportsQueryHandler(IReportDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetReportsQueryResult> Handle(GetReportsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Reports
            .Where(_ => request.ShowHidden || _.DeletedOnUtc == null);

        if (!string.IsNullOrEmpty(request.UserId) && !request.ShowAll)
            query = query.Where(_ => _.ReportUserId.Equals(request.UserId));

        var reports = await query.ProjectTo<ReportSummaryRecord>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return GetReportsQueryResult.CreateSuccess(reports);
    }
}