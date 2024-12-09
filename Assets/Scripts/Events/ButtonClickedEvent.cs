namespace Events
{
    public struct ButtonClickedEvent
    {
        public bool IsNextLevelButtonClicked { get; }

        public ButtonClickedEvent(bool isNextLevelButtonClicked)
        {
            IsNextLevelButtonClicked = isNextLevelButtonClicked;
        }
    }
}