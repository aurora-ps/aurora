using MediatR;

namespace Aurora.Features.Agency.AddAgency;

public class AddAgencyCommand : IRequest<AddAgencyCommandResult>
{
    private AddAgencyCommand(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public static AddAgencyCommand Create(string name)
    {
        return new AddAgencyCommand(name);
    }
}