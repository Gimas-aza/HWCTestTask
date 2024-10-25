namespace Assets.Client
{
    public class GameServerAdapter : IGameServerAdapter
    {
        private IServerComponent _serverComponent;

        public GameServerAdapter(IServerComponent serverComponent)
        {
            _serverComponent = serverComponent;
        }

        public GameState GetGameState()
        {
            return _serverComponent.GetGameState();
        }

        public void StartGame()
        {
            _serverComponent.StartGame();
        }
    }
}