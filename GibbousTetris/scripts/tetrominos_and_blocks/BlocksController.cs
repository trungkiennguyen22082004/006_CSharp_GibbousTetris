using System;
using System.Reflection;

namespace GibbousTetris
{
    public class BlocksController
    {
        private static BlocksController? _instance;
        public static BlocksController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BlocksController();
                }

                return _instance;
            }
        }

        private int[, ] _blocksIndex;
        private List<Block> _blocksDisplay;

        private BlocksController() : this(12, 20)
        {
        }
        private BlocksController(int numOfXBlocks, int numOfYBlocks) 
        {
            _blocksIndex = new int[numOfYBlocks, numOfXBlocks];
            _blocksDisplay = new List<Block>();
        }

        // All terminated blocks presented in an integer 2D-array
        public int[, ] BlocksIndex
        { 
            get
            {
                for (int y = 0; y < _blocksIndex.GetLength(0); y++)
                {
                    for (int x = 0; x < _blocksIndex.GetLength(1); x++)
                    {
                        _blocksIndex[y, x] = 0;
                    }
                }

                foreach (Block block in _blocksDisplay)
                {
                    _blocksIndex[block.YIndex, block.XIndex] = 1;
                }

                return _blocksIndex;
            }
        }

        // All terminated blocks
        private List<Block> BlocksDisplay
        {
            get => _blocksDisplay;
        }

        // Add the new terminating blocks to the list of terminated blocks
        public void TerminateTetromino(List<Block> tetromino)
        {
            for (int i = 0; i < 4; i++)
            {
                this.BlocksDisplay.Add(tetromino[i]);
            }
        }

        public void Update()
        {
            // DELETE THE LINES THAT ARE COMPLETED/FILLED
            //     List of Y-indexes of line filled by blocks
            List<int> listOfFilledLine = new List<int>();

            for (int yIndex = 0; yIndex < _blocksIndex.GetLength(0); yIndex++)
            {
                // Number of blocks in this line
                int numOfBlocks = 0;

                foreach (Block block in _blocksDisplay)
                {
                    if (block.YIndex == yIndex)
                    {
                        numOfBlocks++;
                    }
                }

                if (numOfBlocks == _blocksIndex.GetLength(1)) 
                {
                    listOfFilledLine.Add(yIndex);
                }
            }

            listOfFilledLine.Sort();

            for (int yIndex = 0; yIndex < listOfFilledLine.Count; yIndex++)
            {
                // Delete process
                for (int index = 0; index < _blocksDisplay.Count; index++)
                {
                    if (_blocksDisplay[index].YIndex == listOfFilledLine[yIndex])
                    {
                        _blocksDisplay.Remove(_blocksDisplay[index]);
                        index--;
                    }
                }

                // Terminated blocks that are above each removed line will be automatically moved down by 1
                foreach (Block block in _blocksDisplay)
                {
                    if (block.YIndex < listOfFilledLine[yIndex])
                    {
                        block.MoveDown();
                    }
                }
            }
        }
        public void Draw()
        {
            foreach (Block block in _blocksDisplay)
            {
                block.Draw();
            }
        }
        public void Reset()
        {
            _instance = new BlocksController();
        }
    }
}