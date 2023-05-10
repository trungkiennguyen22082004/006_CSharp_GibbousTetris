namespace GibbousTetris
{
    public static class Constants
    {
        // LOCATION
        public const string MEDIA_FOLDER_LOCATION = "D:/#006 - C# - GibbousTetris/GibbousTetris/media/";

        // -------------------------------------------------------------------------
        // WINDOW
        public const int WINDOW_WIDTH = 800;
        public const int WINDOW_HEIGHT = 800;

        // -------------------------------------------------------------------------
        // GAMEBOARD'S CONSTANTS
        //      Position of the gameboard
        public const double BOARD_X_POSITION = 50;
        public const double BOARD_Y_POSITION = 100;
        //      Number of decorative lines per X side of a cell
        public const uint NUM_OF_LINES_RATE = 6;
        //      Numbers of horizontal(X) and vertical(Y) cells
        public const uint NUM_OF_X_CELLS = 12;
        public const uint NUM_OF_Y_CELLS = 20;
        //      Size of each cells (blocks)
        public const double SIZE_OF_BLOCK = 30;
        //      Position of the sideboard (for the next tetromino)
        public const double SIDEBOARD_X_POSITION = 500;
        public const double SIDEBOARD_Y_POSITION = 100;

        // -------------------------------------------------------------------------
        // SCENES ID
        public const int HOME_SCENE = 0;
        public const int SETTING_SCENE = 1;
        public const int INSTRUCTION_SCENE = 2;
        public const int GAME_SCENE = 3;
        public const int ENDING_SCENE = 4;

        // -------------------------------------------------------------------------
        // TETROMINOS ID
        public const int TETROMINO_TYPE_I = 0;
        public const int TETROMINO_TYPE_J = 1;
        public const int TETROMINO_TYPE_L = 2;
        public const int TETROMINO_TYPE_O = 3;
        public const int TETROMINO_TYPE_S = 4;
        public const int TETROMINO_TYPE_T = 5;
        public const int TETROMINO_TYPE_Z = 6;
    }
}
