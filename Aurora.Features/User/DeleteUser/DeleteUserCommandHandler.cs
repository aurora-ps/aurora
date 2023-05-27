using Aurora.Infrastructure.Data;
using Aurora.Interfaces;
using Aurora.Interfaces.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Aurora.Features.User.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly UserManager<AuroraUser> _userManager;

    public DeleteUserCommandHandler(UserManager<AuroraUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId);
        if (user == null) return DeleteUserResponse.CreateNotFound();

        var result = await _userManager.DeleteAsync(user);
        return DeleteUserResponse.CreateSuccess();
    }
}