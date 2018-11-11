using System;
using System.Collections.Generic;
using System.Text;

namespace CascadingGems
{
    public class Field
    {
        public Shape Shape { get; set; }
        public Position Position { get; set; }
        public bool IsFull { get; set; }
        public override string ToString()
        {
            return $"Shape: {Shape} Position: x:{Position.X}, y:{Position.Y} isFull: {IsFull}";
        }



    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public enum Shape { O = 1, V = 2, H = 3 }
}
