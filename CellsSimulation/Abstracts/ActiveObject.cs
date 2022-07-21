using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWindowsDesktopApplication1.interfaces;
using MonoGameWindowsDesktopApplication1.Utils;
using MonoGameWindowsDesktopApplication1.WorldObjects;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsDesktopApplication1.Abstracts
{
    public abstract class ActiveObject : IUpdatable
    {
        public float Energy { get; set; }
        public Transform Transform { get; set; }
        public Texture2D Sprite { get; set; }
        public Color Color { get; set; }
        public World World { get; private set; }

        public ActiveObject(World world)
        {
            World = world;
        }
        
        public virtual void Init() { }
        public virtual void Load() { }
        public virtual void Update() { }
        
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}