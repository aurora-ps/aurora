namespace Aurora.Interfaces;

[GenerateSerializer]
public class ServerState
{
    [Id(0)] public bool IsInitialized { get; set; }
}