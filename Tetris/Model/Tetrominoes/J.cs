using System;
using System.Data;
using System.Drawing;
using Tetris.Constants;

namespace Tetris.Model.Tetrominoes
{
    public class J : Tetromino
    {
        // There are four shapes in the rotation of J.
        private int shape = 1;
        private Cell pivot;

        public override Color BgColor
        {
            get { return Color.Yellow; }
        }

        /// <summary>
        /// The position of four cells in DataTable when the tetromino is created. 
        /// It also determines the shape of the tetromino.
        /// </summary>
        public override Cell[] Original
        {
            get { return new Cell[4] { new Cell(0, 1), new Cell(1, 1), new Cell(2, 1), new Cell(2, 0) }; }
        }

        public override void Rotate(Cell[] fallingCells, DataTable matrix)
        {

            if (shape == 1)
            {
                pivot = fallingCells[1];
                // Make sure that the rotation is legal.
                if (pivot.ColIndex + 1 < Game.MATRIX_WIDTH
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    )
                {
                    // If it is possible to rotate, first update each value of cells which are falling down to GAP.
                    foreach (var cell in fallingCells)
                    {
                        matrix.Rows[cell.RowIndex][cell.ColIndex] = CellStatus.GAP;
                    }

                    // Then update each value of target cells to FALL.
                    matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex - 1] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex - 1] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 1] = CellStatus.FALL;

                    // After rotation, update shape.
                    shape++;
                }
            }
            else if (shape == 2)
            {
                pivot = fallingCells[2];

                if ((pivot.RowIndex - 1) >= 0
                    && (pivot.RowIndex + 1) < Game.MATRIX_HEIGHT
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex + 0]) != CellStatus.BLOCK)
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
                    matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex + 1] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 0] = CellStatus.FALL;

                    shape++;
                }
            }
            else if (shape == 3)
            {
                pivot = fallingCells[2];

                if ((pivot.ColIndex - 1) >= 0
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 1]) != CellStatus.BLOCK)
                    )
                {
                    foreach (var cell in fallingCells)
                    {
                        matrix.Rows[cell.RowIndex][cell.ColIndex] = CellStatus.GAP;
                    }

                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 1] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex - 1] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 1] = CellStatus.FALL;

                    shape++;
                }
            }
            else if (shape == 4)
            {
                pivot = fallingCells[1];

                if ((pivot.RowIndex - 1) >= 0
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex + 0]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex - 1]) != CellStatus.BLOCK)
                    && (Convert.ToInt32(matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 0]) != CellStatus.BLOCK)
                    )
                {
                    foreach (var cell in fallingCells)
                    {
                        matrix.Rows[cell.RowIndex][cell.ColIndex] = CellStatus.GAP;
                    }

                    matrix.Rows[pivot.RowIndex - 1][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 0][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex + 0] = CellStatus.FALL;
                    matrix.Rows[pivot.RowIndex + 1][pivot.ColIndex - 1] = CellStatus.FALL;

                    shape = 1;
                }
            }
        }
    }
}
