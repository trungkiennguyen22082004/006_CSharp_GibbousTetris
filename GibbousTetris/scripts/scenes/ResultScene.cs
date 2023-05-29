using SplashKitSDK;

namespace GibbousTetris
{
    public class ResultScene : Scene
    {
        private Music _backgroundMusic;
        private string _filterBy, _sortBy;
        private bool _sortUp;

        private List<Result> _results;

        public ResultScene() : base()
        {
            _backgroundMusic = new Music("Theory Of Everything", Constants.MEDIA_FOLDER_LOCATION + "theory_of_everything.mp3");
            _backgroundMusic.Play(5, AudioManager.Instance.MusicVolume);

            this.Buttons.Add(new ButtonCircle(Color.Gray, Color.Black, 50, 50, 20, "Home", "home.png"));
            this.Buttons.Add(new ButtonCircle(Color.DarkOliveGreen, Color.Black, 750, 50, 20, "Setting", "setting.png"));
            this.Buttons.Add(new ButtonRectangle(Color.Pink, Color.Gray, 200, 630, 100, 40, "None Filter", "default.png"));
            this.Buttons.Add(new ButtonRectangle(Color.CornflowerBlue, Color.Gray, 360, 630, 100, 40, "Easy Filter", "default.png"));
            this.Buttons.Add(new ButtonRectangle(Color.GreenYellow, Color.Gray, 520, 630, 100, 40, "Normal Filter", "default.png"));
            this.Buttons.Add(new ButtonRectangle(Color.IndianRed, Color.Gray, 680, 630, 100, 40, "Hard Filter", "default.png"));
            this.Buttons.Add(new ButtonRectangle(Color.Blue, Color.DarkGray, 200, 710, 100, 40, "NewOld Sort", "default.png"));
            this.Buttons.Add(new ButtonRectangle(Color.MediumVioletRed, Color.DarkGray, 360, 710, 100, 40, "Level Sort", "default.png"));
            this.Buttons.Add(new ButtonRectangle(Color.DarkRed, Color.DarkGray, 520, 710, 100, 40, "Score Sort", "default.png"));
            this.Buttons.Add(new ButtonRectangle(Color.DarkOrange, Color.DarkGray, 680, 710, 100, 40, "Time Sort", "default.png"));

            _filterBy = "None";
            _sortBy = "NewOld";
            _sortUp = false;

            _results = new List<Result>();
            GameExecuter.Instance.LoadResults();

            foreach(Result result in GameExecuter.Instance.Results)
            {
                _results.Add(result);
            }
        }

        public override void Update()
        {
            base.Update();

            this.CheckFilter();
            this.CheckSort();
        }

        public override void Draw()
        {
            SplashKit.ClearScreen(Color.White);

            SplashKit.FillRectangle(Color.Black, this.FetchButton(_filterBy + " Filter").X - 2, this.FetchButton(_filterBy + " Filter").Y - 2, 104, 44);
            SplashKit.FillRectangle(Color.Black, this.FetchButton(_sortBy + " Sort").X - 2, this.FetchButton(_sortBy + " Sort").Y - 2, 104, 44);

            base.Draw();

            SplashKit.DrawText("RESULTS", Color.DarkTurquoise, Constants.SPANISH_FAITH, 100, 250, 15);

            SplashKit.FillRectangle(Color.Black, 0, 138, 800, 4);
            SplashKit.DrawText("ID", Color.Black, Constants.LEGEND_BOLD, 25, 20, 160);
            SplashKit.DrawText("Level Type", Color.Black, Constants.LEGEND_BOLD, 25, 100, 160);
            SplashKit.DrawText("Total Gameplay Time", Color.Black, Constants.LEGEND_BOLD, 25, 270, 160);
            SplashKit.DrawText("Played Time", Color.Black, Constants.LEGEND_BOLD, 25, 530, 160);
            SplashKit.DrawText("Score", Color.Black, Constants.LEGEND_BOLD, 25, 710, 160);
            SplashKit.FillRectangle(Color.Black, 0, 195, 800, 4);
            SplashKit.FillRectangle(Color.Black, 60, 140, 3, 60 + _results.Count * 37);
            SplashKit.FillRectangle(Color.Black, 250, 140, 3, 60 + _results.Count * 37);
            SplashKit.FillRectangle(Color.Black, 510, 140, 3, 60 + _results.Count * 37);
            SplashKit.FillRectangle(Color.Black, 680, 140, 3, 60 + _results.Count * 37);

            for (int i = 0; i < _results.Count; i++)
            {
                SplashKit.DrawText($"{_results[i].ID}", _results[i].Color, Constants.LEGEND_BOLD, 22, 22, 210 + i * 37);
                SplashKit.DrawText($"{_results[i].LevelType}", _results[i].Color, Constants.LEGEND_BOLD, 22, 130, 210 + i * 37);
                SplashKit.DrawText($"{_results[i].TotalGamePlayTime}", _results[i].Color, Constants.LEGEND_BOLD, 22, 370, 210 + i * 37);
                SplashKit.DrawText($"{_results[i].PlayedTime}", _results[i].Color, Constants.LEGEND_BOLD, 22, 584, 210 + i * 37);
                SplashKit.DrawText($"{_results[i].Score}", _results[i].Color, Constants.LEGEND_BOLD, 22, 730, 210 + i * 37);

                if (i < _results.Count - 1)
                {
                    SplashKit.DrawLine(Color.Black, 0, 235 + i * 37, 800, 235 + i * 37);
                }
            }
            SplashKit.FillRectangle(Color.Black, 0, 198 + _results.Count * 37, 800, 4);

            SplashKit.DrawText("Filter by: ", Color.ForestGreen, Constants.BROWN_SUGAR, 35, 45, 640);
            SplashKit.DrawText("None", Color.Black, Constants.LEGEND_BOLD, 25, this.FetchButton("None Filter").X + 23, this.FetchButton("Easy Filter").Y + 12);
            SplashKit.DrawText("Easy", Color.Black, Constants.LEGEND_BOLD, 25, this.FetchButton("Easy Filter").X + 23, this.FetchButton("Easy Filter").Y + 12);
            SplashKit.DrawText("Normal", Color.Black, Constants.LEGEND_BOLD, 25, this.FetchButton("Normal Filter").X + 16, this.FetchButton("Normal Filter").Y + 12);
            SplashKit.DrawText("Hard", Color.Black, Constants.LEGEND_BOLD, 25, this.FetchButton("Hard Filter").X + 23, this.FetchButton("Hard Filter").Y + 12);
            
            SplashKit.DrawText("Sort by: ", Color.Crimson, Constants.BROWN_SUGAR, 35, 45, 720);
            SplashKit.DrawText("New-old", Color.White, Constants.LEGEND_BOLD, 25, this.FetchButton("NewOld Sort").X + 5, this.FetchButton("NewOld Sort").Y + 12);
            SplashKit.DrawText("Level", Color.White, Constants.LEGEND_BOLD, 25, this.FetchButton("Level Sort").X + 22, this.FetchButton("Level Sort").Y + 12);
            SplashKit.DrawText("Score", Color.White, Constants.LEGEND_BOLD, 25, this.FetchButton("Score Sort").X + 21, this.FetchButton("Score Sort").Y + 12);
            SplashKit.DrawText("Time", Color.White, Constants.LEGEND_BOLD, 25, this.FetchButton("Time Sort").X + 23, this.FetchButton("Time Sort").Y + 12);
        }

        private void CheckFilter()
        {
            if (this.FetchButton("None Filter").IsClicked)
            {
                _filterBy = "None";
                this.Filter(0);
            }
            else if (this.FetchButton("Easy Filter").IsClicked)
            {
                _filterBy = "Easy";
                this.Filter(LevelType.Easy);
            }
            else if (this.FetchButton("Normal Filter").IsClicked)
            {
                _filterBy = "Normal";
                this.Filter(LevelType.Normal);
            }
            else if (this.FetchButton("Hard Filter").IsClicked)
            {
                _filterBy = "Hard";
                this.Filter(LevelType.Hard);
            }
        }

        private void Filter(LevelType levelType)
        {
            _results = new List<Result>();
            foreach (Result result in GameExecuter.Instance.Results)
            {
                if (levelType == 0)
                {
                    _results.Add(result);
                }
                else
                {
                    if (levelType == result.LevelType)
                    {
                        _results.Add(result);
                    }
                }
            }
            this.Sort();
        }

        public void CheckSort()
        {
            if (this.FetchButton("NewOld Sort").IsClicked)
            {
                _sortBy = "NewOld";
                _sortUp = !_sortUp;
                this.Sort();
            }
            else if (this.FetchButton("Level Sort").IsClicked)
            {
                _sortBy = "Level";
                _sortUp = !_sortUp;
                this.Sort();
            }
            else if (this.FetchButton("Score Sort").IsClicked)
            {
                _sortBy = "Score";
                _sortUp = !_sortUp;
                this.Sort();
            }
            else if (this.FetchButton("Time Sort").IsClicked)
            {
                _sortBy = "Time";
                _sortUp = !_sortUp;
                this.Sort();
            }
        }

        private void Sort()
        {
            switch (_sortBy)
            {
                case "NewOld":
                    if (_results.Count > 0)
                    {
                        if (_sortUp)
                        {
                            _results = _results.OrderBy(r => r.ID).ToList();
                        }
                        else
                        {
                            _results = _results.OrderByDescending(r => r.ID).ToList();
                        }
                    }
                    break;
                case "Level":
                    if (_results.Count > 0)
                    {
                        if (_sortUp)
                        {
                            _results = _results.OrderBy(r => r.LevelType).ToList();
                        }
                        else
                        {
                            _results = _results.OrderByDescending(r => r.LevelType).ToList();
                        }
                    }
                    break;
                case "Score":
                    if (_results.Count > 0)
                    {
                        if (_sortUp)
                        {
                            _results = _results.OrderBy(r => r.Score).ToList();
                        }
                        else
                        {
                            _results = _results.OrderByDescending(r => r.Score).ToList();
                        }
                    }
                    break;
                case "Time":
                    if (_results.Count > 0)
                    {
                        if (_sortUp)
                        {
                            _results = _results.OrderBy(r => r.PlayedTime).ToList();
                        }
                        else
                        {
                            _results = _results.OrderByDescending(r => r.PlayedTime).ToList();
                        }
                    }
                    break;
            }
            
        }
    }
}