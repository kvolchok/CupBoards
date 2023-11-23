using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Settings;
using UnityEngine;

namespace Utils
{
    public class LevelSettingsParser
    {
        public List<ILevelSettings> ParseLevelsFromTextFiles(string[] files)
        {
            var levelsSettings = new List<ILevelSettings>();

            foreach (var file in files)
            {
                if (!file.Contains("meta"))
                {
                    var levelSettings = ParseLevel(file);
                    levelsSettings.Add(levelSettings);
                }
            }

            return levelsSettings;
        }

        private ILevelSettings ParseLevel(string levelConfig)
        {
            using var streamReader = new StreamReader(levelConfig);

            ParseChipsCount(streamReader);

            var nodesPositions = ParsePairs(streamReader);

            var startChipsPositions = ParseChipsPositions(streamReader);
            var targetChipsPositions = ParseChipsPositions(streamReader);

            var connections = ParsePairs(streamReader);

            var levelSettings = new LevelSettings(
                nodesPositions, connections, startChipsPositions, targetChipsPositions);
        
            return levelSettings;
        }

        private int ParseChipsCount(StreamReader streamReader)
        {
            return Convert.ToInt32(streamReader.ReadLine());
        }
    
        private List<Vector2Int> ParsePairs(StreamReader streamReader)
        {
            var list = new List<Vector2Int>();
        
            var count = Convert.ToInt32(streamReader.ReadLine());
            for (var i = 0; i < count; i++)
            {
                var pairs = streamReader.ReadLine()?.Split(",");

                var x = Convert.ToInt32(pairs?[0]);
                var y = Convert.ToInt32(pairs?[1]);

                var element = new Vector2Int(x, y);
                list.Add(element);
            }

            return list;
        }

        private List<int> ParseChipsPositions(StreamReader streamReader)
        {
            return streamReader.ReadLine()?
                .Split(",")
                .Select(position => Convert.ToInt32(position))
                .ToList();
        }
    }
}