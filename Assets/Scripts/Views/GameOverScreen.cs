using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class GameOverScreen : MonoBehaviour
    {
        [field: SerializeField] public Button RestartLevel { get; private set; }
        [field: SerializeField] public Button NextLevel { get; private set; }
    }
}