using SplashKitSDK;
using System;

namespace GibbousTetris
{
    public class HomeScene : Scene
    {
        private Button _settingButton;

        public HomeScene()
        {
            _settingButton = new ButtonCircle(Color.Gray, Color.Black, 50, 50, 20, "Setting", Constants.MEDIA_FOLDER_LOCATION + "setting.png");
        }

        public override void Update()
        {
            SplashKit.ClearScreen(Color.White);

            if (_settingButton.IsClicked)
            {
                GameExecuter.Instance.RequestChangeScene(Constants.SETTING_SCENE);
            }
        }
        public override void Draw()
        {
            _settingButton.Draw();
        }
    }
}
