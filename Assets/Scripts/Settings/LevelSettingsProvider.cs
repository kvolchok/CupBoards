namespace Settings
{
    public class LevelSettingsProvider
    {
        private readonly GameSettings _gameSettings;

        private int _currentLevelIndex;

        public LevelSettingsProvider(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public LevelSettings GetLevel(bool shouldLoadNextLevel)
        {
            if (shouldLoadNextLevel)
            {
                _currentLevelIndex = GetNextLevelIndex();
            }
            
            return _gameSettings.LevelsSettings[_currentLevelIndex];
        }

        private int GetNextLevelIndex()
        {
            return (_currentLevelIndex + 1) % _gameSettings.LevelsSettings.Count;
        }
    }
}