namespace Tetris.Model
{
    /// <summary>
    /// Read/Write a cell position of DataTable 
    /// </summary>
    public class Cell
    {
        public int RowIndex;
        public int ColIndex;

        public Cell() { }

        public Cell(int rowIndex, int colIndex)
        {
            RowIndex = rowIndex;
            ColIndex = colIndex;
        }
    }
}
