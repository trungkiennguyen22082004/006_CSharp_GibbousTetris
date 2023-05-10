using SplashKitSDK;

namespace GibbousTetris
{
    public class ButtonNonShape : Button
    {
        public ButtonNonShape() : this(Color.White, 100, 100, "Default", "D:/GibbousTetris/media/default.png")
        {
        }

        public ButtonNonShape(Color otlColor, double x, double y, string bitmapName, string bitmapLocation) : base(Color.Black, otlColor, x, y, bitmapName, bitmapLocation)
        {
            SplashKit.MoveSpriteTo(this.ButtonSprite, this.X, this.Y);
        }

        protected override bool IsMouseHover
        {
            get => SplashKit.PointInRectangle(SplashKit.MousePosition(), this.BtnSpriteRectangle);
        }

        private Rectangle BtnSpriteRectangle
        {
            get => new Rectangle()
            {
                X = this.X,
                Y = this.Y,
                Width = this.ButtonSprite.Width,
                Height = this.ButtonSprite.Height
            };
        }

        private Rectangle OtlRectangle
        {
            get => new Rectangle()
            {
                X = this.X - 2,
                Y = this.Y - 2,
                Width = this.ButtonSprite.Width + 4,
                Height = this.ButtonSprite.Height + 4
            };
        }

        public override void Draw()
        {
            if (this.IsMouseHover)
            {
                this.DrawOutline();
            }

            SplashKit.DrawSprite(this.ButtonSprite);
        }
        protected override void DrawOutline()
        {
            SplashKit.FillRectangle(this.OutlineColor, this.OtlRectangle);
        }
    }
}
