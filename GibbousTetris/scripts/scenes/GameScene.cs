using System;
using SplashKitSDK;
using static System.Formats.Asn1.AsnWriter;

// Clash between System.Threading.Timer and SplashKitSDK.Timer
using Timer = SplashKitSDK.Timer;

namespace GibbousTetris
{
    public enum LevelType
    {
        Hard = 1,
        Normal = 2,
        Easy = 3
    }

    public class GameScene : Scene
    {
        private Music _backgroundMusic;
        private SoundEffect _gameOverSE;

        private Timer _defaultTimer;
        private GameBoard _mainBoard, _nextBoard;
        private BlocksController _blocksCtrl;
        private Tetromino _currentTetromino, _nextTetromino;

        private bool _gameOver;
        // Duration of time in seconds for each Move Down
        private uint _timeOfSteps;
        // Duration of player time in seconds
        private uint _lastUpdateTimer, _playedTimer;

        private bool _wasMovedDown;

        public GameScene() : base()
        {
            _backgroundMusic = new Music("The seven seas", Constants.MEDIA_FOLDER_LOCATION + "the_seven_seas.mp3");
            _backgroundMusic.Play(5, AudioManager.Instance.MusicVolume);
            _gameOverSE = new SoundEffect("Game over", Constants.MEDIA_FOLDER_LOCATION + "game_over.wav");

            _blocksCtrl = new BlocksController();

            this.Buttons.Add(new ButtonCircle(Color.Gray, Color.Black, 50, 50, 20, "Home", "home.png"));
            this.Buttons.Add(new ButtonCircle(Color.DarkOliveGreen, Color.Black, 750, 50, 20, "Setting", "setting.png"));
            this.Buttons.Add(new ButtonCircle(Color.RGBAColor(1, 1, 1, 0), Color.Wheat, 520, 700, 55, "Save", "save.png"));
            this.Buttons.Add(new ButtonCircle(Color.RGBAColor(1, 1, 1, 0), Color.Wheat, 680, 700, 55, "Load", "load.png"));

            _defaultTimer = new Timer("Game Timer");
            _defaultTimer.Start();
            _lastUpdateTimer = 0;
            _playedTimer = 0;
            _timeOfSteps = (uint)GameExecuter.Instance.LevelType;
            _wasMovedDown = false;

            _mainBoard = new GameBoard();
            _nextBoard = new GameBoard(new Point2D() { X = 500, Y = 100 }, new Point2DIndex() { XIndex = 6, YIndex = 4 });
            _currentTetromino = new Tetromino(_blocksCtrl);
            _nextTetromino = new Tetromino(_blocksCtrl, 17, 1);

            _gameOver = false;
        }

        private uint Score
        {
            get => (uint)(_blocksCtrl.ScoreIndex * (3 / _timeOfSteps) * (600 / GameExecuter.Instance.TotalGameplayTime));
        }

        public override void Update()
        {
            base.Update();
            if (this.FetchButton("Save").IsClicked) 
            {
                this.Save();
            }
            else if (this.FetchButton("Load").IsClicked)
            {
                this.Load();
            }    

            if (!_gameOver)
            {
                _playedTimer += (_defaultTimer.Ticks - _lastUpdateTimer);

                _blocksCtrl.Update();
                _currentTetromino.Update();

                // Most part of each (_timeOfSteps) second, for manipulate the tetrominno
                if ((_defaultTimer.Ticks % (1000 * _timeOfSteps)) <= (1000 * _timeOfSteps - 50))
                {
                    _wasMovedDown = false;

                    if (SplashKit.KeyTyped(GameExecuter.Instance.RotateKey))
                    {
                        _currentTetromino.Rotate();
                    }
                    else if (SplashKit.KeyTyped(GameExecuter.Instance.MoveLeftKey))
                    {
                        _currentTetromino.MoveLeft();
                    }
                    else if (SplashKit.KeyTyped(GameExecuter.Instance.MoveRightKey))
                    {
                        _currentTetromino.MoveRight();
                    }
                    else if (SplashKit.KeyTyped(GameExecuter.Instance.MoveDownKey))
                    {
                        _currentTetromino.MoveDown();
                    }
                    else if (SplashKit.KeyTyped(GameExecuter.Instance.MoveToBottomKey))
                    {
                        _currentTetromino.MoveToBottom();
                    }
                }
                // The rest of each (_timeOfSteps) second, for notify if the tetromino has been terminated, if not, move down automatedly
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
                    _blocksCtrl.TerminateTetromino(_currentTetromino.TheTetromino);
                    _currentTetromino = _nextTetromino;
                    _currentTetromino.SetPosition(new Point2DIndex() { XIndex = 5, YIndex = 0 });
                    _nextTetromino = new Tetromino(_blocksCtrl, 17, 1);
                }

                if ((!_currentTetromino.CheckIfEmpty()) || ((GameExecuter.Instance.TotalGameplayTime - _playedTimer / 1000) <= 0))
                {
                    _gameOver = true;
                    SplashKit.PauseMusic();
                    _gameOverSE.Play(1, AudioManager.Instance.SoundVolume);
                    _defaultTimer.Reset();
                    if ((!_currentTetromino.CheckIfEmpty()))
                    {
                        _currentTetromino.SetPosition(new Point2DIndex() { XIndex = 5, YIndex = 0 });
                        _currentTetromino.Update();
                    }
                    foreach (Button button in this.Buttons)
                    {
                        button.IsDisabled = true;
                    }
                }
            }
            else
            {
                if (_defaultTimer.Ticks >= 3000)
                {
                    GameExecuter.Instance.AddResult(_playedTimer, this.Score);
                    GameExecuter.Instance.End(_playedTimer / 1000, this.Score);
                }
            }

            _lastUpdateTimer = _defaultTimer.Ticks;
        }
        public override void Draw()
        {
            if (!_gameOver)
            {
                SplashKit.ClearScreen(Constants.GameBackgroundColor(_currentTetromino.TetrominoColor));

                base.Draw();

                _mainBoard.Draw();
                _blocksCtrl.Draw();

                SplashKit.DrawText($"Time left: {(GameExecuter.Instance.TotalGameplayTime - _playedTimer / 1000)}/{GameExecuter.Instance.TotalGameplayTime}", Color.Black, Constants.LEGEND_BOLD, 30, 500, 520);

                _currentTetromino.Draw();
                // Highlight the center point of the current tetromino
                SplashKit.FillCircle(Color.White, _currentTetromino.TheTetromino[0].Point.X + (Constants.SIZE_OF_BLOCK / 2), _currentTetromino.TheTetromino[0].Point.Y + (Constants.SIZE_OF_BLOCK / 2), 2);

                SplashKit.DrawText("NEXT", Color.Black, Constants.LEGEND_BOLD, 30, 560, 50);
                _nextBoard.Draw();
                _nextTetromino.Draw();
            }
            else
            {
                SplashKit.ClearScreen(Color.RandomRGB(255));

                _mainBoard.Draw();
                _blocksCtrl.Draw();

                _currentTetromino.Draw();
            }

            SplashKit.DrawText($"LEVEL: {GameExecuter.Instance.LevelType}", Color.Black, Constants.LEGEND_BOLD, 30, 500, 380);
            SplashKit.DrawText($"SCORE: {this.Score}", Color.Black, Constants.LEGEND_BOLD, 30, 500, 450);
        }

        private void Save()
        {
            StreamWriter writer = new StreamWriter(Constants.GAMEPLAY_TEXT_FOLRDER_LOCATION);
            try
            {
                writer.WriteLine("GAMEPLAY");
                writer.WriteLine((int)GameExecuter.Instance.LevelType);
                writer.WriteLine(GameExecuter.Instance.TotalGameplayTime);
                writer.WriteLine(_playedTimer);
                _currentTetromino.Save(writer);
                _nextTetromino.Save(writer);
                _blocksCtrl.Save(writer);
            }
            finally
            {
                writer.Close();
            }
        }
        private void Load()
        {
            StreamReader reader = new StreamReader(Constants.GAMEPLAY_TEXT_FOLRDER_LOCATION);
            try
            {
                string? kind = reader.ReadLine();
                if (kind == "GAMEPLAY")
                {
                    GameExecuter.Instance.LevelType = (LevelType)reader.ReadInteger();
                    GameExecuter.Instance.TotalGameplayTime = reader.ReadInteger();
                    _timeOfSteps = (uint)GameExecuter.Instance.LevelType;
                    _playedTimer = (uint)reader.ReadInteger();
                    _currentTetromino.Load(reader);
                    _nextTetromino.Load(reader);
                    _blocksCtrl.Load(reader);
                }
                else
                {
                    throw new InvalidDataException("Unknown gameplay kind: " + kind);
                }
            }
            finally
            {
                reader.Close();
            }
        }
    }
}