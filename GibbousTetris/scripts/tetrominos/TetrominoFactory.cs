namespace GibbousTetris
{
    public class TetrominoFactory
    {
        // Create a random tetromino with default postion
        public static Tetromino CreateTetromino()
        {
            return CreateTetromino((new Random()).Next(7));
        }
        // Create a specific tetromino with default postion
        public static Tetromino CreateTetromino(int type) 
        {
            return CreateTetromino(type, 5, 0);
        }
        // Create a random tetromino with custom postion
        public static Tetromino CreateTetromino(int xIndex, int yIndex)
        {
            return CreateTetromino((new Random()).Next(7), xIndex, yIndex);
        }
        // Create a specific tetromino with custom postion
        public static Tetromino CreateTetromino(int type, int xIndex, int yIndex)
        {
            switch (type)
            {
                case Constants.TETROMINO_TYPE_I:
                {
                    return new TetrominoTypeI(xIndex, yIndex);
                }
                case Constants.TETROMINO_TYPE_J:
                {
                    return new TetrominoTypeJ(xIndex, yIndex);
                }
                case Constants.TETROMINO_TYPE_L:
                {
                    return new TetrominoTypeL(xIndex, yIndex);
                }
                case Constants.TETROMINO_TYPE_O:
                {
                    return new TetrominoTypeO(xIndex, yIndex);
                }
                case Constants.TETROMINO_TYPE_S:
                {
                    return new TetrominoTypeS(xIndex, yIndex);
                }
                case Constants.TETROMINO_TYPE_T:
                {
                    return new TetrominoTypeT(xIndex, yIndex);
                }
                case Constants.TETROMINO_TYPE_Z:
                {
                    return new TetrominoTypeZ(xIndex, yIndex);
                }
                default:
                {
                    throw new ArgumentException("Invalid tetromino type!");
                }
            }
        }
    }
}