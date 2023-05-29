using SplashKitSDK;

namespace GibbousTetris
{
    public class Block
    {
        private BlocksController _blocksCtrl;

        private Color _color;
        private Point2DIndex _pointIndexes;

        public Block(BlocksController blocksCtrl) : this(blocksCtrl, Color.Black, new Point2DIndex { XIndex = 11, YIndex = 19})
        {
        }
        public Block(BlocksController blocksCtrl, Color blockColor, Point2DIndex pointIndexes)
        {
            _blocksCtrl = blocksCtrl;

            _color = blockColor;
            _pointIndexes = pointIndexes;
        }

        private Rectangle BorderRectangle
        {
            get => new Rectangle()
            {
                X = this.Point.X,
                Y = this.Point.Y,
                Width = Constants.SIZE_OF_BLOCK,
                Height = Constants.SIZE_OF_BLOCK
            };
        }
        private Rectangle InnerRectangle
        {
            get => new Rectangle()
            {
                X = this.Point.X + (Constants.SIZE_OF_BLOCK / 10),
                Y = this.Point.Y + (Constants.SIZE_OF_BLOCK / 10),
                Width = Constants.SIZE_OF_BLOCK - (Constants.SIZE_OF_BLOCK / 5),
                Height = Constants.SIZE_OF_BLOCK - (Constants.SIZE_OF_BLOCK / 5)
            };
        }
        public Point2D Point
        {
            get => new Point2D()
            {
                X = 50 + (this.PointIndexes.XIndex * Constants.SIZE_OF_BLOCK),
                Y = 100 + (this.PointIndexes.YIndex * Constants.SIZE_OF_BLOCK)
            };
        }
        public Point2DIndex PointIndexes
        {
            get => _pointIndexes;
            set => _pointIndexes = value;
        }

        public void Draw()
        {
            // Draw border
            SplashKit.FillRectangle(Color.Black, this.BorderRectangle);

            // Draw inner rectagle
            SplashKit.FillRectangle(_color, this.InnerRectangle);
        }

        public void GetIndexes(Point2DIndex pointIndexes)
        {
            _pointIndexes = pointIndexes;
        }

        public bool CanMoveLeft
        {
            get
            {
                if (this.PointIndexes.XIndex > 0) 
                {
                    return (_blocksCtrl.BlocksIndex[this.PointIndexes.YIndex, this.PointIndexes.XIndex - 1] == 0);
                }
                return false;
            }
        }
        public bool CanMoveRight
        {
            get
            {
                if (this.PointIndexes.XIndex < (_blocksCtrl.BlocksIndex.GetLength(1) - 1))
                {
                    return (_blocksCtrl.BlocksIndex[this.PointIndexes.YIndex, this.PointIndexes.XIndex + 1] == 0);
                }
                return false;
            }
        }
        public bool CanMoveDown
        {
            get
            {
                if (this.PointIndexes.YIndex < (_blocksCtrl.BlocksIndex.GetLength(0) - 1))
                {
                    return (_blocksCtrl.BlocksIndex[this.PointIndexes.YIndex + 1, this.PointIndexes.XIndex] == 0);
                }
                return false;
            }
        }

        public void MoveDown()
        {
            this.PointIndexes = new Point2DIndex()
            {
                XIndex = this.PointIndexes.XIndex,
                YIndex = this.PointIndexes.YIndex + 1
            };
        }

        public void Save(StreamWriter writer) 
        {
            writer.WriteLine("Block");
            writer.WriteColor(_color);
            writer.WriteLine(_pointIndexes.XIndex);
            writer.WriteLine(_pointIndexes.YIndex);
        }
        public void Load(StreamReader reader) 
        {
            string? kind = reader.ReadLine();
            if (kind == "Block")
            {
                _color = reader.ReadColor();

                int xIndex = reader.ReadInteger();
                int yIndex = reader.ReadInteger();
                _pointIndexes = new Point2DIndex()
                {
                    XIndex = xIndex,
                    YIndex = yIndex
                };
            }
            else
            {
                throw new InvalidDataException("Unknown block kind: " + kind);
            }
        }
    }
}