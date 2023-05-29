using SplashKitSDK;

namespace GibbousTetris
{
    public class EndingScene : Scene
    {
        private Music _backgroundMusic;
        private Sprite _handPoint;

        private uint _previousPlayedTime, _previousScore;

        public EndingScene() : this(0, 0)
        {
        }
        public EndingScene(uint playedTime, uint score) : base()
        {
            _backgroundMusic = new Music("Back on Track", Constants.MEDIA_FOLDER_LOCATION + "back_on_track.mp3");
            _backgroundMusic.Play(5, AudioManager.Instance.MusicVolume);

            this.Buttons.Add(new ButtonCircle(Color.Gray, Color.Black, 50, 50, 20, "Home", "home.png"));
            this.Buttons.Add(new ButtonCircle(Color.DarkOliveGreen, Color.Black, 750, 50, 20, "Setting", "setting.png"));
            this.Buttons.Add(new ButtonCircle(Color.Gray, Color.Black, 750, 750, 20, "Result", "result.png"));
            this.Buttons.Add(new ButtonCircle(Color.Yellow, Color.DarkViolet, 400, 600, 39.5, "Replay", "replay.png"));

            _handPoint = SplashKit.CreateSprite(SplashKit.LoadBitmap("Hand Point", Constants.MEDIA_FOLDER_LOCATION + "handpoint.png"));
            _handPoint.X = (float)this.FetchButton("Replay").X - 240;
            _handPoint.Y = (float)this.FetchButton("Replay").Y - (_handPoint.Height / 2);

            _previousPlayedTime = playedTime;
            _previousScore = score;
        }

        public override void Update()
        {
            base.Update();

            _handPoint.X += 0.075f;
            if (_handPoint.X > (float)this.FetchButton("Replay").X - 220)
            {
                _handPoint.X = (float)this.FetchButton("Replay").X - 250;
            }
            this.FetchButton("Replay").ButtonSprite.Rotation -= 0.05f;
        }
        public override void Draw()
        {
            SplashKit.ClearScreen(Color.LightBlue);

            base.Draw();

            _handPoint.Draw();

            SplashKit.DrawText("GAME OVER", Color.Black, Constants.SPANISH_FAITH, 110, 200, 100);
            SplashKit.DrawText($"You have played the {GameExecuter.Instance.LevelType} level in {_previousPlayedTime}/{GameExecuter.Instance.TotalGameplayTime} secs", Color.DarkViolet, Constants.GERALD, 20, 25, 290);
            SplashKit.DrawText($"and gained {_previousScore} score(s)", Color.DarkSlateBlue, Constants.GERALD, 20, 45, 350);
            SplashKit.DrawText("Great job! Want to try again?", Color.DarkRed, Constants.MOTHERCODE, 40, 60, 450);
        }
    }
}
