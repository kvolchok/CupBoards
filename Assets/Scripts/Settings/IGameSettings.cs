using UnityEngine;

namespace Settings
{
    public interface IGameSettings
    {
        GraphViewPrefabs GraphViewPrefabs { get; }
        Color[] Colors { get; }
        GameObject StartGraphRoot { get; }
        GameObject TargetGraphRoot { get; }
    }
}