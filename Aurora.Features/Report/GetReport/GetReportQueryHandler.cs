using Aurora.Grains;
using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
using Aurora.Interfaces.Models.Reporting;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.Report.GetReport
{
    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, GetReportQueryResult>
    {
        private readonly IReportDbContext _context;
        private readonly IMapper _mapper;

        public GetReportQueryHandler(IReportDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetReportQueryResult> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            var report = await _context.Reports.ProjectTo<ReportRecord>(_mapper.ConfigurationProvider)
                .Where(_ => _.Id.Equals(request.ReportId) && _.DeletedOnUtc == null)
                .FirstOrDefaultAsync(cancellationToken);

            if (report == null)
            {
                return GetReportQueryResult.CreateNotFound();
            }
            return GetReportQueryResult.CreateSuccess(report);
        }
    }
}
