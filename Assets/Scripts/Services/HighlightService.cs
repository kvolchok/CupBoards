using System.Collections.Generic;
using Models;

namespace Services
{
    public class HighlightService
    {
        private readonly List<HighlightableObject> _highlightableObjects = new();
    
        public void TurnOnHighlight(params HighlightableObject[] highlightableObjects)
        {
            foreach (var highlightableObject in highlightableObjects)
            {
                _highlightableObjects.Add(highlightableObject);
                highlightableObject.ToggleHighlight(true);
            }
        }
    
        public void TurnOffHighlight()
        {
            foreach (var highlightableObject in _highlightableObjects)
            {
                highlightableObject.ToggleHighlight(false);
            }
        
            _highlightableObjects.Clear();
        }
    }
}