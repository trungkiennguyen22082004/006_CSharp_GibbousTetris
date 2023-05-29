using SplashKitSDK;

namespace GibbousTetris
{
    public class Tetromino
    {
        private SoundEffect _updateTetrominoSE;

        private int _id;
        private Angle _tetrominoAngle;
        private Point2DIndex _pointIndexes;

        private List<Block> _theTetromino;
        private BlocksController _blocksCtrl;

        public enum Angle
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }

        public Tetromino(BlocksController blocksCtrl) : this(blocksCtrl, 5, 0)
        {
            _blocksCtrl = blocksCtrl;
        }
        public Tetromino(BlocksController blocksCtrl, int xIndex, int yIndex) : this(blocksCtrl, new Random().Next(7), xIndex, yIndex)
        {
        }
        public Tetromino(BlocksController blocksCtrl, int id, int xIndex, int yIndex) : this(blocksCtrl, id, Constants.TETROMINO_ANGLES[id], xIndex, yIndex)
        {
        }
        public Tetromino(BlocksController blocksCtrl, int id, Angle tetrominoAngle, int xIndex, int yIndex)
        {
            _updateTetrominoSE = new SoundEffect("Update Tetromino", Constants.MEDIA_FOLDER_LOCATION + "update_tetromino.wav");

            _blocksCtrl = blocksCtrl;
            _id = id;
            _pointIndexes = new Point2DIndex { XIndex = xIndex, YIndex = yIndex };

            _tetrominoAngle = tetrominoAngle;

            _theTetromino = new List<Block>(4);

            for (int index = 0; index < 4; index++)
            {
                this.TheTetromino.Add(new Block(_blocksCtrl, TetrominoColor, TetrominoIndexPoints(_tetrominoAngle)[index]));
            }
        }

        public List<Block> TheTetromino
        {
            get => _theTetromino;
        }
        public Color TetrominoColor
        {
            get => Constants.TETROMINO_COLORS[_id];
        }
        private bool CanMoveLeft
        {
            get
            {
                for (int index = 0; index < 4; index++)
                {
                    if (!this.TheTetromino[index].CanMoveLeft)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private bool CanMoveRight
        {
            get
            {
                for (int index = 0; index < 4; index++)
                {
                    if (!this.TheTetromino[index].CanMoveRight)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public bool CanMoveDown
        {
            get
            {
                for (int index = 0; index < 4; index++)
                {
                    if (!this.TheTetromino[index].CanMoveDown)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public void SetPosition(Point2DIndex pointIndexes)
        {
            _pointIndexes = pointIndexes;
        }
        public void Update()
        {
            for (int index = 0; index < this.TheTetromino.Count; index++)
            {
                this.TheTetromino[index].PointIndexes = TetrominoIndexPoints(_tetrominoAngle)[index];
            }
        }
        public void Draw()
        {
            foreach (Block block in this.TheTetromino)
            {
                block.Draw();
            }
        }
        public void Rotate()
        {
            Angle nextAngle = Angle.Up;

            foreach (Angle angle in Enum.GetValues(typeof(Angle)))
            {
                if (angle == _tetrominoAngle)
                {
                    if (angle != Angle.Left)
                    {
                        nextAngle = (Angle)((int)(angle) + 1);
                    }
                    else
                    {
                        nextAngle = Angle.Up;
                    }
                    break;
                }
            }

            if (this.CheckIfEmpty(nextAngle))
            {
                _tetrominoAngle = nextAngle;
                _updateTetrominoSE.Play(1, AudioManager.Instance.SoundVolume);
            }
        }
        public void MoveLeft()
        {
            if (CanMoveLeft)
            {
                _pointIndexes.XIndex--;
                _updateTetrominoSE.Play(1, AudioManager.Instance.SoundVolume);
            }
        }
        public void MoveRight()
        {
            if (CanMoveRight)
            {
                _pointIndexes.XIndex++;
                _updateTetrominoSE.Play(1, AudioManager.Instance.SoundVolume);
            }
        }
        public void MoveDown()
        {
            if (CanMoveDown)
            {
                _pointIndexes.YIndex++;
                _updateTetrominoSE.Play(1, AudioManager.Instance.SoundVolume);
            }
        }
        public void MoveToBottom()
        {
            while (CanMoveDown)
            {
                this.MoveDown();
                this.Update();
            }
        }
        // Rotate the offset clockwise: Up = 0 rotating, Right = 1 rotating, Down = 2 rotating, Up = 3 rotating
        // Use the simple formula of rotating in the Oxy 2D-coordinate system: (x, y) -> (-y, x)
        private Point2DIndex RotateIndexesClockwise(Point2DIndex indexOffset, Angle tetrominoAngle)
        {
            for (int i = 0; i < (int)tetrominoAngle; i++)
            {
                int x = indexOffset.XIndex;
                int y = indexOffset.YIndex;
                indexOffset.XIndex = -y;
                indexOffset.YIndex = x;
            }
            return indexOffset;
        }
        // There are 4 offsets, corresponding to 4 blocks, obviously the first one is (0, 0) by default
        private Point2DIndex[] GetIndexOffsets(Point2DIndex secondBlockOffset, Point2DIndex thirdBlockOffset, Point2DIndex fourthBlockOffset, Angle tetrominoAngle)
        {
            Point2DIndex firstBlockIndexOffset = new Point2DIndex() { XIndex = 0, YIndex = 0 };
            Point2DIndex secondBlockIndexOffset = RotateIndexesClockwise(secondBlockOffset, tetrominoAngle);
            Point2DIndex thirdBlockIndexOffset = RotateIndexesClockwise(thirdBlockOffset, tetrominoAngle);
            Point2DIndex fourthBlockIndexOffset = RotateIndexesClockwise(fourthBlockOffset, tetrominoAngle);

            return new Point2DIndex[] { firstBlockIndexOffset, secondBlockIndexOffset, thirdBlockIndexOffset, fourthBlockIndexOffset };
        }
        // Offset of a block is the distance from the center one to it (in indexes)
        private Point2DIndex[] TetrominoOffsets(Angle tetrominoAngle)
        {
            switch (_id)
            {
                case Constants.TETROMINO_TYPE_I:
                    return GetIndexOffsets(new Point2DIndex { XIndex = 0, YIndex = -1 }, new Point2DIndex { XIndex = 0, YIndex = +1 }, new Point2DIndex { XIndex = 0, YIndex = +2 }, tetrominoAngle);
                case Constants.TETROMINO_TYPE_J:
                    return GetIndexOffsets(new Point2DIndex { XIndex = 0, YIndex = -1 }, new Point2DIndex { XIndex = 0, YIndex = +1 }, new Point2DIndex { XIndex = -1, YIndex = +1 }, tetrominoAngle);
                case Constants.TETROMINO_TYPE_L:
                    return GetIndexOffsets(new Point2DIndex { XIndex = 0, YIndex = -1 }, new Point2DIndex { XIndex = 0, YIndex = +1 }, new Point2DIndex { XIndex = +1, YIndex = +1 }, tetrominoAngle);
                case Constants.TETROMINO_TYPE_O:
                    return GetIndexOffsets(new Point2DIndex { XIndex = 0, YIndex = +1 }, new Point2DIndex { XIndex = -1, YIndex = +1 }, new Point2DIndex { XIndex = -1, YIndex = 0 }, tetrominoAngle);
                case Constants.TETROMINO_TYPE_S:
                    return GetIndexOffsets(new Point2DIndex { XIndex = +1, YIndex = 0 }, new Point2DIndex { XIndex = 0, YIndex = +1 }, new Point2DIndex { XIndex = -1, YIndex = +1 }, tetrominoAngle);
                case Constants.TETROMINO_TYPE_T:
                    return GetIndexOffsets(new Point2DIndex { XIndex = -1, YIndex = 0 }, new Point2DIndex { XIndex = 0, YIndex = +1 }, new Point2DIndex { XIndex = +1, YIndex = 0 }, tetrominoAngle);
                case Constants.TETROMINO_TYPE_Z:
                    return GetIndexOffsets(new Point2DIndex { XIndex = -1, YIndex = 0 }, new Point2DIndex { XIndex = 0, YIndex = +1 }, new Point2DIndex { XIndex = +1, YIndex = +1 }, tetrominoAngle);
                default:
                    throw new ArgumentException("Invalid tetromino ID!");
            }
        }
        // With the offsets of the four blocks, and the ceter point (in indexes), I can calculate the coordinates of those blocks (in indexes)
        private Point2DIndex[] TetrominoIndexPoints(Angle tetrominoAngle)
        {
            return new Point2DIndex[]
            {
                new Point2DIndex { XIndex = _pointIndexes.XIndex + TetrominoOffsets(tetrominoAngle)[0].XIndex, YIndex = _pointIndexes.YIndex + TetrominoOffsets(tetrominoAngle)[0].YIndex },
                new Point2DIndex { XIndex = _pointIndexes.XIndex + TetrominoOffsets(tetrominoAngle)[1].XIndex, YIndex = _pointIndexes.YIndex + TetrominoOffsets(tetrominoAngle)[1].YIndex },
                new Point2DIndex { XIndex = _pointIndexes.XIndex + TetrominoOffsets(tetrominoAngle)[2].XIndex, YIndex = _pointIndexes.YIndex + TetrominoOffsets(tetrominoAngle)[2].YIndex },
                new Point2DIndex { XIndex = _pointIndexes.XIndex + TetrominoOffsets(tetrominoAngle)[3].XIndex, YIndex = _pointIndexes.YIndex + TetrominoOffsets(tetrominoAngle)[3].YIndex }
            };
        }
        // Check if space for blocks are empty in the case of the assumed Angle of the tetromino
        public bool CheckIfEmpty(Angle tetrominoAngle)
        {
            Point2DIndex[] point2DIndexes = TetrominoIndexPoints(tetrominoAngle);
            foreach (Point2DIndex point in point2DIndexes)
            {
                if (point.XIndex < 0 || point.YIndex < 0 || point.XIndex >= _blocksCtrl.BlocksIndex.GetLength(1) || point.YIndex >= _blocksCtrl.BlocksIndex.GetLength(0))
                {
                    return false;
                }
            }
            foreach (Point2DIndex point in point2DIndexes)
            {
                if (_blocksCtrl.BlocksIndex[point.YIndex, point.XIndex] != 0)
                {
                    return false;
                }
            }
            return true;
        }
        // Check if space for blocks are empty in the case of the current Angle of the tetromino
        public bool CheckIfEmpty()
        {
            return CheckIfEmpty(_tetrominoAngle);
        }

        public void Save(StreamWriter writer)
        {
            writer.WriteLine("Tetromino");
            writer.WriteLine(_id);
            writer.WriteLine((int)_tetrominoAngle);
            writer.WriteLine(_pointIndexes.XIndex);
            writer.WriteLine(_pointIndexes.YIndex);
            foreach (Block block in this.TheTetromino)
            {
                block.Save(writer);
            }
        }
        public void Load(StreamReader reader) 
        {
            string? kind = reader.ReadLine();
            if (kind == "Tetromino")
            {
                _id = reader.ReadInteger();
                _tetrominoAngle = (Angle)reader.ReadInteger();

                int xIndex = reader.ReadInteger();
                int yIndex = reader.ReadInteger();
                _pointIndexes = new Point2DIndex()
                {
                    XIndex = xIndex,
                    YIndex = yIndex
                };
                foreach (Block block in this.TheTetromino)
                {
                    block.Load(reader);
                }
            }
            else
            {
                throw new InvalidDataException("Unknown tetromino kind: " + kind);
            }
        }
    }
}