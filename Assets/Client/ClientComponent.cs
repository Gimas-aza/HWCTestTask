using UnityEngine;

namespace Assets.Client
{
    public class ClientComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _server;

        private IServerComponent _serverComponent => _server.GetComponent<IServerComponent>();
        private IGameServerAdapter _serverAdapter;

        private void Start()
        {
            DontDestroyOnLoad(this);
            
            _serverAdapter = new GameServerAdapter(_serverComponent);
            
            if (_serverAdapter.GetGameState() == GameState.OK)
                _serverAdapter.StartGame();
        }
    }
}
