using System.Collections.Generic;

namespace Settings
{
    public interface ILevelSettingsProvider
    {
        List<ILevelSettings> LevelsSettings { get; }
        ILevelSettings GetCurrentLevel();
    }
}