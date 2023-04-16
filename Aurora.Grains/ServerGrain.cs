using Aurora.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;

namespace Aurora.Grains
{
    public class ServerGrain : Grain, IServerGrain
    {
        private readonly IPersistentState<ServerState> _state;
        private readonly ILogger<ServerGrain> _logger;

        public ServerGrain([PersistentState("server", "chaplainStorage")]
            IPersistentState<ServerState> state,
            ILoggerFactory factory)
        {
            this._state = state;
            this._logger = factory.CreateLogger<ServerGrain>();
        }

        public Task<bool> IsInitialized() => Task.FromResult(_state.State.IsInitialized);

        public Task<ServerState> GetState() => Task.FromResult(_state.State);
    }
}