using SplashKitSDK;

namespace GibbousTetris
{
    public class ButtonRectangle : Button
    {
        private double _width, _height;

        public ButtonRectangle() : this(Color.Black, Color.White, 100, 100, 100, 100, "Default", "D:/GibbousTetris/media/default.png")
        {
        }
        public ButtonRectangle(Color btnColor, Color otlColor, double x, double y, double width, double height, string bitmapName, string bitmapLocation) : base(btnColor, otlColor, x, y, bitmapName, bitmapLocation)
        {
            _width = width;
            _height = height;

            SplashKit.MoveSpriteTo(this.ButtonSprite, this.X + (this.Width / 2) - (this.ButtonSprite.Width / 2), this.Y + (this.Height / 2) - (this.ButtonSprite.Height / 2));
        }

        private double Width
        {
            get => _width;
        }
        private double Height
        {
            get => _height;
        }

        protected override bool IsMouseHover
        {
            get => SplashKit.PointInRectangle(SplashKit.MousePosition(), this.BtnRectangle);
        }

        private Rectangle BtnRectangle
        {
            get => new Rectangle()
            {
                X = this.X,
                Y = this.Y,
                Width = this.Width,
                Height = this.Height
            };
        }
        private Rectangle OtlRectangle
        {
            get => new Rectangle()
            {
                X = this.X - 2,
                Y = this.Y - 2,
                Width = this.Width + 4,
                Height = this.Height + 4
            };
        }

        public override void Draw()
        {
            if (this.IsMouseHover)
            {
                this.DrawOutline();
            }

            SplashKit.FillRectangle(this.ButtonColor, this.BtnRectangle);

            SplashKit.DrawSprite(this.ButtonSprite);
        }
        protected override void DrawOutline()
        {
            SplashKit.FillRectangle(this.OutlineColor, this.OtlRectangle);
        }
    }
}
