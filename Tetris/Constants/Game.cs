using System.Drawing;

namespace Tetris.Constants
{
    /// <summary>
    /// Some constants in game
    /// </summary>
    public class Game
    {
        // The size of Tetris on NES is as follows 
        public const int MATRIX_HEIGHT = 25;
        public const int MATRIX_WIDTH = 10;

        public const int CELL_SIDE_LEN = 23;
        public const int PREVIEW_CELL_SIDE_LEN = 18;

        public const int PREVIEW_HEIGHT = 3;
        public const int PREVIEW_WIDTH = 4;

        // Even though the matrix contains 25 rows, the allowable space is 20 rows.
        public const int DEAD_LINE = 21;
        public const int NEW_LEVEL_LINES = 30;
        
        public static readonly string[] TetrominoNames = { "I", "J", "L", "O", "S", "T", "Z" };

        // This background color settings come from NES.
        public static readonly Color[] BgColor = { 
                                                     Color.Gray, 
                                                     Color.Red, 
                                                     Color.Goldenrod, 
                                                     Color.Blue, 
                                                     Color.Yellow, 
                                                     Color.Magenta, 
                                                     Color.Green, 
                                                     Color.Teal, 
                                                     Color.Purple, 
                                                     Color.White 
                                                 };

    }
}
