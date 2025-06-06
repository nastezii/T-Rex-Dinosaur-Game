﻿using System.Windows.Controls;
using ChromeDinoGame.Globals;

namespace ChromeDinoGame.Entities
{
    public abstract class Entity
    {
        protected double _speed;
        protected int _renderDepth;
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double PosX { get; protected set; }
        public double PosY { get; protected set; }
        public Image Sprite { get; protected set; }

        public bool IsInWindow() => PosX > - Width;

        public virtual void MoveEntity()
        {
            PosX -= _speed;
            Canvas.SetLeft(Sprite, PosX);
        }

        public void RenderEntity()
        {
            if (!GlobalCanvas.GameArea.Children.Contains(Sprite))
            {
                GlobalCanvas.GameArea.Children.Add(Sprite);
                Canvas.SetLeft(Sprite, PosX);
                Canvas.SetBottom(Sprite, PosY);
                Canvas.SetZIndex(Sprite, _renderDepth);
            }
        }

        public void RemoveEntity() => GlobalCanvas.GameArea.Children.Remove(Sprite);
    }
}
