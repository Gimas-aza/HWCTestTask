using Assets.Entity;
using Assets.EntryPoint;
using UnityEngine;

namespace Assets.GameSystems
{
    public class GameManager : IInitializer
    {
        private EntityComponent _entityPlayer;
        private EntityComponent _entityEnemy;
        private ILevelManager _levelManager;

        public void Init(IResolver container)
        {
            _entityPlayer = container.Resolve<EntityComponent>(nameof(EntityType.Player));
            _entityEnemy = container.Resolve<EntityComponent>(nameof(EntityType.Enemy));
            _levelManager = container.Resolve<ILevelManager>();

            _entityPlayer.CanAction += (isActive) => SetActive(EntityType.Enemy);
            _entityEnemy.CanAction += (isActive) => SetActive(EntityType.Player);
            _entityPlayer.OnDead += () => StopGama();
            _entityEnemy.OnDead += () => StopGama();

            SetActive(EntityType.Player);
        }

        private void SetActive(EntityType type)
        {
            if (type == EntityType.Player)
            {
                _entityPlayer.IsCanAction = true;
                _entityPlayer.UpdateEveryTurn();
            }
            else
            {
                _entityEnemy.IsCanAction = true;
                _entityEnemy.UpdateEveryTurn();
            }
        }

        private void StopGama()
        {
            Debug.Log("Game Over");
            _levelManager.ReloadingCurrentLevel();
        }
    }
}