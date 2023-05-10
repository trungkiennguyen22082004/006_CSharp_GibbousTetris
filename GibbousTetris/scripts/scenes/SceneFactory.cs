namespace GibbousTetris
{
    public static class SceneFactory
    {
        public static Scene CreateScene() 
        { 
            return CreateScene(Constants.GAME_SCENE);
        }

        public static Scene CreateScene(int sceneID)
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
                default:
                {
                    throw new ArgumentException("Invalid scene ID!");
                }
            }

            return scene;
        }
    }
}