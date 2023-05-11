namespace Aurora.Features.Agency.RemoveIncidentType;

public class RemoveAgencyIncidentTypeResult
{
    private RemoveAgencyIncidentTypeResult(bool found)
    {
        Found = found;
    }

    public bool Found{ get; private set; }

    public static RemoveAgencyIncidentTypeResult NotFound()
    {
        return new RemoveAgencyIncidentTypeResult(false);
    }

    public static RemoveAgencyIncidentTypeResult Success()
    {
        return new RemoveAgencyIncidentTypeResult(true);
    }
}