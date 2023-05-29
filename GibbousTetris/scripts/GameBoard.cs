using SplashKitSDK;

namespace GibbousTetris
{
    public class GameBoard
    {
        private Color _borderColor, _backgroundColor, _decoratedColor;
        private Point2D _boardPos;
        private Point2DIndex _numsOfCells;

        public GameBoard() : this(Color.RGBColor(128, 128, 128), Color.White, Color.RGBColor(217, 217, 217), new Point2D() { X = 50, Y = 100 }, new Point2DIndex() { XIndex = 12, YIndex = 20 } )
        {
        }
        public GameBoard(Point2D boardPos, Point2DIndex numsOfCells) : this(Color.RGBColor(128, 128, 128), Color.White, Color.RGBColor(217, 217, 217), boardPos, numsOfCells)
        {
        }
        public GameBoard(Color borderColor, Color backgroudColor, Color decoratedColor, Point2D boardPos, Point2DIndex numsOfCells) 
        {
            // Colors of the gameboard
            _borderColor = borderColor;
            _backgroundColor = backgroudColor;
            _decoratedColor = decoratedColor;

            _boardPos = boardPos;
            _numsOfCells = numsOfCells;
        }

        public void Draw()
        {
            // Draw border of the gameboard
            SplashKit.FillRectangle(_borderColor, _boardPos.X - 5, _boardPos.Y - 5, (Constants.SIZE_OF_BLOCK * _numsOfCells.XIndex) + 10, (Constants.SIZE_OF_BLOCK * _numsOfCells.YIndex) + 10);

            // Draw gameboard background
            SplashKit.FillRectangle(_backgroundColor, _boardPos.X, _boardPos.Y, (Constants.SIZE_OF_BLOCK * _numsOfCells.XIndex), (Constants.SIZE_OF_BLOCK * _numsOfCells.YIndex));

            // Draw decorative lines on the gameboard
            for (int i = 1; i <= (_numsOfCells.XIndex * (Constants.SIZE_OF_BLOCK / 6)); i++) 
            {
                SplashKit.DrawLine(_decoratedColor, _boardPos.X + i * 6, _boardPos.Y, _boardPos.X, _boardPos.Y + i * (6 * ((double)_numsOfCells.YIndex / _numsOfCells.XIndex)));
                SplashKit.DrawLine(_decoratedColor, _boardPos.X + i * 6, _boardPos.Y + (Constants.SIZE_OF_BLOCK * _numsOfCells.YIndex), _boardPos.X + (Constants.SIZE_OF_BLOCK * _numsOfCells.XIndex), _boardPos.Y + i * (6 * ((double)_numsOfCells.YIndex / _numsOfCells.XIndex)));
            }
        }
    }
}