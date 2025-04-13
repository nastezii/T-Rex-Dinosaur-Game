
using System.Windows;

namespace ChromeDinoGame.Entities
{
    public abstract class Obstacle : Entity
    {
        public bool CheckCollision(Rect dinoRect, Obstacle obstacle)
        {
            Rect objRect = new Rect(obstacle.PosX, obstacle.PosY, obstacle.Width, obstacle.Height);

            if (dinoRect.IntersectsWith(objRect))
            {
                return true;
            }
            return false;
        }
    }
}
