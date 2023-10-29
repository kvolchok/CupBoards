using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class FinishGameScreen : MonoBehaviour
    {
        [field:SerializeField]
        public Button RestartLevel { get; private set; }
        [field:SerializeField]
        public Button NextLevel { get; private set; }
    }
}