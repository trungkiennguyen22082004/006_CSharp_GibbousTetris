using SplashKitSDK;

namespace GibbousTetris
{
    public abstract class Scene : ObservableSubject
    {
        private List<Button> _buttons;
        public Scene()
        {
            _buttons = new List<Button>();
            _buttons.Add(new ButtonRectangle());

            new ButtonFunctionObserver(this);
        }

        public List<Button> Buttons 
        { 
            get => _buttons;
        }

        public virtual void Update()
        {
            this.Notify();
        }
        public virtual void Draw()
        {
            foreach (Button button in this.Buttons)
            {
                button.Draw();
            }
        }

        protected Button FetchButton(string name)
        {
            foreach(Button button in this.Buttons) 
            {
                if (button.Name == name)
                {
                    return button;
                }    
            }
            throw new ArgumentException("Invalid button name: " + name);
        }
    }
}