using System.Data;
using System.Drawing;

namespace Tetris.Model.Tetrominoes
{
    public class O : Tetromino
    {
        public override Color BgColor
        {
            get { return Color.Blue; }
        }

        public override Cell[] Original
        {
            get { return new Cell[4] { new Cell(0,0), new Cell(0,1), new Cell(1,0), new Cell(1,1) }; }
        }

        public override void Rotate(Cell[] fallingCells, DataTable matrix)
        {
            // pass
        }
    }
}
