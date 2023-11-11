using UnityEngine;

namespace Settings
{
    public class UserProgressController
    {
        private const string LEVEL_KEY = "Level";

        public bool HasCurrentLevelIndex()
        {
            return PlayerPrefs.HasKey(LEVEL_KEY);
        }
        
        public int LoadCurrentLevelIndex()
        {
            return PlayerPrefs.GetInt(LEVEL_KEY);
        }
        
        public void SaveCurrentLevelIndex(int level)
        {
            PlayerPrefs.SetInt(LEVEL_KEY, level);
            PlayerPrefs.Save();
        }
    }
}