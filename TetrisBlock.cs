using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace Tetris
{
    /// <summary>
    /// All blocks take up 4 cells
    /// </summary>
    public class TetrisBlock
    {
        public TetrisShape Shape { get;  set; }
        public List<Point> Cells { get;  set; }
        public Point Position { get; set; }
        public System.Windows.Media.Color BlockColor { get; private set; }
        //public RotationAngles RotationAngle { get; set; } = RotationAngles.Zero;
        public TetrisBlock(TetrisShape shape)
        {
            Shape = shape;
            Cells = GetShapeCoordinates(shape);
            Position = new Point(4, 0); // Initial position at the top of the grid
        }
        private List<Point> GetShapeCoordinates(TetrisShape shape)
        {
            switch (shape)
            {
                case TetrisShape.I:
                    BlockColor = Colors.Cyan;
                    return new List<Point> { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1) };
                case TetrisShape.O:
                    BlockColor = Colors.Yellow;
                    return new List<Point> { new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(1, 1) };
                case TetrisShape.T:
                    BlockColor = Colors.Purple;
                    return new List<Point> { new Point(1, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) };
                case TetrisShape.S:
                    BlockColor = Colors.Green;
                    return new List<Point> { new Point(1, 0), new Point(2, 0), new Point(0, 1), new Point(1, 1) };
                case TetrisShape.Z:
                    BlockColor = Colors.Red;
                    return new List<Point> { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(2, 1) };
                case TetrisShape.J:
                    BlockColor = Colors.Blue;
                    return new List<Point> { new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) };
                case TetrisShape.L:
                    BlockColor = Colors.Orange;
                    return new List<Point> { new Point(2, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


    }
    public enum TetrisShape
    {
        I,
        J,
        L,
        O,
        S,
        T,
        Z
    }
}
