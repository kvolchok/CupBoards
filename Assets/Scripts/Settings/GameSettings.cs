using System;
using UnityEngine;

namespace Settings
{
    [Serializable]
    public class GameSettings
    {
        [field:SerializeField]
        public GraphViewPrefabs GraphViewPrefabs { get; private set; }
    
        [field:SerializeField]
        public Color[] Colors { get; private set; }
        
        [field:SerializeField]
        public GameObject StartGraphView { get; private set; }
        [field:SerializeField]
        public GameObject TargetGraphView { get; private set; }
    }
}