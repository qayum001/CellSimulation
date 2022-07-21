using MonoGameWindowsDesktopApplication1.interfaces;
using MonoGameWindowsDesktopApplication1.WorldObjects;

namespace MonoGameWindowsDesktopApplication1.Component
{
    public abstract class AbstractComponent : IComponent
    {
        protected readonly World World;
        protected readonly Cell Cell;

        protected AbstractComponent(Cell cell)
        {
            Cell = cell;
            World = cell.World;
        }

        public virtual void Action()
        {
            throw new System.NotImplementedException();
        }
    }
}