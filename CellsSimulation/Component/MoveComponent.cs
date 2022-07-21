using System;
using Microsoft.Xna.Framework;
using MonoGameWindowsDesktopApplication1.Utils;
using MonoGameWindowsDesktopApplication1.WorldObjects;

namespace MonoGameWindowsDesktopApplication1.Component
{
    public class MoveComponent : AbstractComponent
    {
        private readonly Random _random;
        public MoveComponent(Cell cell) : base(cell)
        {
            _random = new Random();
        }

        public override void Action()
        {
            Move();
        }
        
        private void Move()
        {
            var currentIndex = Cell.Transform.Index;
            World.Map[currentIndex.X, currentIndex.Y].IsFree = false;
            
            var newPos = CanMoveToThisDirection();
            
            Rotate();
            Cell.Transform.Location = newPos.p;
            Cell.Transform.Rectangle = new Rectangle(newPos.p.X, newPos.p.Y, World.CellsSize, World.CellsSize);
            Cell.Transform.Index = newPos.i;
            World.Map[newPos.i.X, newPos.i.Y].IsFree = true;
        }
        
        private (Point p, Point i) CanMoveToThisDirection()
        {
            var direction = Cell.Transform.Direction;
            var newPos = GetNewPosition(direction);
            if (IsNotOutOfBounds(newPos.i) && !World.Map[newPos.i.X, newPos.i.Y].IsFree)
            {
                return (newPos.p, newPos.i);
            }
            return (Cell.Transform.Rectangle.Location, Cell.Transform.Index);
        }

        private (Point p, Point i) GetNewPosition(Direction direction)
        {
            var location = Cell.Transform.Location;
            var cellSize = World.CellsSize;
            var index = Cell.Transform.Index;
            
            Point newPoint;
            Point newIndex;
            
            switch(direction)
            {
                case Direction.Top:
                    newPoint = new Point(location.X, location.Y - cellSize);
                    newIndex = new Point(index.X, index.Y - 1);
                    break;
                case Direction.RightTop:
                    newPoint = new Point(location.X + cellSize, location.Y - cellSize);
                    newIndex = new Point(index.X + 1, index.Y - 1);
                    break;
                case Direction.Right:
                    newPoint = new Point(location.X + cellSize, location.Y);
                    newIndex = new Point(index.X + 1, index.Y);
                    break;
                case Direction.RightBottom:
                    newPoint = new Point(location.X + cellSize, location.Y + cellSize);
                    newIndex = new Point(index.X + 1, index.Y + 1);    
                    break;    
               case Direction.Bottom:
                   newPoint = new Point(location.X, location.Y + cellSize);
                   newIndex = new Point(index.X, index.Y + 1);
                   break;
                case Direction.LeftBottom:
                    newPoint = new Point(location.X - cellSize, location.Y + cellSize);
                    newIndex = new Point(index.X - 1, index.Y + 1);
                    break;
                case Direction.Left:
                    newPoint = new Point(location.X - cellSize, location.Y);
                    newIndex = new Point(index.X - 1, index.Y);
                    break;
                case Direction.LeftTop:
                    newPoint = new Point(location.X - cellSize, location.Y - cellSize);
                    newIndex = new Point(index.X - 1, index.Y - 1);
                    break;
                default: throw new NotImplementedException();
            };
            return (newPoint, newIndex);
        }

        private bool IsAvailablePosition(Point pos)
        {
            return (pos.X >= 0 && pos.Y >= 0) && (pos.X < Cell.World.Width && pos.Y < Cell.World.Height);
        }

        private bool IsNotOutOfBounds(Point index)
        {
            return (index.X >= 0 && index.Y >= 0) && (index.X < World.L && index.Y < World.H);
        }
        private void Rotate()
        {
            Cell.Transform.Direction = Direction.Top + _random.Next(8);
        }
    }
}