using Models;

namespace Events
{
    public struct ShowGraphEvent
    {
        public GraphModel GraphModel { get; }
        public bool IsInteractable { get; }

        public ShowGraphEvent(GraphModel graphModel, bool isInteractable = true)
        {
            GraphModel = graphModel;
            IsInteractable = isInteractable;
        }
    }
}