using System.Collections.Generic;

namespace Settings
{
    public interface ILevelSettingsProvider
    {
        List<ILevelSettings> LevelsSettings { get; }
        void LoadConfigs();
        ILevelSettings GetCurrentLevel();
    }
}