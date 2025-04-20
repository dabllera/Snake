using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    public class Food
    {
        public Position Position { get; }

        public Food(Position pos)
        {
            Position = pos;
        }
    }
}
