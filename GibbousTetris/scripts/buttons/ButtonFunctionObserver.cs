using SplashKitSDK;

namespace GibbousTetris
{
    public class ButtonFunctionObserver : IObserver
    {
        private Scene _scene;

        public ButtonFunctionObserver(Scene scene)
        {
            _scene = scene;
            _scene.Attach(this);
        }

        public void OnNotify()
        {
            foreach (Button button in _scene.Buttons)
            {
                if (button.IsHoldDown)
                {
                    // Setting scene - Change Volumes
                    if ((SplashKit.MouseX() >= 250) || (SplashKit.MouseX() <= 650))
                    {
                        switch (button.Name)
                        {
                            case "Change Sound":
                                AudioManager.Instance.SoundVolume = (SplashKit.MouseX() - 250) / 400;
                                AudioManager.Instance.SaveVolumesTo();
                                break;
                            case "Change Music":
                                AudioManager.Instance.MusicVolume = (SplashKit.MouseX() - 250) / 400;
                                AudioManager.Instance.SaveVolumesTo();
                                break;
                        }
                    }
                }
                else if (button.IsClicked)
                {
                    this.CheckButtonClicked(button.Name);
                }
            }
        }

        public void CheckButtonClicked(string buttonName)
        {
            switch (buttonName)
            {
                case "Home":
                    GameExecuter.Instance.ChangeScene(Constants.HOME_SCENE);
                    break;
                case "Result":
                    GameExecuter.Instance.ChangeScene(Constants.RESULT_SCENE);
                    break;
                case "Setting":
                    GameExecuter.Instance.ChangeScene(Constants.SETTING_SCENE);
                    break;
                case "Instruction":
                    GameExecuter.Instance.ChangeScene(Constants.INSTRUCTION_SCENE);
                    break;
                case "Play":
                case "Replay":
                    GameExecuter.Instance.ChangeScene(Constants.GAME_SCENE);
                    break;
                // Home Scene - Choosing Level Type buttons
                case "Easy":
                    GameExecuter.Instance.LevelType = LevelType.Easy;
                    break;
                case "Normal":
                    GameExecuter.Instance.LevelType = LevelType.Normal;
                    break;
                case "Hard":
                    GameExecuter.Instance.LevelType = LevelType.Hard;
                    break;
                // Home Scene - Modifying Total Gameplay time buttons
                case "1 Min":
                    GameExecuter.Instance.TotalGameplayTime = 60;
                    break;
                case "2 Min":
                    GameExecuter.Instance.TotalGameplayTime = 120;
                    break;
                case "5 Min":
                    GameExecuter.Instance.TotalGameplayTime = 300;
                    break;
                case "TimePlus":
                    if (GameExecuter.Instance.TotalGameplayTime < 600)
                    {
                        GameExecuter.Instance.TotalGameplayTime++;
                    }
                    else
                    {
                        GameExecuter.Instance.TotalGameplayTime = 1;
                    }
                    break;
                case "TimeMinus":
                    if (GameExecuter.Instance.TotalGameplayTime > 1)
                    {
                        GameExecuter.Instance.TotalGameplayTime--;
                    }
                    else
                    {
                        GameExecuter.Instance.TotalGameplayTime = 600;
                    }
                    break;
                case "Sound":
                    if (AudioManager.Instance.SoundVolume > 0) 
                    {
                        AudioManager.Instance.SoundVolume = 0;
                    }
                    else
                    {
                        AudioManager.Instance.SoundVolume = 0.5f;
                    }
                    break;
                case "Music":
                    if (AudioManager.Instance.MusicVolume > 0)
                    {
                        AudioManager.Instance.MusicVolume = 0;
                    }
                    else
                    {
                        AudioManager.Instance.MusicVolume = 0.5f;
                    }
                    break;
            }
        }
    }
}