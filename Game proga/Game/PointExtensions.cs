using System.Drawing;

namespace Game
{
    public static class PointExtensions
    {
        public static bool InBounds(this Point point, Rectangle rect)
        {
            return point.X <= rect.Right
                && point.X >= rect.Left
                && point.Y <= rect.Bottom
                && point.Y >= rect.Top;
        }
    }
}
