using SplashKitSDK;

namespace GibbousTetris
{
    public class TetrominoTypeZ : Tetromino
    {
        public TetrominoTypeZ(int xIndex, int yIndex) : base(xIndex, yIndex, Color.Cyan, Angle.Up)
        {
        }

        public override void Update()
        {
            this.TheTetromino[0].GetIndexes(this.XIndex, this.YIndex);

            switch (this.TetrominoAngle)
            {
                case (Angle.Left):
                /*
                       1
                    1* 1
                    1
                */
                {
                    this.TheTetromino[1].GetIndexes(this.XIndex, this.YIndex + 1);
                    this.TheTetromino[2].GetIndexes(this.XIndex + 1, this.YIndex);
                    this.TheTetromino[3].GetIndexes(this.XIndex + 1, this.YIndex - 1);
                    break;
                }
                case (Angle.Up):
                /*
                    1 1* 
                      1 1
                */
                {
                    this.TheTetromino[1].GetIndexes(this.XIndex - 1, this.YIndex);
                    this.TheTetromino[2].GetIndexes(this.XIndex, this.YIndex + 1);
                    this.TheTetromino[3].GetIndexes(this.XIndex + 1, this.YIndex + 1);
                    break;
                }
                case (Angle.Right):
                /*
                      1
                    1 1*
                    1
                */
                {
                    this.TheTetromino[1].GetIndexes(this.XIndex, this.YIndex - 1);
                    this.TheTetromino[2].GetIndexes(this.XIndex - 1, this.YIndex);
                    this.TheTetromino[3].GetIndexes(this.XIndex - 1, this.YIndex + 1);
                    break;
                }
                case (Angle.Down):
                /*
                    1 1
                      1* 1
                */
                {
                    this.TheTetromino[1].GetIndexes(this.XIndex + 1, this.YIndex);
                    this.TheTetromino[2].GetIndexes(this.XIndex, this.YIndex - 1);
                    this.TheTetromino[3].GetIndexes(this.XIndex - 1, this.YIndex - 1);
                    break;
                }
            }
        }
        public override void Rotate()
        {
            switch (this.TetrominoAngle)
            {
                case Angle.Left:
                {
                    if ((this.XIndex > 0))
                    {
                        if ((BlocksController.Instance.BlocksIndex[YIndex, XIndex - 1] == 0) && (BlocksController.Instance.BlocksIndex[YIndex + 1, XIndex + 1] == 0))
                        {    
                            this.TetrominoAngle = Angle.Up;
                        }
                    }
                    break;
                }
                case Angle.Up:
                {
                    if (this.YIndex > 0)
                    {
                        if ((BlocksController.Instance.BlocksIndex[YIndex - 1, XIndex] == 0) && (BlocksController.Instance.BlocksIndex[YIndex + 1, XIndex - 1] == 0))
                        { 
                            this.TetrominoAngle = Angle.Right;
                        }
                    }
                    break;
                }
                case Angle.Right:
                {
                    if (this.XIndex < BlocksController.Instance.BlocksIndex.GetLength(1) - 1)
                    {
                        if ((BlocksController.Instance.BlocksIndex[YIndex, XIndex + 1] == 0) && (BlocksController.Instance.BlocksIndex[YIndex - 1, XIndex - 1] == 0))
                        { 
                            this.TetrominoAngle = Angle.Down;
                        }
                    }
                    break;
                }
                case Angle.Down:
                {
                    if (this.YIndex < BlocksController.Instance.BlocksIndex.GetLength(0) - 1)
                    {
                        if ((BlocksController.Instance.BlocksIndex[YIndex + 1, XIndex] == 0) && (BlocksController.Instance.BlocksIndex[YIndex - 1, XIndex + 1] == 0))
                        { 
                            this.TetrominoAngle = Angle.Left;
                        }
                    }
                    break;
                }
            }
        }
    }
}
