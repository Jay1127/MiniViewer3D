using System.Windows.Media;
using MiniEyes.Geometry;

namespace MiniViewer3D
{
    internal static class MiniEyesHelper
    {
        public static SolidColorBrush ToBrush(ColorF color)
        {
            return new SolidColorBrush(ToColor(color));
        }

        public static Color ToColor(ColorF color)
        {
            return Color.FromArgb(ToByte(color.A), ToByte(color.R), ToByte(color.G), ToByte(color.B));
        }

        public static ColorF ToColorF(Color color)
        {
            return new ColorF(color.R / 255.0, color.G / 255.0, color.B / 255.0);
        }

        public static ColorF ToColorF(SolidColorBrush solidColorBrush)
        {
            return ToColorF(solidColorBrush.Color);
        }

        private static byte ToByte(float fValue)
        {
            return (byte)(fValue * 255);
        }
    }
}