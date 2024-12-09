using System;
using UnityEngine;

namespace Views
{
    public class NodeView : HighlightableView
    {
        public event Action NodeSelected;

        public void Initialize(Vector3 position)
        {
            transform.localPosition = position;
        }

        private void OnMouseDown()
        {
            NodeSelected?.Invoke();
        }
    }
}