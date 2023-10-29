using UnityEngine;

namespace Views
{
    public class EdgeView : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer _line;

        public void Show(NodeView from, NodeView to)
        {
            _line.positionCount = 2;
            _line.widthMultiplier *= transform.parent.localScale.x;
        
            _line.SetPosition(0, from.transform.localPosition);
            _line.SetPosition(1, to.transform.localPosition);
        }
    }
}