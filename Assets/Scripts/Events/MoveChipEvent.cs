using System.Collections.Generic;
using Models;

namespace Events
{
    public struct MoveChipEvent
    {
        public ChipModel CurrentChip { get; }
        public Stack<NodeModel> Route { get; }

        public MoveChipEvent(ChipModel currentChip, Stack<NodeModel> route)
        {
            CurrentChip = currentChip;
            Route = route;
        }
    }
}