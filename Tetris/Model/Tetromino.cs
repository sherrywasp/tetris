using System.Data;
using System.Drawing;

namespace Tetris.Model
{
    /// <summary>
    /// A tetromino is a geometric shape composed of four squares, connected orthogonally.
    /// </summary>
    public abstract class Tetromino
    {
        public abstract Color BgColor { get; }
        public abstract Cell[] Original { get; }
        public abstract void Rotate(Cell[] fallingCells, DataTable matrix);
    }
}
