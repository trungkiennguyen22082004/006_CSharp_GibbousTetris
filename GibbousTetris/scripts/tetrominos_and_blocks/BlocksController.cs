using SplashKitSDK;

namespace GibbousTetris
{
    public class BlocksController
    {
        private SoundEffect _collectingSE;

        private List<Block> _terminatedBlocks;
        private int _numOfRows, _numOfCols;

        private uint _scoreIndex;

        public BlocksController() : this(12, 20)
        {
        }
        public BlocksController(int numOfXBlocks, int numOfYBlocks)
        {
            _collectingSE = new SoundEffect("Collecting", Constants.MEDIA_FOLDER_LOCATION + "collecting.wav");

            _numOfCols = numOfXBlocks;
            _numOfRows = numOfYBlocks;
            _terminatedBlocks = new List<Block>();

            _scoreIndex = 0;
        }

        // All terminated blocks presented in an integer 2D-array
        public int[,] BlocksIndex
        {
            get
            {
                int[, ] blocksIndex = new int[_numOfRows, _numOfCols];
                for (int y = 0; y < _numOfRows; y++)
                {
                    for (int x = 0; x < _numOfCols; x++)
                    {
                        blocksIndex[y, x] = 0;
                    }
                }

                foreach (Block block in _terminatedBlocks)
                {
                    blocksIndex[block.PointIndexes.YIndex, block.PointIndexes.XIndex] = 1;
                }

                return blocksIndex;
            }
        }

        // This is raw score calculated without considering the Level Type and Total Gameplay Time from the GameScene
        public uint ScoreIndex
        {
            get => _scoreIndex;
        }

        // Add the new terminating blocks to the list of terminated blocks
        public void TerminateTetromino(List<Block> tetromino)
        {
            for (int i = 0; i < 4; i++)
            {
                _terminatedBlocks.Add(tetromino[i]);
            }
        }

        public void Update()
        {
            for (int row = _numOfRows - 1; row >= 0; row--)
            {
                bool allOnes = true;

                for (int col = 0; col < _numOfCols; col++)
                {
                    if (this.BlocksIndex[row, col] != 1)
                    {
                        allOnes = false;
                        break;
                    }
                }

                if (allOnes)
                {
                    // Remove that line
                    _terminatedBlocks.RemoveAll(block => block.PointIndexes.YIndex == row);

                    // Gain score
                    _scoreIndex++;

                    // Shift all blocks above it down by one position
                    foreach (Block block in _terminatedBlocks)
                    {
                        if (block.PointIndexes.YIndex < row)
                        {
                            block.MoveDown();
                        }
                    }

                    _collectingSE.Play(1, AudioManager.Instance.SoundVolume);
                }
            }
        }
        public void Draw()
        {
            foreach (Block block in _terminatedBlocks)
            {
                block.Draw();
            }
        }

        public void Save(StreamWriter writer)
        {
            writer.WriteLine("Blocks Controller");
            writer.WriteLine(_numOfCols);
            writer.WriteLine(_numOfRows);
            writer.WriteLine(_scoreIndex);
            writer.WriteLine(_terminatedBlocks.Count);
            foreach (Block block in _terminatedBlocks)
            {
                block.Save(writer);
            }
        }
        public void Load(StreamReader reader)
        {
            string? kind = reader.ReadLine();
            if (kind == "Blocks Controller")
            {
                _numOfCols = reader.ReadInteger();
                _numOfRows = reader.ReadInteger();
                _scoreIndex = (uint)reader.ReadInteger();
                int blocksCount = reader.ReadInteger();

                _terminatedBlocks = new List<Block>();
                for (int i = 0; i < blocksCount; i++) 
                {
                    _terminatedBlocks.Add(new Block(this));
                    _terminatedBlocks[i].Load(reader);
                }
            }
            else
            {
                throw new InvalidDataException("Unknown blocks controller kind: " + kind);
            }
        }
    }
}