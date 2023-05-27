using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.User.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<GetUserQuery> _validator;

    public GetUserQueryHandler(IValidator<GetUserQuery> validator, IApplicationDbContext context, IMapper mapper)
    {
        _validator = validator;
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return GetUserResponse.CreateFailure(validationResult.Errors);

        var user = await _context.Users.ProjectTo<UserRecord>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(_ => _.Id.Equals(request.UserId), cancellationToken);

        if (user is null)
            return new GetUserResponse { Success = false };

        return GetUserResponse.CreateSuccess(user);
    }
}