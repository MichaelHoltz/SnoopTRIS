using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tetris
{
    /// <summary>
    /// https://tetris.fandom.com/wiki/Tetromino
    /// https://harddrop.com/wiki/Tetris_(NES,_Nintendo)
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        private TetrisGame _game;
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();

            
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MediaElement1.Play();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500); // Move the block down every 500ms
            _timer.Tick += Timer_Tick;
        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _game = new TetrisGame();
            _timer.Start();
            DrawGame();

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_game.IsGameOver)
            {
                _timer.Stop();
                Sound.PlayEventSound(SoundEvents.AlignmentFailed);
                MessageBox.Show("Game Over!");
                return;
            }

            _game.MoveBlockDown();
            DrawGame();
        }

        private void DrawGame()
        {
            GameCanvas.Children.Clear();
            DrawGrid();
            DrawCurrentBlock();
            lblScore.Content = _game.Score;
        }

        private void DrawGrid()
        {
            for (int x = 0; x < TetrisGame.GridWidth; x++)
            {
                for (int y = 0; y < TetrisGame.GridHeight; y++)
                {
                    if (_game.Grid[x, y].IsOccupied)
                    {
                        DrawCell(x, y, _game.Grid[x, y].CellColor);
                    }
                }
            }
        }

        private void DrawCurrentBlock()
        {
            foreach (Point cell in _game.CurrentBlock.Cells)
            {
                int x = (int)_game.CurrentBlock.Position.X + (int)cell.X;
                int y = (int)_game.CurrentBlock.Position.Y + (int)cell.Y;
                DrawCell(x, y, _game.CurrentBlock.BlockColor);
            }
        }

        private void DrawCell(int x, int y, Color color)
        {
            int cellSize = 20; // Size of each cell
            Rectangle rect = new Rectangle
            {
                Width = cellSize,
                Height = cellSize,
                Fill = new SolidColorBrush(color),
                Stroke = Brushes.Black
            };
            Canvas.SetLeft(rect, x * cellSize);
            Canvas.SetTop(rect, y * cellSize);
            GameCanvas.Children.Add(rect);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (_game.IsGameOver) return;

            switch (e.Key)
            {
                case Key.Left:
                    _game.MoveBlockLeft();
                    break;
                case Key.Right:
                    _game.MoveBlockRight();
                    break;
                case Key.Down:
                    _game.MoveBlockDown();
                    break;
                case Key.Up:
                    _game.RotateBlock();
                    break;
            }

            DrawGame();
        }

        private void MediaElement1_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaElement1.Position = new TimeSpan(0,0,0,0, 1);
            MediaElement1.Play();
        }

        private void MediaElement1_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

 
    }
}