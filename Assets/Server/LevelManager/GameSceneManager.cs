using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.LevelManager
{
    public class GameSceneManager : LevelManager
    {
        protected override int _maxLevelIndex { get; set; }

        private readonly int _ignoredSceneIndex = 2;
        private readonly int _sceneIndexOffset = 1;

        public GameSceneManager()
        {
            _maxLevelIndex = SceneManager.sceneCountInBuildSettings - _ignoredSceneIndex;
        }

        public override void LoadLevel(int sceneIndex)
        {
            if (!IsValidSceneIndex(sceneIndex))
                return;

            LoadScene(sceneIndex);
        }

        public override void ReloadingCurrentLevel()
        {
            LoadScene(_currentLevelIndex);
        }

        private async void LoadScene(int sceneIndex)
        {
            OnLevelStartedToLoad();

            _currentLevelIndex = sceneIndex;
            await SceneManager.LoadSceneAsync(sceneIndex + _sceneIndexOffset);

            OnLevelLoaded();
        }

        private bool IsValidSceneIndex(int sceneIndex)
        {
            return sceneIndex >= 0 && sceneIndex <= _maxLevelIndex && sceneIndex != _entryPointIndex;
        }
    }
}
