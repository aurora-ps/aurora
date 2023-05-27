using MediatR;

namespace Aurora.Features.Agency.AddAgency;

public class AddAgencyCommand : IRequest
{
    private AddAgencyCommand(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; }

    public string Name { get; }

    public static AddAgencyCommand Create(string id, string name)
    {
        return new AddAgencyCommand(id, name);
    }
}