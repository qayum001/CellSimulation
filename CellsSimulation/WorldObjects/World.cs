using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWindowsDesktopApplication1.Abstracts;
using MonoGameWindowsDesktopApplication1.interfaces;
using MonoGameWindowsDesktopApplication1.Utils;

namespace MonoGameWindowsDesktopApplication1.WorldObjects
{
    public class World : IUpdatable
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public float[] SunEnergy { get; set; }
        private readonly Random _random;
        public int L { get; }
        public int H { get; }
        
        private readonly int _cellsCount;
        public int CellsSize { get; }
        public Spot[,] Map { get; set; }
        public ActiveObject[] Cells { get; set; }
        public Game Game { get; set; }
        public bool[,] IsCell { get; set; }

        public World(Game game)
        {
            Game = game;
            Width = 1400;
            Height = 800;
            CellsSize = Params.CellsSize;
            _cellsCount = Params.CellsCount;
            L = Width / CellsSize;
            H = Height / CellsSize;
            Cells = new ActiveObject[_cellsCount];
            _random = new Random();
            IsCell = new bool[L, H];
            Map = new Spot[L, H];

            for (var i = 0; i < L; i++)
            {
                for (var j = 0; j < H; j++)
                {
                    Map[i, j] = new Spot();
                }
            }
        }

        #region Game
        public void Init()
        {
            SpawnCells(_cellsCount);
            foreach (var cell in Cells)
            {
                cell?.Init();
            }
        }

        public void Load()
        {
            foreach (var cell in Cells)
            {
                cell?.Load();
            }
        }

        public void Update()
        {
            foreach (var cell in Cells)
            {
                cell?.Update();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var cell in Cells)
            {
                cell?.Draw(gameTime, spriteBatch);
            }
        }
        #endregion
        private void SunUpdate()
        {
            
        }

        private void SpawnCells(int amount)
        {
            var currentCount = 0;
            var l = 0;
            var h = 0;
            
            while (currentCount < amount)
            {
                Cells[currentCount] = new Cell(this)
                {
                    Transform = new Transform()
                    {
                        Rectangle = new Rectangle(l * CellsSize, h * CellsSize, CellsSize, CellsSize),
                        Direction = Direction.Top + _random.Next(7),
                        Index = new Point(l, h),
                        Location = new Point(l * CellsSize, h* CellsSize),
                        Id = currentCount,
                    }
                };
                Map[l, h].IsFree = true;
                l++;
                currentCount++;
                if (l == L)
                {
                    h++;
                    l = 0;
                }

                if (h == H) break;
            }
        }
    }
}