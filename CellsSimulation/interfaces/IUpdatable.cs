using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsDesktopApplication1.interfaces
{
    public interface IUpdatable
    {
        public abstract void Init();
        public abstract void Load();
        public abstract void Update();
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}