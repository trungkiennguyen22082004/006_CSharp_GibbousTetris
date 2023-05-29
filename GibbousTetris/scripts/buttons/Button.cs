using SplashKitSDK;

namespace GibbousTetris
{
    public abstract class Button
    {
        private SoundEffect _buttonClickSE;
        private double _x, _y;
        private bool _isDisabled;
        private Color _btnColor, _otlColor;
        private Bitmap _btnBitmap;
        private Sprite _btnSprite;

        public Button(Color btnColor, Color otlColor, double x, double y, string bitmapName, string bitmapLocation)
        {
            _buttonClickSE = new SoundEffect("Button Click", Constants.MEDIA_FOLDER_LOCATION + "button_click.wav");

            _btnColor = btnColor;
            _otlColor = otlColor;

            _x = x;
            _y = y;

            _btnBitmap = SplashKit.LoadBitmap(bitmapName, Constants.MEDIA_FOLDER_LOCATION + bitmapLocation);
            _btnSprite = SplashKit.CreateSprite(_btnBitmap);

            _isDisabled = false;
        }

        public double X
        {
            get => _x;
            set => _x = value;
        }
        public double Y
        {
            get => _y;
            set => _y = value;
        }
        protected Color ButtonColor
        {
            get => _btnColor;
        }
        protected Color OutlineColor
        {
            get => _otlColor;
        }
        public Sprite ButtonSprite
        {
            get => _btnSprite;
        }
        public string Name
        {
            get => _btnBitmap.Name;
        }

        protected abstract bool IsMouseHover
        {
            get;
        }
        public bool IsDisabled
        {   
            get => _isDisabled; 
            set => _isDisabled = value;
        }
        public bool IsClicked
        {
            get
            {
                if ((!this.IsDisabled) && (this.IsMouseHover) && (SplashKit.MouseClicked(MouseButton.LeftButton)))
                {
                    _buttonClickSE.Play(1, AudioManager.Instance.SoundVolume);
                    return true;
                }
                return false;
            }
        }
        public bool IsHoldDown
        {
            get => ((!this.IsDisabled) && (this.IsMouseHover) && (SplashKit.MouseDown(MouseButton.LeftButton)));
        }

        public abstract void Draw();
        protected abstract void DrawOutline();
    }
}
