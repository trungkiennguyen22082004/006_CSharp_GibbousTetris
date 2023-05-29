namespace GibbousTetris
{
    public class SceneFactory
    {
        public Scene CreateScene() 
        {
            return CreateScene(Constants.HOME_SCENE);
        }

        public Scene CreateScene(int sceneID)
        {
            Scene scene;

            switch (sceneID)
            {
                case Constants.HOME_SCENE:
                {
                    scene = new HomeScene();
                    break;
                }
                case Constants.SETTING_SCENE:
                {
                    scene = new SettingScene();
                    break;
                }
                case Constants.INSTRUCTION_SCENE:
                {
                    scene = new InstructionScene();
                    break;
                }
                case Constants.GAME_SCENE:
                {
                    scene = new GameScene();
                    break;
                }
                case Constants.ENDING_SCENE:
                {
                    scene = new EndingScene();
                    break;
                }
                case Constants.RESULT_SCENE:
                    scene = new ResultScene();
                    break;
                default:
                {
                    throw new ArgumentException("Invalid scene ID!");
                }
            }

            return scene;
        }
        public Scene CreateEndScene(uint playedTime, uint score)
        {
            return new EndingScene(playedTime, score);
        }
    }
}