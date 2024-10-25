namespace Assets.Client
{
    public interface IServerComponent
    {
        void StartGame();
        GameState GetGameState();
    }
}