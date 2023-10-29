using System;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    [Serializable]
    public class GameSettings
    {
        [field:SerializeField]
        public GraphViewPrefabs GraphViewPrefabs { get; private set; }
    
        [field:SerializeField]
        public Color[] Colors { get; private set; }
    
        public IReadOnlyList<LevelSettings> LevelsSettings { get; private set; }
    
        public void SaveLevelsSettings(IReadOnlyList<LevelSettings> levelsSettings)
        {
            LevelsSettings = levelsSettings;
        }
    }
}