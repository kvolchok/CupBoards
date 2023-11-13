using UnityEngine;

namespace Models
{
    public class ChipModel : IHighlightable
    {
        public Color Color { get; }
        public int Id { get; }

        public ChipModel(Color color, int id)
        {
            Color = color;
            Id = id;
        }
    }
}