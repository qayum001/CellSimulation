using Microsoft.Xna.Framework;

namespace MonoGameWindowsDesktopApplication1.Utils
{
    public class Transform
    {
        public Rectangle Rectangle { get; set; }
        public Point Index { get; set; }
        public Point Location { get; set; }
        public int Id { get; set; }
        public Direction Direction { get; set; }
    }
}