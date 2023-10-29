using System;
using UnityEngine;
using Views;

namespace Settings
{
    [Serializable]
    public class GraphViewPrefabs
    {
        [field: SerializeField]
        public NodeView NodePrefab { get; private set; }
        [field: SerializeField]
        public ChipView ChipPrefab { get; private set; }
        [field: SerializeField]
        public EdgeView EdgePrefab { get; private set; }
    }
}