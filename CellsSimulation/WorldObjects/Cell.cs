using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWindowsDesktopApplication1.Abstracts;
using SharpDX.Direct3D9;
using Microsoft.Xna.Framework;
using MonoGameWindowsDesktopApplication1.Component;

namespace MonoGameWindowsDesktopApplication1.WorldObjects
{
    public class Cell : ActiveObject
    {
        public float DigestionStrength { get; set; }
        private float OrganicEnergyAmount { get; set; }
        private float PhotosynthesisEnergyAmount { get; set; }
        
        private readonly Random _random;
        public List<AbstractComponent> Components { get; set; }
        private readonly MoveComponent _moveComponent;
        public Cell(World world) : base(world)
        {
            _random = new Random();
            _moveComponent = new MoveComponent(this);
            Components = new List<AbstractComponent>();
            Color = new Color(new Vector3(1f, 1f, 1f));
        }

        public override void Init()
        {
            OrganicEnergyAmount = _random.Next(256);
            PhotosynthesisEnergyAmount = _random.Next(256);
            Components.Add(_moveComponent);
        }

        public override void Load()
        {
            Sprite = World.Game.Content.Load<Texture2D>("Cell");
        }

        public override void Update()
        {
            OrganicEnergyAmount = _random.Next(1, 100);
            PhotosynthesisEnergyAmount = _random.Next(1, 100);
            ChangeColor();
            _moveComponent.Action();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Transform.Rectangle, Color);
        }

        private void ChangeColor()
        {
            var colorC = ((OrganicEnergyAmount / PhotosynthesisEnergyAmount) % 1);
            Color = new Color(new Vector3(colorC, (1.0f - colorC), 0f));
        }
    }
}