using System.Collections.Generic;
using Models;

namespace Events
{
    public struct TurnOnHighlightEvent
    {
        public List<IHighlightable> Models { get; }

        public TurnOnHighlightEvent(List<IHighlightable> models)
        {
            Models = models;
        }
    }
}