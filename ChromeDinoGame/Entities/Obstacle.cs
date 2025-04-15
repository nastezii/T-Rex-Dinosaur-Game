
using System.Windows;

namespace ChromeDinoGame.Entities
{
    public abstract class Obstacle : Entity
    {
        public bool CheckCollision(Rect dinoRect, Obstacle obstacle)
        {
            Rect obstRect = new Rect(obstacle.PosX, obstacle.PosY, obstacle.Width, obstacle.Height);

            if (dinoRect.IntersectsWith(obstRect))
            {
                return true;
            }
            return false;
        }
    }
}
