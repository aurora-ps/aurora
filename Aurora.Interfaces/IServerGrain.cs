namespace Aurora.Interfaces;

public interface IServerGrain : IGrainWithStringKey
{
    Task<bool> IsInitialized();

    Task<ServerState> GetState();
}