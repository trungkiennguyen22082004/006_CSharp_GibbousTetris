using SplashKitSDK;

namespace GibbousTetris
{
    public class ButtonCircle : Button
    {
        private double _radius;

        public ButtonCircle() : this(Color.Black, Color.White, 100, 100, 20, "Default", "D:/GibbousTetris/media/default.png")
        {
        }
        public ButtonCircle(Color btnColor, Color otlColor, double x, double y, double radius, string bitmapName, string bitmapLocation) : base(btnColor, otlColor, x, y, bitmapName,bitmapLocation)
        {
            _radius = radius;

            SplashKit.MoveSpriteTo(this.ButtonSprite, this.X - (this.ButtonSprite.Width / 2), this.Y - (this.ButtonSprite.Height / 2));
        }

        private double Radius
        {
            get => _radius;
        }

        protected override bool IsMouseHover
        {
            get => SplashKit.PointInCircle(SplashKit.MousePosition(), this.BtnCircle);
        }

        private Circle BtnCircle
        {
            get => new Circle()
            {
                Center = new Point2D()
                {
                    X = this.X,
                    Y = this.Y
                },
                Radius = this.Radius
            };
        }
        private Circle OtlCircle
        {
            get => new Circle()
            {
                Center = new Point2D()
                {
                    X = this.X,
                    Y = this.Y
                },
                Radius = this.Radius + 2
            };
        }

        public override void Draw()
        {
            if (this.IsMouseHover)
            {
                this.DrawOutline();
            }

            SplashKit.FillCircle(this.ButtonColor, this.BtnCircle);

            SplashKit.DrawSprite(this.ButtonSprite);
        }
        protected override void DrawOutline()
        {
            SplashKit.FillCircle(this.OutlineColor, this.OtlCircle);
        }
    }
}
