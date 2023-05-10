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
        private Scene _currentScene;
        private Scene? _nextScene;

        public Scene? NextScene
        {
            get => _nextScene;
            set => _nextScene = value;
        }

        private GameExecuter()
        {
            _gameWindow = new Window("Gibbous Teltris", Constants.WINDOW_WIDTH, Constants.WINDOW_HEIGHT);

            _currentScene = SceneFactory.CreateScene();
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
            this.ChangeScene();
        }
        private void Draw()
        {
            _currentScene.Draw();
        }

        private void ChangeScene()
        {
            if (_nextScene != null)
            {
                _currentScene = _nextScene;
                _nextScene = null;
            }
        }
        public void RequestChangeScene(int sceneID)
        {
            _nextScene = SceneFactory.CreateScene(sceneID);
        }
    }
}