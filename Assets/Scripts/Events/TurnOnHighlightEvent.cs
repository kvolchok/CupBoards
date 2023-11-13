using Models;

namespace Events
{
    public struct TurnOnHighlightEvent
    {
        public IHighlightable[] Models { get; }

        public TurnOnHighlightEvent(params IHighlightable[] models)
        {
            Models = models;
        }
    }
}