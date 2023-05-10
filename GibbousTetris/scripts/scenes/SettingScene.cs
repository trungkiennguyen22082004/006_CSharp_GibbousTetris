using SplashKitSDK;
using System;

namespace GibbousTetris
{
    public class SettingScene : Scene
    {
        private Button _homeButton;

        public SettingScene()
        {
            _homeButton = new ButtonCircle(Color.Gray, Color.Black, 50, 50, 20, "Home", Constants.MEDIA_FOLDER_LOCATION + "home.png");
        }

        public override void Update()
        {
            SplashKit.ClearScreen(Color.Yellow);

            if (_homeButton.IsClicked)
            {
                GameExecuter.Instance.RequestChangeScene(Constants.HOME_SCENE);
            }
        }
        public override void Draw()
        {
            _homeButton.Draw();
        }
    }
}
