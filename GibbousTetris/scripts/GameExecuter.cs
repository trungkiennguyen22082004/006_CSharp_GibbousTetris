using SplashKitSDK;

namespace GibbousTetris
{
    public class GameExecuter
    {
        private static GameExecuter? _instance;
        public static GameExecuter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameExecuter();
                }

                return _instance;
            }

            // get => _instance ??= new GameExecuter();
        }

        private Window _gameWindow;
        private SceneFactory _sceneFactory;
        private Scene _currentScene;

        private GameExecuter()
        {
            _gameWindow = new Window("Gibbous Teltris", Constants.WINDOW_WIDTH, Constants.WINDOW_HEIGHT);

            _levelType = LevelType.Hard;
            _totalGameplayTime = 120;

            this.LoadKeysFrom();

            _results = new List<Result>();
            this.LoadResults();

            _sceneFactory = new SceneFactory();
            _currentScene = _sceneFactory.CreateScene();

            AudioManager.Instance.LoadVolumesFrom();
        }

        private LevelType _levelType;
        private int _totalGameplayTime;

        public LevelType LevelType
        {
            get => _levelType;
            set => _levelType = value;
        }
        public int TotalGameplayTime
        {
            get => _totalGameplayTime;
            set => _totalGameplayTime = value;
        }

        public void Execute()
        {
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                this.Update();
                this.Draw();

                SplashKit.RefreshScreen(120);
            }
            while (!_gameWindow.CloseRequested);
        }
        private void Update()
        {
            _currentScene.Update();
            SplashKit.SetMusicVolume(AudioManager.Instance.MusicVolume);
        }
        private void Draw()
        {
            _currentScene.Draw();
        }

        public void ChangeScene(int sceneID)
        {
            _currentScene = _sceneFactory.CreateScene(sceneID);
        }
        public void End(uint playedTime, uint score)
        {
            _currentScene = _sceneFactory.CreateEndScene(playedTime, score);
        }

        // Playing Keys section
        private KeyCode _rotate, _moveLeft, _moveRight, _moveDown, _moveToBottom;

        public KeyCode RotateKey                  // The key to rotate tetromino in GameScene
        {
            get => _rotate;
        }
        public KeyCode MoveLeftKey                // The key to move tetromino left in GameScene
        {
            get => _moveLeft;
        }
        public KeyCode MoveRightKey               // The key to move tetromino right in GameScene
        {
            get => _moveRight;
        }
        public KeyCode MoveDownKey                // The key to move tetromino down in GameScene
        {
            get => _moveDown;
        }
        public KeyCode MoveToBottomKey            // The key to move tetromino to bottom in GameScene
        {
            get => _moveToBottom;
        }

        public void ListenToChangeKey(SettingScene settingScene)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode))) 
            {
                if (SplashKit.KeyTyped(keyCode))
                {
                    if ((keyCode != _rotate) && (keyCode != _moveLeft) && (keyCode != _moveRight) && (keyCode != _moveDown) && (keyCode != _moveToBottom))
                    {
                        switch (settingScene.KeyToChange)
                        {
                            case "Rotate Key":
                                _rotate = keyCode;
                                break;
                            case "Move Left Key":
                                _moveLeft = keyCode;
                                break;
                            case "Move Right Key":
                                _moveRight = keyCode;
                                break;
                            case "Move Down Key":
                                _moveDown = keyCode;
                                break;
                            case "Move To Bottom Key":
                                _moveToBottom = keyCode;
                                break;
                        }
                        this.SaveKeysTo();
                        settingScene.InvalidKeyWarning = false;
                    }
                    else
                    {
                        settingScene.InvalidKeyWarning = true;
                    }
                }
            }
        }

        private void SaveKeysTo()
        {
            StreamWriter writer = new StreamWriter(Constants.KEYS_TEXT_FOLRDER_LOCATION);
            try
            {
                writer.WriteLine((int)this.RotateKey);
                writer.WriteLine((int)this.MoveLeftKey);
                writer.WriteLine((int)this.MoveRightKey);
                writer.WriteLine((int)this.MoveDownKey);
                writer.WriteLine((int)this.MoveToBottomKey);
            }
            finally
            {
                writer.Close();
            }
        }
        private void LoadKeysFrom() 
        {
            StreamReader reader = new StreamReader(Constants.KEYS_TEXT_FOLRDER_LOCATION);
            try
            {
                _rotate = (KeyCode)reader.ReadInteger();
                _moveLeft = (KeyCode)reader.ReadInteger();
                _moveRight = (KeyCode)reader.ReadInteger();
                _moveDown = (KeyCode)reader.ReadInteger();
                _moveToBottom = (KeyCode)reader.ReadInteger();
            }
            finally
            {
                if (this.RotateKey == KeyCode.UnknownKey)
                {
                    _rotate = KeyCode.UpKey;
                }
                if (this.MoveLeftKey == KeyCode.UnknownKey)
                {
                    _moveLeft = KeyCode.LeftKey;
                }
                if (this.MoveRightKey == KeyCode.UnknownKey)
                {
                    _moveRight = KeyCode.RightKey;
                }
                if (this.MoveDownKey == KeyCode.UnknownKey)
                {
                    _moveDown = KeyCode.DownKey;
                }
                if (this.MoveToBottomKey == KeyCode.UnknownKey)
                {
                    _moveToBottom = KeyCode.SpaceKey;
                }
                reader.Close();
            }
        }

        // Achievements Section
        private List<Result> _results;
        public List<Result> Results
        {
            get => _results;
        }
        
        public void AddResult(uint playedTime, uint score)
        {
            Color color;
            do
            {
                color = Color.RandomRGB(255);
            }
            while ((color.R == Color.White.R) && (color.G == Color.White.G) && (color.B == Color.White.B));

            _results.Insert(0, new Result()
            {
                Color = color,
                LevelType = this.LevelType,
                TotalGamePlayTime = this.TotalGameplayTime,
                PlayedTime = playedTime / 1000,
                Score = score
            });
            this.SaveResults();
        }
        public void SaveResults()
        {
            StreamWriter writer = new StreamWriter(Constants.RESULT_TEXT_FOLRDER_LOCATION);
            try 
            {
                while (_results.Count >= 12)
                {
                    _results.RemoveAt(_results.Count - 1);
                }
                writer.WriteLine(_results.Count);
                foreach (Result result in _results) 
                {
                    writer.WriteColor(result.Color);
                    writer.WriteLine((int)result.LevelType);
                    writer.WriteLine(result.TotalGamePlayTime);
                    writer.WriteLine(result.PlayedTime);
                    writer.WriteLine(result.Score);
                }
            }
            finally
            { 
                writer.Close();
            }
        }
        public void LoadResults()
        {
            StreamReader reader = new StreamReader(Constants.RESULT_TEXT_FOLRDER_LOCATION);
            _results = new List<Result>();
            int count = reader.ReadInteger();
            for (int i = 0; i < count; i++)
            {
                _results.Add(new Result()
                {
                    ID = count - i,
                    Color = reader.ReadColor(),
                    LevelType = (LevelType)reader.ReadInteger(),
                    TotalGamePlayTime = reader.ReadInteger(),
                    PlayedTime = (uint)reader.ReadInteger(),
                    Score = (uint)reader.ReadInteger()
                });
            }
        }
    }
}