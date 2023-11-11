using System.Collections.Generic;
using System.IO;
using Utils;

namespace Settings
{
    public class LevelSettingsProvider
    {
        private readonly LevelSettingsParser _parser;

        private int _currentLevelIndex;
        private List<LevelSettings> _levelsSettings;

        public LevelSettingsProvider(LevelSettingsParser parser)
        {
            _parser = parser;
        }

        public void LoadLevels()
        {
            var files = Directory.GetFiles(GlobalConstants.LEVELS_CONFIGS_PATH);
            _levelsSettings = _parser.ParseLevelsFromTextFiles(files);
        }

        public LevelSettings GetLevel(bool shouldLoadNextLevel)
        {
            if (shouldLoadNextLevel)
            {
                _currentLevelIndex = GetNextLevelIndex();
            }
            
            return _levelsSettings[_currentLevelIndex];
        }

        private int GetNextLevelIndex()
        {
            return (_currentLevelIndex + 1) % _levelsSettings.Count;
        }
    }
}