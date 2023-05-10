using SplashKitSDK;

namespace GibbousTetris
{
    public class GameBoard
    {
        private static GameBoard? _instance;
        public static GameBoard Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameBoard();
                }

                return _instance;
            }
        }

        private Color _borderColor;
        private Color _backgroundColor;
        private Color _boardColor;

        private GameBoard() : this(Color.RGBColor(128, 128, 128), Color.White, Color.RGBColor(217, 217, 217))
        { 
        }
        private GameBoard(Color borderColor, Color backgroudColor, Color boardColor) 
        {
            // Colors of the gameboard
            _borderColor = borderColor;
            _backgroundColor = backgroudColor;
            _boardColor = boardColor;
        }

        public void Draw()
        {
            // Draw border of the gameboard
            SplashKit.FillRectangle(_borderColor, Constants.BOARD_X_POSITION - 5, Constants.BOARD_Y_POSITION - 5, (Constants.SIZE_OF_BLOCK * Constants.NUM_OF_X_CELLS) + 10, (Constants.SIZE_OF_BLOCK * Constants.NUM_OF_Y_CELLS) + 10);

            // Draw gameboard background
            SplashKit.FillRectangle(_backgroundColor, Constants.BOARD_X_POSITION, Constants.BOARD_Y_POSITION, (Constants.SIZE_OF_BLOCK * Constants.NUM_OF_X_CELLS), (Constants.SIZE_OF_BLOCK * Constants.NUM_OF_Y_CELLS));

            // Draw decorative lines on the gameboard
            for (int i = 1; i <= (Constants.NUM_OF_X_CELLS * (Constants.SIZE_OF_BLOCK / Constants.NUM_OF_LINES_RATE)); i++) 
            {
                SplashKit.DrawLine(_boardColor, Constants.BOARD_X_POSITION + i * Constants.NUM_OF_LINES_RATE, Constants.BOARD_Y_POSITION, Constants.BOARD_X_POSITION, Constants.BOARD_Y_POSITION + i * (Constants.NUM_OF_LINES_RATE * ((double)Constants.NUM_OF_Y_CELLS / Constants.NUM_OF_X_CELLS)));
                SplashKit.DrawLine(_boardColor, Constants.BOARD_X_POSITION + i * Constants.NUM_OF_LINES_RATE, Constants.BOARD_Y_POSITION + (Constants.SIZE_OF_BLOCK * Constants.NUM_OF_Y_CELLS), Constants.BOARD_X_POSITION + (Constants.SIZE_OF_BLOCK * Constants.NUM_OF_X_CELLS), Constants.BOARD_Y_POSITION + i * (Constants.NUM_OF_LINES_RATE * ((double)Constants.NUM_OF_Y_CELLS / Constants.NUM_OF_X_CELLS)));
            }

            // Draw sideboard for the next tetromino
            SplashKit.DrawText("NEXT", Color.Black, 575, 50);

            SplashKit.FillRectangle(_borderColor, Constants.SIDEBOARD_X_POSITION - 5, Constants.SIDEBOARD_Y_POSITION - 5, Constants.SIZE_OF_BLOCK * 6 + 10, Constants.SIZE_OF_BLOCK * 4 + 10);
            SplashKit.FillRectangle(_backgroundColor, Constants.SIDEBOARD_X_POSITION, Constants.SIDEBOARD_Y_POSITION, Constants.SIZE_OF_BLOCK * 6, Constants.SIZE_OF_BLOCK * 4);
        }
    }
}