using System.Windows;
using System.Windows.Media;
namespace Tetris
{
    public class TetrisGame
    {
        public const int GridWidth = 10;
        public const int GridHeight = 20;
        
        private TetrisBlock _currentBlock;
        private Random _random = new Random();
        public int Score { get; set; } = 0;

        public (bool IsOccupied, Color CellColor)[,] Grid = new (bool, Color)[GridWidth, GridHeight];

        public TetrisBlock CurrentBlock => _currentBlock;
        public bool IsGameOver { get; private set; }


        public TetrisGame()
        {
            _currentBlock = CreateRandomBlock();
        }

        private TetrisBlock CreateRandomBlock()
        {
            Array shapes = Enum.GetValues(typeof(TetrisShape));
            TetrisShape randomShape = (TetrisShape)shapes.GetValue(_random.Next(shapes.Length));
            var newBlock = new TetrisBlock(randomShape);
            if (!CanMove(newBlock, 0, 0))
            {
                IsGameOver = true;
            }
            return newBlock;
        }

        public void MoveBlockDown()
        {
            if (IsGameOver) return;

            if (CanMove(_currentBlock, 0, 1))
            {
                
                Point position = _currentBlock.Position;
                position.Y += 1;
                _currentBlock.Position = position;
            }
            else
            {
                // Place the block on the grid and create a new block
                PlaceBlock();
                _currentBlock = CreateRandomBlock();
            }
        }

        public void MoveBlockLeft()
        {
            if (IsGameOver) return;

            if (CanMove(_currentBlock, -1, 0))
            {
                Point position = _currentBlock.Position;
                position.X -= 1;
                _currentBlock.Position = position;
            }
        }

        public void MoveBlockRight()
        {
            if (IsGameOver) return;

            if (CanMove(_currentBlock, 1, 0))
            {
                Point position = _currentBlock.Position;
                position.X += 1;
                _currentBlock.Position = position;
            }
        }

        public void RotateBlock()
        {
            if (IsGameOver) return;

            var newCells = new List<Point>();
            foreach (Point cell in _currentBlock.Cells)
            {
                // Rotate 90 degrees clockwise
                newCells.Add(new Point(-cell.Y, cell.X));
            }

            // Check if the new position is valid
            if (CanMove(_currentBlock, newCells, 0, 0))
            {
                _currentBlock.Cells = newCells;
            }
        }

        private bool CanMove(TetrisBlock block, int deltaX, int deltaY)
        {
            foreach (Point cell in block.Cells)
            {
                int newX = (int)block.Position.X + (int)cell.X + deltaX;
                int newY = (int)block.Position.Y + (int)cell.Y + deltaY;

                if (newX < 0 || newX >= GridWidth || newY < 0 || newY >= GridHeight || Grid[newX, newY].IsOccupied)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CanMove(TetrisBlock block, List<Point> newCells, int deltaX, int deltaY)
        {
            foreach (Point cell in newCells)
            {
                int newX = (int)block.Position.X + (int)cell.X + deltaX;
                int newY = (int)block.Position.Y + (int)cell.Y + deltaY;

                if (newX < 0 || newX >= GridWidth || newY < 0 || newY >= GridHeight || Grid[newX, newY].IsOccupied)
                {
                    return false;
                }
            }
            return true;
        }

        private void PlaceBlock()
        {
            foreach (Point cell in _currentBlock.Cells)
            {
                int x = (int)_currentBlock.Position.X + (int)cell.X;
                int y = (int)_currentBlock.Position.Y + (int)cell.Y;
                Grid[x, y] = (true, _currentBlock.BlockColor);
            }
            Sound.PlayEventSound(SoundEvents.DefectFound);
            CheckForCompleteRows();
        }

        private void CheckForCompleteRows()
        {
            for (int y = 0; y < GridHeight; y++)
            {
                bool isComplete = true;
                for (int x = 0; x < GridWidth; x++)
                {
                    if (!Grid[x, y].IsOccupied)
                    {
                        isComplete = false;
                        break;
                    }
                }
                if (isComplete)
                {
                    Sound.PlayEventSound(SoundEvents.AlignmentSuccess);
                    Score += y*10;
                    
                    DeleteRow(y);
                }
            }
        }

        private void DeleteRow(int row)
        {
            for (int y = row; y > 0; y--)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    Grid[x, y] = Grid[x, y - 1];
                }
            }
            // Clear the top row
            for (int x = 0; x < GridWidth; x++)
            {
                Grid[x, 0] = (false, Colors.Transparent);
            }
        }
    }
}