using Assets.DI;
using Assets.LevelManager;
using Assets.ObjectCreation;
using Assets.Entity;
using UnityEngine;
using System.Collections.Generic;
using Assets.MVP;
using Assets.GameSystems;
using Assets.Client;

namespace Assets.EntryPoint
{
    public class ServerComponent : MonoBehaviour, IServerComponent
    {
        private DIContainer _container;
        private IObject _creationObjects;
        private ILevelManager _levelManager;
        private IInitializer _gameManager;

        public void StartGame()
        {
            DontDestroyOnLoad(this);

            InitializeDependencies();
            LoadInitialLevel();
        }

        public GameState GetGameState()
        {
            return GameState.OK;
        }

        private void InitializeDependencies()
        {
            _container = new DIContainer();
            _levelManager = new GameSceneManager();
            _creationObjects = new ObjectFactory(new ResourcesProvider());
            _gameManager = new GameManager();

            _container.RegisterInstance(_levelManager);
            _levelManager.LevelLoaded += LevelLoaded;
        }

        private void LoadInitialLevel()
        {
            _levelManager.LoadLevel(0);
        }

        private void LevelLoaded(int sceneIndex)
        {
            var container = new DIContainer(_container);
            var models = new List<IModel>();
            var presenter = new Presenter();
            var view = _creationObjects.CreateObject<View>() as IInitializer;

            RegisterDependencies(container, presenter);
            RegisterEntities(container, models);

            presenter.Init(container);
            view.Init(container);
            _gameManager.Init(container);
        }

        private void RegisterEntities(DIContainer container, List<IModel> models)
        {
            models.Add(_levelManager as IModel);
            models.Add(container.Resolve<EntityComponent>(nameof(EntityType.Player)));
            models.Add(container.Resolve<EntityComponent>(nameof(EntityType.Enemy)));

            container.RegisterInstance(models);
        }

        private EntityComponent CreateEntity(DIContainer container, EntityType entityType)
        {
            var entity = _creationObjects.CreateObject<EntityComponent>(entityType.ToString());
            entity.EntityType = entityType;
            entity.Init(container);
            return entity;
        }

        private void RegisterDependencies(DIContainer container, Presenter presenter)
        {
            container.RegisterInstance(presenter);
            container.RegisterSingleton(nameof(EntityType.Enemy), (c) => {
                return CreateEntity(c, EntityType.Enemy);
            });
            container.RegisterSingleton(nameof(EntityType.Player), (c) => {
                return CreateEntity(c, EntityType.Player);
            });
        }
    }
}