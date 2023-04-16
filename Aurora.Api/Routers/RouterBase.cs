namespace Aurora.Api.Routers;

public abstract class RouterBase
{
    public const string UrlFragment = "";

    protected RouterBase(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; set; }
}