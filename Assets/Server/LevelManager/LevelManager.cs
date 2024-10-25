using Assets.EntryPoint;
using Assets.MVP;
using UnityEngine.Events;

namespace Assets.LevelManager
{
    public abstract class LevelManager : ILevelManager, IModel
    {
        protected static readonly int _entryPointIndex = -1;
        protected static readonly int _mainMenuIndex = 0;

        protected int _currentLevelIndex { get; set; } = _entryPointIndex;
        protected abstract int _maxLevelIndex { get; set; }

        public event UnityAction LevelStartedToLoad;
        public event UnityAction<int> LevelLoaded;

        public abstract void LoadLevel(int sceneIndex);
        public abstract void ReloadingCurrentLevel();

        protected void OnLevelLoaded() => LevelLoaded?.Invoke(_currentLevelIndex);
        protected void OnLevelStartedToLoad() => LevelStartedToLoad?.Invoke();

        public void SubscribeToEvents(IResolver container)
        {
            container.Resolve<UIEvents>().RestartLevel += ReloadingCurrentLevel;
        }

        public int GetLevelIndex() => _currentLevelIndex;
        public int GetAmountLevels() => _maxLevelIndex;
    }
}
