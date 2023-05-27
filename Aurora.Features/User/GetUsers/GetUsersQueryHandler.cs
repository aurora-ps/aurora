using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Features.User.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetUsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _context.Users.ProjectTo<UserRecord>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return GetUsersResponse.CreateSuccess(users);
    }
}