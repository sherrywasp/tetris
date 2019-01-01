using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Tetris.Constants;
using Tetris.Model;
using Tetris.Model.Tetrominoes;

namespace Tetris
{
    public partial class Form1 : Form
    {
        static int level;
        static int score;
        static int lines;

        static DataTable matrix;
        static DataTable preview;

        static Tetromino current;
        static Tetromino next;

        static bool isGameOver;

        public Form1()
        {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            matrix = GetDataTable(Game.MATRIX_HEIGHT, Game.MATRIX_WIDTH);
            preview = GetDataTable(Game.PREVIEW_HEIGHT, Game.PREVIEW_WIDTH);

            this.dgvMatrix.DataSource = matrix;
            this.dgvPreview.DataSource = preview;

            dgvMatrix.ClearSelection();
            dgvPreview.ClearSelection();

            ResizeGrid();

            GameStart();
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            bool enableDown;
            MoveDown(out enableDown);
            AfterMoveDown(enableDown);
            MatrixRender();
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GameStart();
            }
            else if (!isGameOver)
            {
                if (e.KeyCode == Keys.Left)
                {
                    MoveLeft();
                    MatrixRender();
                }
                else if (e.KeyCode == Keys.Right)
                {
                    MoveRight();
                    MatrixRender();
                }
                else if (e.KeyCode == Keys.Down)
                {
                    bool enableDown;
                    MoveDown(out enableDown);
                    AfterMoveDown(enableDown);
                    MatrixRender();
                }
                else if (e.KeyCode == Keys.Up)
                {
                    Rotate();
                    MatrixRender();
                }
                else if (e.KeyCode == Keys.Space)
                {
                    bool enableDown;
                    do MoveDown(out enableDown); while (enableDown);
                    AfterMoveDown(enableDown);
                    MatrixRender();
                }
                else if (e.KeyCode == Keys.F8)
                {
                    timer1.Stop();
                    MessageBox.Show("PAUSE");
                    timer1.Start();
                }
            }
        }

        void GameStart()
        {
            // Reset
            isGameOver = false;
            score = 0;
            lines = 0;
            level = 1;
            timer1.Interval = 1000;
            MatrixReset();
            // New Game
            next = NewTetromino();
            PlayNext();
            PreviewRender();
            MatrixRender();
            timer1.Start();
        }

        void ResizeGrid()
        {
            for (int i = 0; i < dgvMatrix.Rows.Count; dgvMatrix.Rows[i++].Height = Game.CELL_SIDE_LEN) ;
            for (int i = 0; i < dgvMatrix.Columns.Count; dgvMatrix.Columns[i++].Width = Game.CELL_SIDE_LEN) ;
            for (int i = 0; i < dgvPreview.Rows.Count; dgvPreview.Rows[i++].Height = Game.PREVIEW_CELL_SIDE_LEN) ;
            for (int i = 0; i < dgvPreview.Columns.Count; dgvPreview.Columns[i++].Width = Game.PREVIEW_CELL_SIDE_LEN) ;
        }

        DataTable GetDataTable(int rows, int columns)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < columns; i++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < rows; i++)
            {
                DataRow row = dt.NewRow();
                for (int j = 0; j < columns; row[j++] = 0) ;
                dt.Rows.Add(row);
            }
            return dt;
        }

        Tetromino NewTetromino()
        {
            Random ran = new Random();
            string name = Game.TetrominoNames[ran.Next(0, Game.TetrominoNames.Length)];
            switch (name)
            {
                case "I": return new I();
                case "J": return new J();
                case "L": return new L();
                case "O": return new O();
                case "S": return new S();
                case "T": return new T();
                case "Z": return new Z();
                default: return null;
            }
        }

        void PlayNext()
        {
            current = next;
            next = NewTetromino();
            foreach (var item in current.Original)
            {
                matrix.Rows[item.RowIndex][item.ColIndex] = CellStatus.FALL;
            }
        }

        void MoveRight()
        {
            Cell[] fallingCells = GetFallingCells();

            foreach (var cell in fallingCells)
            {
                if (cell.ColIndex + 1 >= Game.MATRIX_WIDTH ||
                    Convert.ToInt32(matrix.Rows[cell.RowIndex][cell.ColIndex + 1]) == CellStatus.BLOCK)
                {
                    return;
                }
            }

            FallingCellsReset(fallingCells);

            foreach (var cell in fallingCells)
            {
                matrix.Rows[cell.RowIndex][cell.ColIndex + 1] = CellStatus.FALL;
            }
        }

        void MoveLeft()
        {
            Cell[] fallingCells = GetFallingCells();

            foreach (var cell in fallingCells)
            {
                if (cell.ColIndex - 1 < 0 ||
                    Convert.ToInt32(matrix.Rows[cell.RowIndex][cell.ColIndex - 1]) == CellStatus.BLOCK)
                {
                    return;
                }
            }

            FallingCellsReset(fallingCells);

            foreach (var cell in fallingCells)
            {
                matrix.Rows[cell.RowIndex][cell.ColIndex - 1] = CellStatus.FALL;
            }
        }

        void MoveDown(out bool enableDown)
        {
            enableDown = true;

            Cell[] fallingCells = GetFallingCells();

            foreach (var cell in fallingCells)
            {
                if (cell.RowIndex + 1 >= Game.MATRIX_HEIGHT ||
                    Convert.ToInt32(matrix.Rows[cell.RowIndex + 1][cell.ColIndex]) == CellStatus.BLOCK)
                {
                    enableDown = false;
                    break;
                }
            }

            FallingCellsReset(fallingCells);

            if (enableDown)
            {
                foreach (var cell in fallingCells)
                {
                    matrix.Rows[cell.RowIndex + 1][cell.ColIndex] = CellStatus.FALL;
                }
            }
            else
            {
                foreach (var cell in fallingCells)
                {
                    matrix.Rows[cell.RowIndex][cell.ColIndex] = CellStatus.BLOCK;
                }
            }
        }

        void AfterMoveDown(bool enableDown)
        {
            if (!enableDown)
            {
                GameOverDetected();
                if (!isGameOver)
                {
                    GameProgress(UnblockDetected());
                    GameProgressCount();
                    PlayNext();
                    PreviewRender();
                }
            }
        }

        void Rotate()
        {
            Cell[] fallingCells = GetFallingCells();
            current.Rotate(fallingCells, matrix);
        }

        Cell[] GetFallingCells()
        {
            Cell[] cells = new Cell[4];
            int n = 0;
            for (int i = 0; i < Game.MATRIX_HEIGHT && n < 4; i++)
            {
                for (int j = 0; j < Game.MATRIX_WIDTH && n < 4; j++)
                {
                    if (Convert.ToInt32(matrix.Rows[i][j]) == CellStatus.FALL)
                    {
                        cells[n++] = new Cell(i, j);
                    }
                }
            }
            return cells;
        }

        void FallingCellsReset(Cell[] fallingCells)
        {
            foreach (var cell in fallingCells)
            {
                matrix.Rows[cell.RowIndex][cell.ColIndex] = CellStatus.GAP;
            }
        }

        int UnblockDetected()
        {
            // The list contains the indexes of rows which should be unblocked.
            List<int> indexes = new List<int>();

            int bottom = Game.MATRIX_HEIGHT - 1;
            int top = Game.MATRIX_HEIGHT - Game.DEAD_LINE;

            for (int i = bottom; i > top && indexes.Count < 4; i--)
            {
                bool hasGap = false;
                for (int j = 0; j < Game.MATRIX_WIDTH; j++)
                {
                    if (Convert.ToInt32(matrix.Rows[i][j]) != CellStatus.BLOCK)
                    {
                        hasGap = true;
                        break;
                    }
                }
                if (!hasGap) indexes.Add(i);
            }

            if (indexes.Count > 0)
            {
                DataTable dtTemp = new DataTable();
                for (int i = 0; i < Game.MATRIX_WIDTH; i++)
                {
                    dtTemp.Columns.Add();
                }
                for (int i = 0; i < indexes.Count; i++)
                {
                    DataRow newRow = dtTemp.NewRow();
                    for (int j = 0; j < matrix.Columns.Count; newRow[j++] = CellStatus.GAP) ;
                    dtTemp.Rows.Add(newRow);
                }
                for (int i = 0; i < Game.MATRIX_HEIGHT; i++)
                {
                    if (!indexes.Contains(i))
                    {
                        DataRow newRow = dtTemp.NewRow();
                        for (int j = 0; j < matrix.Columns.Count; j++)
                        {
                            newRow[j] = matrix.Rows[i][j];
                        }
                        dtTemp.Rows.Add(newRow);
                    }
                }
                matrix = dtTemp;
            }

            // return the count of unblocking rows for scoring.
            return indexes.Count;
        }

        // Scoring
        void GameProgress(int unblockCount)
        {
            lines += unblockCount;
            switch (unblockCount)
            {
                case 1:
                    score += Score.SINGLE;
                    break;
                case 2:
                    score += Score.DOUBLE;
                    break;
                case 3:
                    score += Score.TRIPLE;
                    break;
                case 4:
                    score += Score.TETRIS;
                    break;
            }
        }

        void GameProgressCount()
        {
            if (lines >= level * Game.NEW_LEVEL_LINES)
            {
                level++;
                timer1.Stop();
                timer1.Interval = (int)(timer1.Interval * 0.9);
                timer1.Start();
            }
            this.lblLines.Text = lines.ToString();
            this.lblScore.Text = score.ToString();
            this.lblLvl.Text = level.ToString();
        }

        void GameOverDetected()
        {
            int deadRowIndex = Game.MATRIX_HEIGHT - Game.DEAD_LINE;
            for (int i = 0; i < Game.MATRIX_WIDTH; i++)
            {
                if (Convert.ToInt32(matrix.Rows[deadRowIndex][i]) == CellStatus.BLOCK)
                {
                    GameOver();
                    return;
                }
            }
        }

        void GameOver()
        {
            timer1.Stop();
            next = null;
            MessageBox.Show("GAME OVER");
            isGameOver = true;
        }

        void MatrixReset()
        {
            for (int i = 0; i < Game.MATRIX_HEIGHT; i++)
            {
                for (int j = 0; j < Game.MATRIX_WIDTH; j++)
                {
                    matrix.Rows[i][j] = CellStatus.GAP;
                }
            }
        }

        void PreviewReset()
        {
            for (int i = 0; i < Game.PREVIEW_HEIGHT; i++)
            {
                for (int j = 0; j < Game.PREVIEW_WIDTH; j++)
                {
                    preview.Rows[i][j] = CellStatus.GAP;
                }
            }
        }

        void MatrixRender()
        {
            for (int i = 0; i < Game.MATRIX_HEIGHT; i++)
            {
                for (int j = 0; j < Game.MATRIX_WIDTH; j++)
                {
                    int cellStatus = Convert.ToInt32(matrix.Rows[i][j]);
                    DataGridViewCell cell = this.dgvMatrix.Rows[i].Cells[j];

                    if (cellStatus == CellStatus.FALL)
                    {
                        cell.Style.BackColor = cell.Style.ForeColor = current.BgColor;
                    }
                    else if (cellStatus == CellStatus.BLOCK)
                    {
                        cell.Style.BackColor = cell.Style.ForeColor = Game.BgColor[level - 1];
                    }
                    else
                    {
                        cell.Style.BackColor = cell.Style.ForeColor = Color.Black;
                    }
                }
            }
        }

        void PreviewRender()
        {
            PreviewReset();
            foreach (var item in next.Original)
            {
                preview.Rows[item.RowIndex][item.ColIndex] = CellStatus.FALL;
            }
            for (int i = 0; i < Game.PREVIEW_HEIGHT; i++)
            {
                for (int j = 0; j < Game.PREVIEW_WIDTH; j++)
                {
                    int cellStatus = Convert.ToInt32(preview.Rows[i][j]);
                    DataGridViewCell cell = this.dgvPreview.Rows[i].Cells[j];
                    cell.Style.BackColor = cell.Style.ForeColor = cellStatus == CellStatus.FALL ? next.BgColor : this.BackColor;
                }
            }
        }
    }
}