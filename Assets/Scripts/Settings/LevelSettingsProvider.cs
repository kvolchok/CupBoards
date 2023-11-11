using System.Collections.Generic;
using System.IO;
using Utils;

namespace Settings
{
    public class LevelSettingsProvider
    {
        private readonly LevelSettingsParser _parser;
        private readonly UserProgressController _userProgressController;

        private List<LevelSettings> _levelsSettings;

        public LevelSettingsProvider(LevelSettingsParser parser, UserProgressController userProgressController)
        {
            _parser = parser;
            _userProgressController = userProgressController;
        }

        public void LoadConfigs()
        {
            var files = Directory.GetFiles(GlobalConstants.LEVELS_CONFIGS_PATH);
            _levelsSettings = _parser.ParseLevelsFromTextFiles(files);
        }

        public LevelSettings GetCurrentLevel()
        {
            if (!_userProgressController.HasCurrentLevelIndex())
            {
                return _levelsSettings[0];
            }
            
            var currentLevelIndex = _userProgressController.LoadCurrentLevelIndex();
            currentLevelIndex %= _levelsSettings.Count;
            
            return _levelsSettings[currentLevelIndex];
        }
    }
}