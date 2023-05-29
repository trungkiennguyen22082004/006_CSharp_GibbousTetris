using SplashKitSDK;

namespace GibbousTetris
{
    public class SettingScene : Scene
    {
        private Music _backgroundMusic;

        private Sprite _muteSprite;

        private string _keyToChange;
        private bool _invalidKeyWarning;

        public string KeyToChange
        {
            get => _keyToChange;
            set => _keyToChange = value;
        }
        public bool InvalidKeyWarning
        {
            get => _invalidKeyWarning;
            set => _invalidKeyWarning = value;
        }

        public SettingScene()
        {
            _backgroundMusic = new Music("Cant let go", Constants.MEDIA_FOLDER_LOCATION + "cant_let_go.mp3");
            _backgroundMusic.Play(5, AudioManager.Instance.MusicVolume);

            this.Buttons.Add(new ButtonCircle(Color.Gray, Color.Black, 50, 50, 20, "Home", "home.png"));
            this.Buttons.Add(new ButtonRectangle(Color.RGBAColor(1, 1, 1, 0), Color.RGBAColor(1, 1, 1, 0), 75, 220, 75, 75, "Sound", "sound.png"));
            this.Buttons.Add(new ButtonRectangle(Color.RGBAColor(1, 1, 1, 0), Color.RGBAColor(1, 1, 1, 0), 75, 340, 75, 75, "Music", "music.png"));
            this.Buttons.Add(new ButtonCircle(Color.RGBAColor(1, 1, 1, 0), Color.RGBAColor(1, 1, 1, 0), 250, 260, 24, "Change Sound", "change_sound.png"));
            this.Buttons.Add(new ButtonCircle(Color.RGBAColor(1, 1, 1, 0), Color.RGBAColor(1, 1, 1, 0), 250, 380, 24, "Change Music", "change_music.png"));
            this.Buttons.Add(new ButtonRectangle(Color.LightBlue, Color.Gray, 80, 550, 180, 60, "Rotate Key", "default.png"));
            this.Buttons.Add(new ButtonRectangle(Color.Lime, Color.Gray, 320, 550, 180, 60, "Move Left Key", "default.png"));
            this.Buttons.Add(new ButtonRectangle(Color.YellowGreen, Color.Gray, 560, 550, 180, 60, "Move Right Key", "default.png"));
            this.Buttons.Add(new ButtonRectangle(Color.Violet, Color.Gray, 200, 660, 180, 60, "Move Down Key", "default.png"));
            this.Buttons.Add(new ButtonRectangle(Color.HotPink, Color.Gray, 450, 660, 220, 60, "Move To Bottom Key", "default.png"));

            _muteSprite = SplashKit.CreateSprite(SplashKit.LoadBitmap("Mute", Constants.MEDIA_FOLDER_LOCATION + "mute.png"));

            _keyToChange = "";
            _invalidKeyWarning = false;
        }

        public override void Update()
        {
            base.Update();
            foreach (Button button in this.Buttons) 
            {
                if (button.IsClicked) 
                {
                    if ((button.Name == "Rotate Key") || (button.Name == "Move Left Key") || (button.Name == "Move Right Key") || (button.Name == "Move Down Key") || (button.Name == "Move To Bottom Key"))
                    {
                        this.KeyToChange = button.Name;
                    }
                    else
                    {
                        this.KeyToChange = "";
                    }
                }
            }

            this.FetchButton("Change Sound").X = AudioManager.Instance.SoundVolume * 400 + 250;
            this.FetchButton("Change Music").X = AudioManager.Instance.MusicVolume * 400 + 250;
            if (this.KeyToChange != "")
            {                    
                GameExecuter.Instance.ListenToChangeKey(this);
            }
            else
            {
                this.InvalidKeyWarning = false;
            }
        }
        public override void Draw()
        {
            SplashKit.ClearScreen(Color.LightYellow);

            SplashKit.FillRectangle(Color.LightGray, 250, this.FetchButton("Change Sound").Y - 10, 400, 20);
            SplashKit.FillRectangle(Color.DarkViolet, 250, this.FetchButton("Change Sound").Y - 10, AudioManager.Instance.SoundVolume * 400, 20);
            SplashKit.FillRectangle(Color.LightGray, 250, this.FetchButton("Change Music").Y - 10, 400, 20);
            SplashKit.FillRectangle(Color.DarkBlue, 250, this.FetchButton("Change Music").Y - 10, AudioManager.Instance.MusicVolume * 400, 20);

            SplashKit.DrawText("SETTINGS", Color.DarkMagenta, Constants.SPANISH_FAITH, 120, 240, 15);

            SplashKit.DrawText("Audio: ", Color.DarkGreen, Constants.BROWN_SUGAR, 35, 80, 160);
            SplashKit.DrawText("Sound Effects and Background Musics:", Color.DarkSeaGreen, Constants.BROWN_SUGAR, 35, 180, 160);

            if (AudioManager.Instance.SoundVolume <= 0)
            {
                _muteSprite.Draw(this.FetchButton("Sound").X, this.FetchButton("Sound").Y);
            }
            if (AudioManager.Instance.MusicVolume <= 0)
            {
                _muteSprite.Draw(this.FetchButton("Music").X, this.FetchButton("Music").Y);
            }

            SplashKit.DrawText("Playing Keys: ", Color.DarkGreen, Constants.BROWN_SUGAR, 35, 80, 460);

            if (this.KeyToChange == "")
            {
                SplashKit.DrawText("Choose Key you want to change:", Color.DarkSeaGreen, Constants.BROWN_SUGAR, 35, 270, 460);
            }
            else
            {
                if (this.KeyToChange == "Move To Bottom Key")
                {
                    SplashKit.FillRectangle(Color.Black, this.FetchButton(this.KeyToChange).X - 2, this.FetchButton(this.KeyToChange).Y - 2, 224, 64);
                }
                else
                {
                    SplashKit.FillRectangle(Color.Black, this.FetchButton(this.KeyToChange).X - 2, this.FetchButton(this.KeyToChange).Y - 2, 184, 64);
                }
                SplashKit.DrawText("Press Key you want to change to", Color.DarkSeaGreen, Constants.BROWN_SUGAR, 35, 270, 460);
            }

            base.Draw();

            SplashKit.DrawText("Rotate Key:", Color.Black, Constants.LEGEND_BOLD, 20, this.FetchButton("Rotate Key").X + 20, this.FetchButton("Rotate Key").Y + 10);
            SplashKit.DrawText($"{SplashKit.KeyName(GameExecuter.Instance.RotateKey)}", Color.Black, Constants.MOTHERCODE, 16, this.FetchButton("Rotate Key").X + 20, this.FetchButton("Rotate Key").Y + 32);
            SplashKit.DrawText("Move Left Key:", Color.Black, Constants.LEGEND_BOLD, 20, this.FetchButton("Move Left Key").X + 20, this.FetchButton("Move Left Key").Y + 10);
            SplashKit.DrawText($"{SplashKit.KeyName(GameExecuter.Instance.MoveLeftKey)}", Color.Black, Constants.MOTHERCODE, 16, this.FetchButton("Move Left Key").X + 20, this.FetchButton("Move Left Key").Y + 32);
            SplashKit.DrawText("Move Right Key:", Color.Black, Constants.LEGEND_BOLD, 20, this.FetchButton("Move Right Key").X + 20, this.FetchButton("Move Right Key").Y + 10);
            SplashKit.DrawText($"{SplashKit.KeyName(GameExecuter.Instance.MoveRightKey)}", Color.Black, Constants.MOTHERCODE, 16, this.FetchButton("Move Right Key").X + 20, this.FetchButton("Move Right Key").Y + 32);
            SplashKit.DrawText("Move Down Key:", Color.Black, Constants.LEGEND_BOLD, 20, this.FetchButton("Move Down Key").X + 20, this.FetchButton("Move Down Key").Y + 10);
            SplashKit.DrawText($"{SplashKit.KeyName(GameExecuter.Instance.MoveDownKey)}", Color.Black, Constants.MOTHERCODE, 16, this.FetchButton("Move Down Key").X + 20, this.FetchButton("Move Down Key").Y + 32);
            SplashKit.DrawText("Move To Bottom Key:", Color.Black, Constants.LEGEND_BOLD, 20, this.FetchButton("Move To Bottom Key").X + 20, this.FetchButton("Move To Bottom Key").Y + 10);
            SplashKit.DrawText($"{SplashKit.KeyName(GameExecuter.Instance.MoveToBottomKey)}", Color.Black, Constants.MOTHERCODE, 16, this.FetchButton("Move To Bottom Key").X + 20, this.FetchButton("Move To Bottom Key").Y + 32);
        
            if (this.InvalidKeyWarning)
            {
                SplashKit.DrawText("New key must be different from the existing ones", Color.DarkRed, Constants.BROWN_SUGAR, 30, 150, 750);
            }
        }
    }
}