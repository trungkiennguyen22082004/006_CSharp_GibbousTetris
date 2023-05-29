using SplashKitSDK;

namespace GibbousTetris
{
    public class HomeScene : Scene
    {
        private Music _backgroundMusic;
        private Sprite _homeBackground, _downSprite1, _downSprite2, _handPoint;

        public HomeScene() : base()
        {
            _backgroundMusic = new Music("Press Start", Constants.MEDIA_FOLDER_LOCATION + "press_start.mp3");
            _backgroundMusic.Play(6, AudioManager.Instance.MusicVolume);

            this.Buttons.Add(new ButtonCircle(Color.Gray, Color.Lavender, 50, 50, 20, "Result", "result.png"));
            this.Buttons.Add(new ButtonCircle(Color.DarkOliveGreen, Color.LightYellow, 750, 50, 20, "Setting", "setting.png"));
            this.Buttons.Add(new ButtonCircle(Color.RGBAColor(1, 1, 1, 0), Color.LightGoldenrodYellow, 300, 700, 80, "Play", "play.png"));
            this.Buttons.Add(new ButtonCircle(Color.RGBAColor(1, 1, 1, 0), Color.LightGoldenrodYellow, 640, 700, 78, "Instruction", "instruction.png"));

            this.Buttons.Add(new ButtonCircle(Color.RGBAColor(1, 1, 1, 0), Color.Orange, 350, 350, 33, "Easy", "easy.png"));
            this.Buttons.Add(new ButtonCircle(Color.RGBAColor(1, 1, 1, 0), Color.MediumPurple, 500, 350, 33, "Normal", "normal.png"));
            this.Buttons.Add(new ButtonCircle(Color.RGBAColor(1, 1, 1, 0), Color.LightCyan, 650, 350, 33, "Hard", "hard.png"));
            this.Buttons.Add(new ButtonCircle(Color.RGBAColor(1, 1, 1, 0), Color.LightSeaGreen, 300, 520, 40, "1 Min", "1min.png"));
            this.Buttons.Add(new ButtonCircle(Color.RGBAColor(1, 1, 1, 0), Color.PaleVioletRed, 430, 520, 40, "2 Min", "2min.png"));
            this.Buttons.Add(new ButtonCircle(Color.RGBAColor(1, 1, 1, 0), Color.LightSalmon, 560, 520, 40, "5 Min", "5min.png"));

            this.Buttons.Add(new ButtonRectangle(Color.RGBAColor(1, 1, 1, 0), Color.LightSkyBlue, 650, 490, 50, 50, "TimeMinus", "next.png"));
            this.Buttons.Add(new ButtonRectangle(Color.RGBAColor(1, 1, 1, 0), Color.LightSkyBlue, 720, 490, 50, 50, "TimePlus", "next.png"));
            this.FetchButton("TimePlus").ButtonSprite.Rotation = 180f;

            _homeBackground = SplashKit.CreateSprite(SplashKit.LoadBitmap("Home Background", Constants.MEDIA_FOLDER_LOCATION + "home_background.png"));
            _homeBackground.Scale = (float)2 / 3;

            _downSprite1 = SplashKit.CreateSprite(SplashKit.LoadBitmap("Down", Constants.MEDIA_FOLDER_LOCATION + "down.png"));
            _downSprite1.X = (float)this.FetchButton("Easy").X - (_downSprite1.Width / 2);
            _downSprite1.Y = (float)this.FetchButton("Easy").Y - 100;

            _downSprite2 = SplashKit.CreateSprite(SplashKit.LoadBitmap("Down", Constants.MEDIA_FOLDER_LOCATION + "down.png"));
            _downSprite2.X = (float)this.FetchButton("1 Min").X - (_downSprite2.Width / 2);
            _downSprite2.Y = (float)this.FetchButton("1 Min").Y - 100;

            _handPoint = SplashKit.CreateSprite(SplashKit.LoadBitmap("Hand Point", Constants.MEDIA_FOLDER_LOCATION + "handpoint.png"));
            _handPoint.X = (float)this.FetchButton("Play").X - 250;
            _handPoint.Y = (float)this.FetchButton("Play").Y - (_handPoint.Height / 2);
        }

        public override void Update()
        {
            base.Update();

            switch (GameExecuter.Instance.LevelType)
            {
                case (LevelType.Easy):
                    _downSprite1.X = (float)this.FetchButton("Easy").X - (_downSprite1.Width / 2);
                    break;
                case (LevelType.Normal):
                    _downSprite1.X = (float)this.FetchButton("Normal").X - (_downSprite1.Width / 2);
                    break;
                case (LevelType.Hard):
                    _downSprite1.X = (float)this.FetchButton("Hard").X - (_downSprite1.Width / 2);
                    break;
            }

            switch (GameExecuter.Instance.TotalGameplayTime)
            {
                case (60):
                    _downSprite2.X = (float)this.FetchButton("1 Min").X - (_downSprite2.Width / 2);
                    break;
                case (120):
                    _downSprite2.X = (float)this.FetchButton("2 Min").X - (_downSprite2.Width / 2);
                    break;
                case (300):
                    _downSprite2.X = (float)this.FetchButton("5 Min").X - (_downSprite2.Width / 2);
                    break;
                default:
                    _downSprite2.X = 1200f;
                    break;
            }

            _downSprite1.Y += 0.15f;
            if (_downSprite1.Y > (float)this.FetchButton("Easy").Y - 95)
            {
                _downSprite1.Y = (float)this.FetchButton("Easy").Y - 110;
            }
            _downSprite2.Y += 0.15f;
            if (_downSprite2.Y > (float)this.FetchButton("1 Min").Y - 95)
            {
                _downSprite2.Y = (float)this.FetchButton("1 Min").Y - 110;
            }
            _handPoint.X += 0.3f;
            if (_handPoint.X > (float)this.FetchButton("Play").X - 230)
            {
                _handPoint.X = (float)this.FetchButton("Play").X - 260;
            }
        }
        public override void Draw()
        {
            //SplashKit.ClearScreen(Color.White);
            _homeBackground.Draw(-700, -200);

            base.Draw();

            _downSprite1.Draw();
            _downSprite2.Draw();
            _handPoint.Draw();

            SplashKit.DrawText("GIBBOUS", Color.LightPink, Constants.SPANISH_FAITH, 140, 50, 80);
            SplashKit.DrawText("TETRIS", Color.LightBlue, Constants.SPANISH_FAITH, 140, 420, 80);
            SplashKit.FillRectangle(Color.AntiqueWhite, this.FetchButton("Easy").X - 350, this.FetchButton("Easy").Y - 25, 260, 60);
            SplashKit.FillTriangle(Color.AntiqueWhite, this.FetchButton("Easy").X - 90, this.FetchButton("Easy").Y - 25, this.FetchButton("Easy").X - 60, this.FetchButton("Easy").Y + 5, this.FetchButton("Easy").X - 90, this.FetchButton("Easy").Y + 34);
            SplashKit.DrawText($"Level : {GameExecuter.Instance.LevelType}", Color.DarkBlue, Constants.BROWN_SUGAR, 40, this.FetchButton("Easy").X - 310, this.FetchButton("Easy").Y - 15);
            SplashKit.FillRectangle(Color.WhiteSmoke, this.FetchButton("1 Min").X - 350, this.FetchButton("1 Min").Y - 25, 250, 60);
            SplashKit.FillTriangle(Color.WhiteSmoke, this.FetchButton("1 Min").X - 100, this.FetchButton("1 Min").Y - 25, this.FetchButton("1 Min").X - 70, this.FetchButton("1 Min").Y + 5, this.FetchButton("1 Min").X - 100, this.FetchButton("1 Min").Y + 34);
            SplashKit.DrawText($"Time : {GameExecuter.Instance.TotalGameplayTime}", Color.DarkRed, Constants.BROWN_SUGAR, 40, this.FetchButton("1 Min").X - 260, this.FetchButton("1 Min").Y - 15);
            SplashKit.DrawText("or", Color.White, Constants.GALACTIC_ADVENTURE, 60, 450, 660);
        }
    }
}