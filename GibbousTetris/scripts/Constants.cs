using SplashKitSDK;

namespace GibbousTetris
{
    public static class Constants
    {
        // LOCATIONS
        //      Project location, change this to match the code if you have just downloaded
        public const string PROJECT_LOCATION = "D:/MOON006 - CSharp - GibbousTetris/";

        public const string MEDIA_FOLDER_LOCATION = PROJECT_LOCATION + "GibbousTetris/media/";
        public const string FONT_FOLDER_LOCATION = PROJECT_LOCATION + "GibbousTetris/font/";
        public const string AUDIO_TEXT_FOLRDER_LOCATION = PROJECT_LOCATION + "GibbousTetris/text/audio.txt";
        public const string KEYS_TEXT_FOLRDER_LOCATION = PROJECT_LOCATION + "GibbousTetris/text/keys.txt";
        public const string GAMEPLAY_TEXT_FOLRDER_LOCATION = PROJECT_LOCATION + "GibbousTetris/text/gameplay.txt";
        public const string RESULT_TEXT_FOLRDER_LOCATION = PROJECT_LOCATION + "GibbousTetris/text/results.txt";

        // -------------------------------------------------------------------------
        // FONTS
        public static Font LEGEND_BOLD = new Font("Legend Bold", Constants.FONT_FOLDER_LOCATION + "legend_bold.otf");
        public static Font SPANISH_FAITH = new Font("Spanish Faith", Constants.FONT_FOLDER_LOCATION + "spanish_faith.ttf");
        public static Font BROWN_SUGAR = new Font("Brown Sugar", Constants.FONT_FOLDER_LOCATION + "brown_sugar.otf");
        public static Font MOTHERCODE = new Font("Mothercode", Constants.FONT_FOLDER_LOCATION + "mothercode.otf");
        public static Font GERALD = new Font("Gerald", Constants.FONT_FOLDER_LOCATION + "gerald.otf");
        public static Font GALACTIC_ADVENTURE = new Font("Galactic Adventure", Constants.FONT_FOLDER_LOCATION + "galactic_adventure.otf");

        // -------------------------------------------------------------------------
        // WINDOW
        public const int WINDOW_WIDTH = 800;
        public const int WINDOW_HEIGHT = 800;

        // -------------------------------------------------------------------------
        // GAMEBOARD'S CONSTANTS
        //      Size of each cells (blocks)
        public const double SIZE_OF_BLOCK = 30;
        //      Background Colors mathching with the tetromino
        public static Color[] GAME_BACKGROUND_COLORS = new Color[] { Color.PeachPuff, Color.LightBlue, Color.BlueViolet, Color.Pink, Color.LawnGreen, Color.LightYellow, Color.LightCyan };
        public static Color GameBackgroundColor(Color color)
        {
            for (int i = 0; i < TETROMINO_COLORS.Length; i++) 
            {
                if ((color.R == TETROMINO_COLORS[i].R) && (color.G == TETROMINO_COLORS[i].G) && (color.B == TETROMINO_COLORS[i].B))
                {
                    return GAME_BACKGROUND_COLORS[i];
                }
            }
            return Color.White;
        }

        // -------------------------------------------------------------------------
        // SCENES ID
        public const int INTRO_SCENE = -1;
        public const int HOME_SCENE = 0;
        public const int SETTING_SCENE = 1;
        public const int INSTRUCTION_SCENE = 2;
        public const int GAME_SCENE = 3;
        public const int ENDING_SCENE = 4;
        public const int RESULT_SCENE = 5;

        // -------------------------------------------------------------------------
        // TETROMINOS
        //      ID
        public const int TETROMINO_TYPE_I = 0;
        public const int TETROMINO_TYPE_J = 1;
        public const int TETROMINO_TYPE_L = 2;
        public const int TETROMINO_TYPE_O = 3;
        public const int TETROMINO_TYPE_S = 4;
        public const int TETROMINO_TYPE_T = 5;
        public const int TETROMINO_TYPE_Z = 6;
        //      Colors
        public static Color[] TETROMINO_COLORS = new Color[] { Color.Orange, Color.Blue, Color.Purple, Color.Red, Color.Lime, Color.Yellow, Color.Cyan };
        //      Default angles
        public static Tetromino.Angle[] TETROMINO_ANGLES = new Tetromino.Angle[] { Tetromino.Angle.Left, Tetromino.Angle.Left, Tetromino.Angle.Right, Tetromino.Angle.Left, Tetromino.Angle.Up, Tetromino.Angle.Up, Tetromino.Angle.Up };
    }
}
