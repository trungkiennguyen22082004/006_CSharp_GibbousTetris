using SplashKitSDK;

namespace GibbousTetris
{
    public enum LevelType
    {
        Hard = 1,
        Normal = 2,
        Easy = 3
    }

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
        private Scene _currentScene;

        private GameExecuter()
        {
            _gameWindow = new Window("Gibbous Teltris", Constants.WINDOW_WIDTH, Constants.WINDOW_HEIGHT);

            _currentScene = SceneFactory.CreateScene();

            Level = LevelType.Hard;
        }

        public LevelType Level
        {
            get;
            set;
        }

        public void Execute()
        {
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                this.Update();
                this.Draw();

                SplashKit.RefreshScreen();
            }
            while (!_gameWindow.CloseRequested);
        }
        private void Update()
        {
            _currentScene.Update();
        }
        private void Draw()
        {
            _currentScene.Draw();
        }

        public void ChangeScene(int sceneID)
        {
            _currentScene = SceneFactory.CreateScene(sceneID);
        }
    }
}