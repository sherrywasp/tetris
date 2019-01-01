using System;
using System.Data;
using System.Drawing;
using Tetris.Constants;

namespace Tetris.Model.Tetrominoes
{
    public class I : Tetromino
    {
        private Cell pivot;

        public override Color BgColor
        {
            get { return Color.Red; }
        }

        /// <summary>
        /// The position of four cells in DataTable when the tetromino is created. 
        /// It also determines the shape of the tetromino.
        /// </summary>
        public override Cell[] Original
        {
            get { return new Cell[4] { new Cell(0, 0), new Cell(0, 1), new Cell(0, 2), new Cell(0, 3) }; }
        }

        public override void Rotate(Cell[] fallingCells, DataTable matrix)
        {
            pivot = fallingCells[1];

            if (fallingCells[0].RowIndex == pivot.RowIndex)   // Horizontal => Vertical
            {
                // Make sure that the rotation is legal.
                if (pivot.RowIndex > 0
                    && (pivot.RowIndex + 2) < Game.MATRIX_HEIGHT
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex + 0]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 0]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 2]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 2][pivot.ColIndex + 0]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 2][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    )
                {
                    // If it is possible to rotate, first update each value of cells which are falling down to GAP.
                    foreach (var cell in fallingCells)
                    {
                        matrix.Rows[cell.RowIndex][cell.ColIndex] = CellStatus.GAP;
                    }

                    // Then update each value of target cells to FALL.
                    matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 2][pivot.ColIndex] = CellStatus.FALL;
                }
            }
            else // Vertical => Horizontal
            {
                if ((pivot.ColIndex + 2) < Game.MATRIX_WIDTH
                    && (pivot.ColIndex - 1) >= 0
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 2]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 2]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 2][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    )
                {
                    foreach (var cell in fallingCells)
                    {
                        matrix.Rows[cell.RowIndex][cell.ColIndex] = CellStatus.GAP;
                    }

                    matrix.Rows[pivot.RowIndex][pivot.ColIndex - 1] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex][pivot.ColIndex + 1] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex][pivot.ColIndex + 2] = CellStatus.FALL;
                }
            }
        }
    }
}
