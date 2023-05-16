using SplashKitSDK;
using System;

namespace GibbousTetris
{
    public class HomeScene : Scene
    {
        private Button _settingButton;
        private Button _playButton;

        public HomeScene()
        {
            _settingButton = new ButtonCircle(Color.Gray, Color.Black, 50, 50, 20, "Setting", Constants.MEDIA_FOLDER_LOCATION + "setting.png");
            _playButton = new ButtonRectangle(Color.Transparent, Color.Black, 360, 380, 80, 40, "Play", Constants.MEDIA_FOLDER_LOCATION + "play.png");
        }

        public override void Update()
        {
            SplashKit.ClearScreen(Color.White);

            if (_settingButton.IsClicked)
            {
                GameExecuter.Instance.ChangeScene(Constants.SETTING_SCENE);
            }
            else if (_playButton.IsClicked) 
            {
                GameExecuter.Instance.ChangeScene(Constants.GAME_SCENE);
            }
        }
        public override void Draw()
        {
            _settingButton.Draw();
            _playButton.Draw();
        }
    }
}
