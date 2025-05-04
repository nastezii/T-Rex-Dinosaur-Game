using System.Windows;

namespace ChromeDinoGame.Entities
{
    public abstract class Obstacle : Entity
    {
        public Rect CollisionBox { get; private set; }

        protected void SetCollisionBox() => CollisionBox = new Rect(PosX, PosY, Width - 20, Height - 20);

        public bool CheckCollision(Rect dinoRect, Rect obstacleRect)
        {
            if (dinoRect.IntersectsWith(obstacleRect))
            {
                return true;
            }
            return false;
        }
    }
}
