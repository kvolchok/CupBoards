using Models;

namespace Events
{
    public struct ChipHighlightedEvent
    {
        public ChipModel ChipModel { get; }
        public bool IsActive { get; }

        public ChipHighlightedEvent(ChipModel chipModel, bool isActive)
        {
            ChipModel = chipModel;
            IsActive = isActive;
        }
    }
}