using Settings;
using UnityEngine;

namespace Tests.SharedTestUtilities
{
    public class TestGameSettings : IGameSettings
    {
        public Color[] Colors { get; } = 
        {
            new(255, 127, 0),
            new(255, 255, 0),
            new(0, 255, 0),
            new(0, 0, 255),
            new(0, 127, 255),
            new(117, 236, 0),
            new(200, 70, 200),
            new(70, 200, 200)
        };
    }
}