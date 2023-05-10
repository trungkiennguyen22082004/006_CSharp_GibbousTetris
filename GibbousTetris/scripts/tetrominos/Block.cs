using SplashKitSDK;

namespace GibbousTetris
{
    public class Block
    {
        private Color _borderColor;
        private Color _color;
        private int _xIndex, _yIndex;

        public Block(Color blockColor) : this(blockColor, -100, -100)
        {
        }
        public Block(Color blockColor, int xIndex, int yIndex) : this(Color.Black, blockColor, xIndex, yIndex)
        {
        }
        public Block(Color borderColor, Color blockColor, int xIndex, int yIndex)
        {
            _borderColor = borderColor;
            _color = blockColor;

            this.GetIndexes(xIndex, yIndex);
        }

        private Rectangle BorderRectangle
        {
            get => new Rectangle()
            {
                X = this.X,
                Y = this.Y,
                Width = Constants.SIZE_OF_BLOCK,
                Height = Constants.SIZE_OF_BLOCK
            };
        }
        private Rectangle InnerRectangle
        {
            get => new Rectangle()
            {
                X = this.X + (Constants.SIZE_OF_BLOCK / 10),
                Y = this.Y + (Constants.SIZE_OF_BLOCK / 10),
                Width = Constants.SIZE_OF_BLOCK - (Constants.SIZE_OF_BLOCK / 5),
                Height = Constants.SIZE_OF_BLOCK - (Constants.SIZE_OF_BLOCK / 5)
            };
        }
        public double X
        {
            get => Constants.BOARD_X_POSITION + (_xIndex * Constants.SIZE_OF_BLOCK);
        }
        public double Y
        {
            get => Constants.BOARD_Y_POSITION + (_yIndex * Constants.SIZE_OF_BLOCK);
        }
        public int XIndex
        {
            get => _xIndex;
        }
        public int YIndex
        {
            get => _yIndex;
        }

        public void Draw()
        {
            // Draw border
            SplashKit.FillRectangle(_borderColor, this.BorderRectangle);

            // Draw inner rectagle
            SplashKit.FillRectangle(_color, this.InnerRectangle);
        }

        public void GetIndexes(int xIndex, int yIndex)
        {
            _xIndex = xIndex;
            _yIndex = yIndex;
        }

        public bool CanMoveLeft
        {
            get
            {
                if (this.XIndex > 0) 
                {
                    return (BlocksController.Instance.BlocksIndex[this.YIndex, this.XIndex - 1] == 0);
                }
                return false;
            }
        }
        public bool CanMoveRight
        {
            get
            {
                if (this.XIndex < (BlocksController.Instance.BlocksIndex.GetLength(1) - 1))
                {
                    return (BlocksController.Instance.BlocksIndex[this.YIndex, this.XIndex + 1] == 0);
                }
                return false;
            }
        }
        public bool CanMoveDown
        {
            get
            {
                if (this.YIndex < (BlocksController.Instance.BlocksIndex.GetLength(0) - 1))
                {
                    return (BlocksController.Instance.BlocksIndex[this.YIndex + 1, this.XIndex] == 0);
                }
                return false;
            }
        }
        public void MoveDown()
        {
            _yIndex++;
        }
    }
}