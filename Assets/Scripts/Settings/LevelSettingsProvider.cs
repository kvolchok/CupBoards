using System.Collections.Generic;
using System.IO;
using Utils;

namespace Settings
{
    public class LevelSettingsProvider : ILevelSettingsProvider
    {
        public List<ILevelSettings> LevelsSettings { get; private set; }
        
        private readonly LevelSettingsParser _parser;
        private readonly UserProgressController _userProgressController;

        public LevelSettingsProvider(LevelSettingsParser parser, UserProgressController userProgressController)
        {
            _parser = parser;
            _userProgressController = userProgressController;
            LoadConfigs();
        }

        public ILevelSettings GetCurrentLevel()
        {
            if (!_userProgressController.HasCurrentLevelIndex())
            {
                return LevelsSettings[0];
            }
            
            var currentLevelIndex = _userProgressController.LoadCurrentLevelIndex();
            currentLevelIndex %= LevelsSettings.Count;
            
            return LevelsSettings[currentLevelIndex];
        }

        private void LoadConfigs()
        {
            var files = Directory.GetFiles(GlobalConstants.LEVELS_CONFIGS_PATH);
            LevelsSettings = _parser.ParseLevelsFromTextFiles(files);
        }
    }
}