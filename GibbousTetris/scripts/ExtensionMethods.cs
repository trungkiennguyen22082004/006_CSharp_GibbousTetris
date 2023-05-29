using SplashKitSDK;

namespace GibbousTetris
{
    public static class ExtensionMethods
    {
        public static int ReadInteger(this StreamReader reader)
        {
            return Convert.ToInt32(reader.ReadLine());
        }

        public static float ReadFloat(this StreamReader reader)
        {
            return Convert.ToSingle(reader.ReadLine());
        }

        public static double ReadDouble(this StreamReader reader)
        {
            return Convert.ToDouble(reader.ReadLine());
        }

        public static Color ReadColor(this StreamReader reader)
        {
            return Color.RGBColor(reader.ReadFloat(), reader.ReadFloat(), reader.ReadFloat());
        }

        public static void WriteColor(this StreamWriter writer, Color clr)
        {
            writer.WriteLine("{0}\n{1}\n{2}", clr.R, clr.G, clr.B);
        }
    }
}
