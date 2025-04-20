using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    public class GameState
    {
        public Snake1 Snake { get; private set; }
        public Food Food { get; private set; }
        public List<Position> Obstacles { get; private set; } = new List<Position>();
        public int Width { get; }
        public int Height { get; }
        public bool IsGameOver { get; private set; }
        public int Score => Snake.Body.Count - 3;
        public bool HardMode { get; }

        private Random rand = new Random();

        public GameState(int width, int height, bool hardMode)
        {
            Width = width;
            Height = height;
            HardMode = hardMode;
            Snake = new Snake1(new Position(width / 2, height / 2));
            Food = new Food(GetRandomEmptyPosition());
            Obstacles = new List<Position>();

            if (hardMode)
                GenerateObstacles();
        }

        public void Update()
        {
            if (IsGameOver) return;

            Snake.Move();

            if (Snake.IsCollisionWithSelf() || Snake.Head.X < 0 || Snake.Head.Y < 0 || Snake.Head.X >= Width || Snake.Head.Y >= Height)
            {
                IsGameOver = true;
                return;
            }

            if (HardMode && Obstacles.Any(o => o.Equals(Snake.Head)))
            {
                IsGameOver = true;
                return;
            }

            if (Snake.Head.Equals(Food.Position))
            {
                Snake.Grow();
                Food = new Food(GetRandomEmptyPosition());
            }

            // Ускорение
            Snake.Speed = 200 - Math.Min(Score * 5, 150);
        }

        private Position GetRandomEmptyPosition()
        {
            Position pos;
            do
            {
                pos = new Position(rand.Next(Width), rand.Next(Height));
            }
            while (Snake.Body.Contains(pos) || (HardMode && Obstacles.Contains(pos)));

            return pos;
        }

        private void GenerateObstacles()
        {
            for (int i = 0; i < 20; i++)
            {
                Obstacles.Add(GetRandomEmptyPosition());
            }
        }
    }
}
