namespace GameStates
{
    public struct LevelLoaderStateContext
    {
        public bool IsLevelCompleted { get; private set; }
        
        public LevelLoaderStateContext(bool isLevelCompleted)
        {
            IsLevelCompleted = isLevelCompleted;
        }
    }
}