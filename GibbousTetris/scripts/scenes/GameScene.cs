using SplashKitSDK;
using Timer = SplashKitSDK.Timer;

namespace GibbousTetris
{
    public class GameScene : Scene
    {
        private Button _homeButton;
        private Timer _timer;

        // Duration of time in seconds for each Move Down
        private double _timeOfSteps;

        private Tetromino _currentTetromino;
        private Tetromino _nextTetromino;

        private bool _wasMovedDown;

        public GameScene() : this(1)
        { 
        }
        public GameScene(double timeOfSteps)
        {
            BlocksController.Instance.Reset();
            _timeOfSteps = timeOfSteps;

            _homeButton = new ButtonCircle(Color.Gray, Color.Black, 50, 50, 20, "Home", Constants.MEDIA_FOLDER_LOCATION + "home.png");

            _timer = new Timer("Game Timer");
            _timer.Start();
            _wasMovedDown = false;

            _currentTetromino = TetrominoFactory.CreateTetromino();
            _nextTetromino = TetrominoFactory.CreateTetromino(17, 1);
        }

        public override void Update()
        {
            if (_homeButton.IsClicked)
            {
                GameExecuter.Instance.RequestChangeScene(Constants.HOME_SCENE);
            }

            BlocksController.Instance.Update();
            _currentTetromino.Update();

            if ((_timer.Ticks % (1000 * _timeOfSteps)) <= (1000 * _timeOfSteps - 50))
            {
                _wasMovedDown = false;

                if (SplashKit.KeyTyped(KeyCode.UpKey))
                {
                    _currentTetromino.Rotate();
                }
                else if (SplashKit.KeyTyped(KeyCode.LeftKey)) 
                {
                    _currentTetromino.MoveLeft();
                }
                else if (SplashKit.KeyTyped(KeyCode.RightKey))
                {
                    _currentTetromino.MoveRight();
                }
                else if (SplashKit.KeyTyped(KeyCode.DownKey))
                {
                    _currentTetromino.MoveDown();
                }
                else if (SplashKit.KeyTyped(KeyCode.SpaceKey)) 
                {
                    _currentTetromino.MoveToBottom();
                }
            }
            else
            {
                if (!_wasMovedDown)
                {
                    _wasMovedDown = true;
                    _currentTetromino.MoveDown();
                }
            }

            // Process of terminating current tetromino and push next one into the game board
            if (!_currentTetromino.CanMoveDown)
            {
                BlocksController.Instance.TerminateTetromino(_currentTetromino.TheTetromino);
                _currentTetromino = _nextTetromino;
                _currentTetromino.SetPosition(5, 0);
                _nextTetromino = TetrominoFactory.CreateTetromino(17, 1);
            }
        }
        public override void Draw()
        {
            _homeButton.Draw();

            GameBoard.Instance.Draw();
            BlocksController.Instance.Draw();

            // Draw current and next tetrominos
            _currentTetromino.Draw();
            // Highlight the center point of the current tetromino
            SplashKit.FillCircle(Color.White, _currentTetromino.TheTetromino[0].X + (Constants.SIZE_OF_BLOCK / 2), _currentTetromino.TheTetromino[0].Y + (Constants.SIZE_OF_BLOCK / 2), 2);
            _nextTetromino.Draw();
          
            SplashKit.DrawText((_timer.Ticks / 1000).ToString(), Color.Black, 200, 20);
        }
    }
}