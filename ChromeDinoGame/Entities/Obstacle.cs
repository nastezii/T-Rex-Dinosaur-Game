using System.Windows;

namespace ChromeDinoGame.Entities
{
    public abstract class Obstacle : Entity
    {
        private Dino _dino = Dino.Instance;
        private const int hitboxReduction = 20;

        public bool CheckCollision()
        {
            Rect dinoRect = new Rect(_dino.PosX, _dino.PosY, _dino.Width - hitboxReduction, _dino.Height - hitboxReduction);
            Rect obstacleRect = new Rect(PosX, PosY, Width - hitboxReduction, Height - hitboxReduction);

            if (dinoRect.IntersectsWith(obstacleRect))
            {
                return true;
            }
            return false;
        }
    }
}
