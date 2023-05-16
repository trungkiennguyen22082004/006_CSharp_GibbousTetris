using SplashKitSDK;

namespace GibbousTetris
{

    public abstract class Tetromino
    {
        private Color _tetrominoColor;
        private Angle _tetrominoAngle;
        private int _xIndex, _yIndex;

        private List<Block> _theTetromino;

        public enum Angle
        {
            Up,
            Right,
            Down,
            Left
        }

        public Tetromino(int xIndex, int yIndex, Color color, Angle originAngle)
        {
            _xIndex = xIndex;
            _yIndex = yIndex;

            _tetrominoColor = color;
            _tetrominoAngle = originAngle;

            _theTetromino = new List<Block>();

            this.AddBlocks();
            this.Update();
        }

        protected Angle TetrominoAngle
        {
            get => _tetrominoAngle;
            set => _tetrominoAngle = value;
        }
        protected int XIndex
        {
            get => _xIndex;
        }
        protected int YIndex
        {
            get => _yIndex;
        }

        public List<Block> TheTetromino
        {
            get => _theTetromino;
        }
        private bool CanMoveLeft
        {
            get
            {
                for (int index = 0; index < 4; index++)
                {
                    if (!this.TheTetromino[index].CanMoveLeft)
                    {
                        return (false);
                    }
                }
                return (true);
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
                        return (false);
                    }
                }
                return (true);
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
                        return (false);
                    }
                }
                return (true);
            }
        }

        public void SetPosition(int xIndex, int yIndex)
        {
            _xIndex = xIndex;
            _yIndex = yIndex;
        }
        private void AddBlocks()
        {
            this.TheTetromino.Add(new Block(_tetrominoColor));
            this.TheTetromino.Add(new Block(_tetrominoColor));
            this.TheTetromino.Add(new Block(_tetrominoColor));
            this.TheTetromino.Add(new Block(_tetrominoColor));
        }
        public abstract void Update();
        public void Draw()
        {
            this.TheTetromino[0].Draw();
            this.TheTetromino[1].Draw();
            this.TheTetromino[2].Draw();
            this.TheTetromino[3].Draw();
        }
        public abstract void Rotate();
        public void MoveLeft()
        {
            if (this.CanMoveLeft)
            {
                _xIndex--;
            }
        }
        public void MoveRight()
        {
            if (this.CanMoveRight)
            {
                _xIndex++;
            }
        }
        public void MoveDown()
        {
            if (this.CanMoveDown)
            {
                _yIndex++;
            }
        }
        public void MoveToBottom()
        {
            while (this.CanMoveDown)
            {
                _yIndex++;
                this.Update();
            }
        }
    }
}