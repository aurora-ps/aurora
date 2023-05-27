using Aurora.Features.Report.GetReport;
using Aurora.Infrastructure.Data;
using Aurora.Interfaces.Mapping;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Aurora.FeaturesTests
{
    namespace Aurora.Features.Report.Tests
    {
        public class GetReportQueryHandlerTests
        {
            private readonly IRequestHandler<GetReportQuery, GetReportQueryResult> _getReportQueryHandler;
            private readonly IMapper _mapper;
            private readonly IReportDbContext _mockReportDbContext;

            public GetReportQueryHandlerTests()
            {
                _mockReportDbContext = new Mock<IReportDbContext>().Object;
                var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
                _mapper = new Mapper(config);

                _getReportQueryHandler = new GetReportQueryHandler(_mockReportDbContext, _mapper);
            }

            [Fact]
            public async Task Handle_ReturnsSuccess_WhenReportExists()
            {
                // Arrange
                var reportId = Guid.NewGuid().ToString();


                var report = new Interfaces.Models.Reporting.Report
                {
                    Id = reportId,
                    // Initialize all properties
                };

                var optionsBuilder = new DbContextOptionsBuilder<ReportDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
                using (var context = new ReportDbContext(optionsBuilder))
                {
                    await context.Reports.AddAsync(report);
                    await context.SaveChangesAsync();
                }

                var mockDbContext = new ReportDbContext(optionsBuilder);

                var query = new GetReportQuery(reportId);
                var cancellationToken = new CancellationToken();

                var handler = new GetReportQueryHandler(mockDbContext, _mapper);

                // Act
                var result = await handler.Handle(query, cancellationToken);

                // Assert
                //result.Should().BeEquivalentTo(GetReportQueryResult.CreateSuccess(reportRecord));
            }

            //[Fact]
            //public async Task Handle_ReturnsNotFound_WhenReportDoesNotExist()
            //{
            //    // Arrange
            //    var reportId = Guid.NewGuid();

            //    var query = new GetReportQuery(reportId);
            //    var cancellationToken = new CancellationToken();

            //    var mockDbSet = new Mock<DbSet<ReportRecord>>();
            //    mockDbSet.Setup(x => x.FindAsync(reportId)).ReturnsAsync(null as ReportRecord);

            //    var mockDbContext = new Mock<IReportDbContext>();
            //    mockDbContext.Setup(x => x.Reports).Returns(mockDbSet.Object);

            //    var mockMapper = new Mock<IMapper>();

            //    var handler = new GetReportQueryHandler(mockDbContext.Object, mockMapper.Object);

            //    // Act
            //    var result = await handler.Handle(query, cancellationToken);

            //    // Assert
            //    result.Should().BeEquivalentTo(GetReportQueryResult.CreateNotFound());
            //}
        }
    }
}