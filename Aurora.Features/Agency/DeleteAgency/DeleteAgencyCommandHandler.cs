using Aurora.Infrastructure.Data;
using FluentValidation.Results;
using MediatR;

namespace Aurora.Features.Agency.DeleteAgency;

public class DeleteAgencyCommandHandler : IRequestHandler<DeleteAgencyCommand, DeleteAgencyResponse>
{
    private readonly IReportDbContext _context;

    public DeleteAgencyCommandHandler(IReportDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteAgencyResponse> Handle(DeleteAgencyCommand request, CancellationToken cancellationToken)
    {
        // validate command
        var validator = new DeleteAgencyValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return DeleteAgencyResponse.ValidationFailure(validationResult.Errors);

        // get agency
        var agency = _context.Agencies.FirstOrDefault(_ => _.Id.Equals(request.AgencyId));
        if (agency == null)
            return DeleteAgencyResponse.ValidationFailure(new List<ValidationFailure>
            {
                new(nameof(request.AgencyId), $"Agency with id {request.AgencyId} not found")
            });

        if (agency.DeletedOnUtc.HasValue)
            return DeleteAgencyResponse.ValidationFailure(new List<ValidationFailure>
            {
                new(nameof(request.AgencyId), $"Agency with id {request.AgencyId} is already deleted")
            });

        // delete agency
        agency.DeletedOnUtc = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return DeleteAgencyResponse.Deleted();
    }
}