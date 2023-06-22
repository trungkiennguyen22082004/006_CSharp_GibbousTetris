using SplashKitSDK;
using Timer = SplashKitSDK.Timer;

namespace GibbousTetris
{
    public class IntroScene : Scene
    {
        private SoundEffect _introSoundEffect;
        private Sprite _introBackground, _logo, _swinburneLogo;

        private Timer _timer;

        public IntroScene() 
        {
            _introSoundEffect = new SoundEffect("Introduction", Constants.MEDIA_FOLDER_LOCATION + "dsmom_trailer_intro.wav");

            _introBackground = SplashKit.CreateSprite(SplashKit.LoadBitmap("Intro Background", Constants.MEDIA_FOLDER_LOCATION + "nightsky.jpg"));
            _logo = SplashKit.CreateSprite(SplashKit.LoadBitmap("Logo", Constants.MEDIA_FOLDER_LOCATION + "logo.png"));
            _logo.Scale = (float)1 / 5;
            _logo.Y = -480;
            _swinburneLogo = SplashKit.CreateSprite(SplashKit.LoadBitmap("Swinburne Logo", Constants.MEDIA_FOLDER_LOCATION + "swinburne_logo.png"));
            _swinburneLogo.Y = 840;
            _introSoundEffect.Play(1, 0.5f);

            _timer = new Timer("Intro Timer");
        }

        public override void Update() 
        {
            if (_logo.Y <= 1.0f)
            {
                _logo.Y += 1.25f;
            }
            if (_swinburneLogo.Y >= 0f) 
            {
                _swinburneLogo.Y -= 2f;
            }
            else
            {
                if (!SplashKit.TimerStarted(_timer))
                {
                    _timer.Start();
                }
                if (_timer.Ticks >= 2500)
                {
                    _timer.Stop();
                    GameExecuter.Instance.ChangeScene(Constants.HOME_SCENE);
                }
            }
        }

        public override void Draw()
        {
            _introBackground.Draw();
            _logo.Draw(-500, 0);
            _swinburneLogo.Draw(460, 240);
        }
    }
}
