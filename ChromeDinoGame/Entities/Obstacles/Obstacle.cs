using System.Windows.Controls;

namespace ChromeDinoGame.Entities.Obstacles
{
    public abstract class Obstacle
    {
        public Image Sprite { get; protected set; }
        public double PosX { get; protected set; }
        public double PosY { get; protected set; }
        public double Speed { get; protected set; }
        public bool IsActive { get; protected set; } = true;

        public Obstacle(double x, double y)
        {
            PosX = x;
            PosY = y;
        }

        public void UpdateObject()
        {
            PosX -= Speed;

            if (IsOffScreen())
                IsActive = false;
        }

        protected bool IsOffScreen()
        {
            return PosX + Sprite.ActualWidth < 0;
        }
    }
}
