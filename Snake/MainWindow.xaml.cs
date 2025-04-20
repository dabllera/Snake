using Snake.Models;
using Snake.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameState gameState;
        private DispatcherTimer timer;
        private int cellSize = 20;

        public MainWindow()
        {
            InitializeComponent();
            StartGame();
        }

        private void StartGame(bool hardMode = false)
        {
            GameCanvas.Children.Clear();
            gameState = new GameState(30, 30, hardMode); // 30x30 поле
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            gameState.Update();

            if (gameState.IsGameOver)
            {
                timer.Stop();
                MessageBox.Show($"Игра окончена! Счёт: {gameState.Score}");
                ScoreManager.SaveScore(gameState.Score, gameState.HardMode);
                StartGame(gameState.HardMode);
            }

            DrawGame();
        }

        private void DrawGame()
        {
            GameCanvas.Children.Clear();

            // Змейка
            foreach (var pos in gameState.Snake.Body)
            {
                var rect = new Rectangle
                {
                    Width = cellSize,
                    Height = cellSize,
                    Fill = Brushes.Green
                };
                Canvas.SetLeft(rect, pos.X * cellSize);
                Canvas.SetTop(rect, pos.Y * cellSize);
                GameCanvas.Children.Add(rect);
            }

            // Еда
            var food = new Rectangle
            {
                Width = cellSize,
                Height = cellSize,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(food, gameState.Food.Position.X * cellSize);
            Canvas.SetTop(food, gameState.Food.Position.Y * cellSize);
            GameCanvas.Children.Add(food);

            // Препятствия
            if (gameState.HardMode)
            {
                foreach (var obs in gameState.Obstacles)
                {
                    var obstacle = new Rectangle
                    {
                        Width = cellSize,
                        Height = cellSize,
                        Fill = Brushes.Gray
                    };
                    Canvas.SetLeft(obstacle, obs.X * cellSize);
                    Canvas.SetTop(obstacle, obs.Y * cellSize);
                    GameCanvas.Children.Add(obstacle);
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: gameState.Snake.ChangeDirection(Direction.Up); break;
                case Key.Down: gameState.Snake.ChangeDirection(Direction.Down); break;
                case Key.Left: gameState.Snake.ChangeDirection(Direction.Left); break;
                case Key.Right: gameState.Snake.ChangeDirection(Direction.Right); break;
                case Key.H: StartGame(true); break; // Усложнённый режим
                case Key.C: StartGame(false); break; // Классика
            }
        }
    }
}
