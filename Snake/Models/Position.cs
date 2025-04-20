using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    public struct Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y) => (X, Y) = (x, y);

        public override bool Equals(object obj)
        {
            if (obj is Position p)
                return p.X == X && p.Y == Y;
            return false;
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
    }
}
