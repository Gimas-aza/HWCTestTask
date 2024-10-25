namespace Assets.Client
{
    public interface IGameServerAdapter
    {
        void StartGame();
        GameState GetGameState();
    }
}