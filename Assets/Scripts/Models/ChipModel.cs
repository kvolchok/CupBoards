using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Models
{
    public class ChipModel : IHighlightable
    {
        public event Func<Vector3, UniTask> PositionChanged; 
        
        public Color Color { get; }
        public int Id { get; }

        public ChipModel(Color color, int id)
        {
            Color = color;
            Id = id;
        }

        public async UniTask ChangePosition(Vector3 nextPosition)
        {
            await PositionChanged.Invoke(nextPosition);
        }
    }
}