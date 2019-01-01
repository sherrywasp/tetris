using System;
using System.Data;
using System.Drawing;
using Tetris.Constants;

namespace Tetris.Model.Tetrominoes
{
    public class T : Tetromino
    {
        private int shape = 1;
        private Cell pivot;

        public override Color BgColor
        {
            get { return Color.Goldenrod; }
        }

        public override Cell[] Original
        {
            get { return new Cell[4] { new Cell(0, 0), new Cell(0, 1), new Cell(0, 2), new Cell(1, 1) }; }
        }

        public override void Rotate(Cell[] fallingCells, DataTable matrix)
        {
            if (shape == 1)
            {
                pivot = fallingCells[1];

                if (pivot.RowIndex - 1 >= 0
                    && pivot.ColIndex - 1 >= 0
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex + 0]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    )
                {
                    foreach (var cell in fallingCells)
                    {
                        matrix.Rows[cell.RowIndex][cell.ColIndex] = CellStatus.GAP;
                    }

                    matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex - 1] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 0] = CellStatus.FALL;
                    shape++;
                }
            }
            else if (shape == 2)
            {
                pivot = fallingCells[2];

                if ((pivot.ColIndex + 1) < Game.MATRIX_WIDTH
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    )
                {
                    foreach (var cell in fallingCells)
                    {
                        matrix.Rows[cell.RowIndex][cell.ColIndex] = CellStatus.GAP;
                    }

                    matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex - 1] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 1] = CellStatus.FALL;

                    shape++;
                }
            }
            else if (shape == 3)
            {
                pivot = fallingCells[2];

                if ((pivot.RowIndex + 1) < Game.MATRIX_HEIGHT
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 0]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    )
                {
                    foreach (var cell in fallingCells)
                    {
                        matrix.Rows[cell.RowIndex][cell.ColIndex] = CellStatus.GAP;
                    }

                    matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 1] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 0] = CellStatus.FALL;

                    shape++;
                }
            }
            else if (shape == 4)
            {
                pivot = fallingCells[1];

                if (pivot.ColIndex - 1 >= 0
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    )
                {
                    foreach (var cell in fallingCells)
                    {
                        matrix.Rows[cell.RowIndex][cell.ColIndex] = CellStatus.GAP;
                    }

                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex - 1] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 1] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 0] = CellStatus.FALL;

                    shape = 1;
                }
            }
        }
    }
}
