using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    public class Snake1
    {
        public List<Position> Body { get; private set; }
        public Position Head => Body[0];
        private Direction currentDirection;
        public int Speed { get; set; }

        public Snake1(Position start)
        {
            Body = new List<Position> { start, new Position(start.X - 1, start.Y), new Position(start.X - 2, start.Y) };
            currentDirection = Direction.Right;
        }

        public void Move()
        {
            Position newHead = Head;

            switch (currentDirection)
            {
                case Direction.Up: newHead = new Position(Head.X, Head.Y - 1); break;
                case Direction.Down: newHead = new Position(Head.X, Head.Y + 1); break;
                case Direction.Left: newHead = new Position(Head.X - 1, Head.Y); break;
                case Direction.Right: newHead = new Position(Head.X + 1, Head.Y); break;
            }

            Body.Insert(0, newHead);
            Body.RemoveAt(Body.Count - 1);
        }

        public void Grow()
        {
            Body.Add(Body[Body.Count - 1]);
        }

        public void ChangeDirection(Direction direction)
        {
            // Нельзя повернуть на 180 градусов
            if ((currentDirection == Direction.Up && direction == Direction.Down) ||
                (currentDirection == Direction.Down && direction == Direction.Up) ||
                (currentDirection == Direction.Left && direction == Direction.Right) ||
                (currentDirection == Direction.Right && direction == Direction.Left))
                return;

            currentDirection = direction;
        }

        public bool IsCollisionWithSelf()
        {
            for (int i = 1; i < Body.Count; i++)
                if (Body[i].Equals(Head))
                    return true;

            return false;
        }
    }

    public enum Direction { Up, Down, Left, Right }
}
