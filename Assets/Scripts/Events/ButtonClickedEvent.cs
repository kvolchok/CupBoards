namespace Events
{
    public struct ButtonClickedEvent
    {
        public bool IsNextLevelButtonClicked { get; private set; }
        
        public ButtonClickedEvent(bool isNextLevelButtonClicked)
        {
            IsNextLevelButtonClicked = isNextLevelButtonClicked;
        }
    }
}