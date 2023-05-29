using SplashKitSDK;
using Timer = SplashKitSDK.Timer;

namespace GibbousTetris
{
    public class InstructionScene : Scene
    {
        private Music _backgroundMusic;

        private Timer _timer;
        private uint _stage;

        private GameBoard _mainBoard, _nextBoard;
        private BlocksController _blocksCtrl;
        private List<Tetromino> _exampleTetrominos;
        private Sprite _saveSprite, _loadSprite;

        public InstructionScene()
        {
            _backgroundMusic = new Music("Power Trip", Constants.MEDIA_FOLDER_LOCATION + "power_trip.mp3");
            _backgroundMusic.Play(1, AudioManager.Instance.MusicVolume);

            this.Buttons.Add(new ButtonCircle(Color.Gray, Color.Black, 50, 50, 20, "Home", "home.png"));
            this.Buttons.Add(new ButtonCircle(Color.DarkOliveGreen, Color.Black, 750, 50, 20, "Setting", "setting.png"));

            _timer = new Timer("Instruction Timer");
            _timer.Start();
            _stage = 0;

            _mainBoard = new GameBoard();
            _nextBoard = new GameBoard(new Point2D() { X = 440, Y = 100 }, new Point2DIndex() { XIndex = 6, YIndex = 4 });
            _blocksCtrl = new BlocksController(12, 20);

            _exampleTetrominos = new List<Tetromino>() 
            { 
                new Tetromino(_blocksCtrl, 0, 5, 6),
                new Tetromino(_blocksCtrl, 3, 10, 5),
                new Tetromino(_blocksCtrl, 2, 6, 9),
                new Tetromino(_blocksCtrl, 1, 11, 9),
                new Tetromino(_blocksCtrl, 4, 6, 13),
                new Tetromino(_blocksCtrl, 6, 11, 13), 
                new Tetromino(_blocksCtrl, 5, 6, 17) 
            };

            _saveSprite = SplashKit.CreateSprite(SplashKit.LoadBitmap("Save", Constants.MEDIA_FOLDER_LOCATION + "save.png"));
            _loadSprite = SplashKit.CreateSprite(SplashKit.LoadBitmap("Load", Constants.MEDIA_FOLDER_LOCATION + "load.png"));
        }

        public override void Update()
        {
            base.Update();

            _blocksCtrl.Update();

            switch (_stage)
            {
                case 0:
                    if (_timer.Ticks >= 3000)
                    {
                        _exampleTetrominos = new List<Tetromino>();
                        _stage++;
                    }
                    break;
                case 1:
                    if (_timer.Ticks >= 4000)
                    {
                        _exampleTetrominos.Add(new Tetromino(_blocksCtrl, 1, 5, 0));
                        _stage++;
                    }
                    break;
                case 2:
                    if (_timer.Ticks >= 4500)
                    {
                        _exampleTetrominos[0].MoveDown();
                        _stage++;
                    }
                    break;
                case 3:
                    if (_timer.Ticks >= 5500)
                    { 
                        _exampleTetrominos[0].MoveDown(); 
                        _stage++;
                    }
                    break;
                case 4:
                    if (_timer.Ticks >= 6000)
                    {
                        _timer.Pause();
                        if (SplashKit.KeyTyped(GameExecuter.Instance.RotateKey))
                        {
                            _exampleTetrominos[0].Rotate();
                            _timer.Resume();
                            _stage++;
                        }
                    }
                    break;
                case 5:
                    if (_timer.Ticks >= 7500)
                    {
                        _timer.Pause();
                        if (SplashKit.KeyTyped(GameExecuter.Instance.MoveDownKey))
                        {
                            _exampleTetrominos[0].MoveDown();
                            _timer.Resume();
                            _stage++;
                        }
                    }
                    break;
                case 6:
                    if (_timer.Ticks >= 9000)
                    {
                        _timer.Pause();
                        if (SplashKit.KeyTyped(GameExecuter.Instance.MoveLeftKey))
                        {
                            _exampleTetrominos[0].MoveLeft();
                            _timer.Resume();
                            _stage++;
                        }
                    }
                    break;
                case 7:
                    if (_timer.Ticks >= 10500)
                    {
                        _timer.Pause();
                        if (SplashKit.KeyTyped(GameExecuter.Instance.MoveRightKey))
                        {
                            _exampleTetrominos[0].MoveRight();
                            _timer.Resume();
                            _stage++;
                        }
                    }
                    break;
                case 8:
                    if (_timer.Ticks >= 12000)
                    {
                        _timer.Pause();
                        if (SplashKit.KeyTyped(GameExecuter.Instance.MoveToBottomKey))
                        {
                            _exampleTetrominos[0].MoveToBottom();
                            _timer.Resume();
                            _stage++;
                        }
                    }
                    break;
                case 9:
                    if (_timer.Ticks >= 14000)
                    {
                        _exampleTetrominos.Add(new Tetromino(_blocksCtrl, 0, 15, 1));
                        _stage++;
                    }
                    break;
                case 10:
                    if (_timer.Ticks >= 14500)
                    {
                        _exampleTetrominos[1].SetPosition(new Point2DIndex() { XIndex = 5, YIndex = 0 });
                        _exampleTetrominos.Add(new Tetromino(_blocksCtrl, 2, 15, 1));
                        _stage++;
                    }
                    break;
                case 11:
                    if (_timer.Ticks >= 15500)
                    {
                        _exampleTetrominos[1].SetPosition(new Point2DIndex() { XIndex = 1, YIndex = 19 });
                        _stage++;
                    }
                    break;
                case 12:
                    if (_timer.Ticks >= 16500)
                    {
                        _exampleTetrominos[2].SetPosition(new Point2DIndex() { XIndex = 5, YIndex = 1 });
                        _exampleTetrominos.Add(new Tetromino(_blocksCtrl, 1, 15, 1));
                        _stage++;
                    }
                    break;
                case 13:
                    if (_timer.Ticks >= 17500)
                    {
                        _exampleTetrominos[2].Rotate();
                        _exampleTetrominos[2].Rotate();
                        _exampleTetrominos[2].SetPosition(new Point2DIndex() { XIndex = 7, YIndex = 19 });
                        _stage++;
                    }
                    break;
                case 14:
                    if (_timer.Ticks >= 18500)
                    {
                        _exampleTetrominos[3].SetPosition(new Point2DIndex() { XIndex = 5, YIndex = 1 });
                        _stage++;
                    }
                    break;
                case 15:
                    if (_timer.Ticks >= 19500)
                    {
                        _exampleTetrominos[3].Rotate();
                        _exampleTetrominos[3].Rotate();
                        _exampleTetrominos[3].SetPosition(new Point2DIndex() { XIndex = 10, YIndex = 19 });
                        _stage++;
                    }
                    break;
                case 16:
                    if (_timer.Ticks >= 20500)
                    {
                        foreach (Tetromino t in _exampleTetrominos)
                        {
                            _blocksCtrl.TerminateTetromino(t.TheTetromino);
                        }
                        _exampleTetrominos.Clear();
                        _stage++;
                    }
                    break;
                case 17:
                    if (_timer.Ticks >= 22000)
                    {
                        _stage++;
                    }
                    break;
                case 18:
                    if (_timer.Ticks >= 23000)
                    {
                        _stage++;
                    }
                    break;
                case 19:
                    if (_timer.Ticks >= 27000)
                    {
                        this.Buttons.Add(new ButtonCircle(Color.RGBAColor(1, 1, 1, 0), Color.LightGoldenrodYellow, 600, 640, 80, "Play", "play.png"));
                        _stage++;
                    }
                    break;
                case 20:
                    _timer.Pause();
                    break;
            }

            foreach (Tetromino t in _exampleTetrominos)
            {
                t.Update();
            }
            _blocksCtrl.Update();
        }
        public override void Draw()
        {
            SplashKit.ClearScreen(Color.Pink);

            base.Draw();

            SplashKit.DrawText("HOW TO PLAY?", Color.DarkOrange, Constants.SPANISH_FAITH, 80, 225, 5);

            if ((_timer.Ticks >= 1000) && (_timer.Ticks < 3000))
            {
                SplashKit.DrawText("There is a total of 7 different tetromino: ", Color.DarkViolet, Constants.BROWN_SUGAR, 35, 80, 160);
                if (_timer.Ticks >= 1500)
                {
                    foreach (Tetromino t in _exampleTetrominos)
                    {
                        t.Draw();
                    }
                }
            }
            if (_timer.Ticks >= 3000)
            {
                _mainBoard.Draw();
                SplashKit.DrawText("This is the main gameboard,", Color.DimGray, Constants.BROWN_SUGAR, 30, 50, 720);
                SplashKit.DrawText("your tetromino is randomly created here!", Color.DimGray, Constants.BROWN_SUGAR, 30, 50, 755);
                if (_timer.Ticks >= 4000)
                {
                    if (_timer.Ticks < 6000)
                    {
                        SplashKit.DrawText("Your tetromino will automatically", Color.DarkGreen, Constants.BROWN_SUGAR, 25, 450, 150);
                        SplashKit.DrawText("fall from the top.", Color.DarkGreen, Constants.BROWN_SUGAR, 25, 450, 180);
                    }
                    else if ((_timer.Ticks >= 6000) && (_timer.Ticks < 7500))
                    {
                        this.DrawKeysInstruction($"Please press {SplashKit.KeyName(GameExecuter.Instance.RotateKey)} to rotate it!", 5);
                    }
                    else if ((_timer.Ticks >= 7500) && (_timer.Ticks < 9000))
                    {
                        this.DrawKeysInstruction($"Please press {SplashKit.KeyName(GameExecuter.Instance.MoveDownKey)} to move it down!", 6);
                    }
                    else if ((_timer.Ticks >= 9000) && (_timer.Ticks < 10500))
                    {
                        this.DrawKeysInstruction($"Please press {SplashKit.KeyName(GameExecuter.Instance.MoveLeftKey)} to move it left!", 7);
                    }
                    else if ((_timer.Ticks >= 10500) && (_timer.Ticks < 12000))
                    {
                        this.DrawKeysInstruction($"Please press {SplashKit.KeyName(GameExecuter.Instance.MoveRightKey)} to move it right!", 8);
                    }
                    else if ((_timer.Ticks >= 12000) && (_timer.Ticks < 13500))
                    {
                        this.DrawKeysInstruction($"Press {SplashKit.KeyName(GameExecuter.Instance.MoveToBottomKey)} to move it to bottom!", 9);
                    }
                    else if (_timer.Ticks >= 13500)
                    {
                        _nextBoard.Draw();
                        SplashKit.DrawText("This side board", Color.DimGray, Constants.BROWN_SUGAR, 25, 650, 110);
                        SplashKit.DrawText("presents you", Color.DimGray, Constants.BROWN_SUGAR, 25, 650, 140);
                        SplashKit.DrawText("the next one", Color.DimGray, Constants.BROWN_SUGAR, 25, 650, 170);
                        if (_timer.Ticks >= 15000)
                        {
                            SplashKit.DrawText("Fill the line(s) to gain score(s)", Color.DarkRed, Constants.BROWN_SUGAR, 25, 450, 250);
                        }
                        if (_timer.Ticks >= 21000)
                        {
                            SplashKit.DrawText($"SCORE: {_blocksCtrl.ScoreIndex}", Color.Black, Constants.LEGEND_BOLD, 30, 550, 340);
                        }
                        if (_timer.Ticks >= 22000 && (_timer.Ticks < 27000))
                        {
                            SplashKit.DrawText("Click                     to save the current game", Color.BrightGreen, Constants.BROWN_SUGAR, 20, 440, 450);
                            _saveSprite.Draw(490, 400);
                            if (_timer.Ticks >= 23000)
                            {
                                SplashKit.DrawText("Click                      to load the previous", Color.Orange, Constants.BROWN_SUGAR, 20, 440, 580);
                                SplashKit.DrawText("saved game", Color.Orange, Constants.BROWN_SUGAR, 20, 600, 600);
                                _loadSprite.Draw(490, 530);
                            }
                        }
                        if (_timer.Ticks >= 27000)
                        {
                            SplashKit.DrawText("PLAY NOW!!!", Color.DarkKhaki, Constants.GALACTIC_ADVENTURE, 40, 475, 480);
                        }
                    }
                    foreach (Tetromino t in _exampleTetrominos)
                    {
                        t.Draw();
                    }
                    _blocksCtrl.Draw();
                }   
            }
        }

        private void DrawKeysInstruction(string instruction, int currentStage)
        {
            SplashKit.DrawText(instruction, Color.DarkGreen, Constants.BROWN_SUGAR, 25, 440, 150);
            if (_stage == currentStage)
            {
                SplashKit.DrawText("Well done!", Color.Red, Constants.BROWN_SUGAR, 25, 440, 185);
            }
        }
    }
}