using SplashKitSDK;

namespace GibbousTetris
{
    public abstract class Button
    {
        private double _x, _y;
        private Color _btnColor;
        private Color _otlColor;
        private Bitmap _btnBitmap;
        private Sprite _btnSprite;

        public Button(Color btnColor, Color otlColor, double x, double y, string bitmapName, string bitmapLocation)
        {
            _btnColor = btnColor;
            _otlColor = otlColor;
            _x = x;
            _y = y;

            _btnBitmap = SplashKit.LoadBitmap(bitmapName, bitmapLocation);
            _btnSprite = SplashKit.CreateSprite(_btnBitmap);
        }

        protected double X
        {
            get => _x;
        }
        protected double Y
        {
            get => _y;
        }
        protected Color ButtonColor
        {
            get => _btnColor;
        }
        protected Color OutlineColor
        {
            get => _otlColor;
        }
        protected Sprite ButtonSprite
        {
            get => _btnSprite;
        }

        protected abstract bool IsMouseHover
        {
            get;
        }
        public bool IsClicked
        {
            get => ((IsMouseHover) && (SplashKit.MouseClicked(MouseButton.LeftButton)));
        }

        public abstract void Draw();
        protected abstract void DrawOutline();
    }
}
