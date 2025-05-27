using System.Windows.Controls;
using ChromeDinoGame.Entities;

namespace ChromeDinoGame.Services
{
    class ObstacleSpawner
    {
        private static Random _random = new Random();

        public Obstacle GenerateObstacle(double lineOfGround, double speed)
        {
            if (_random.Next(0, 4) == 0)
                return new Bird(_random, speed);
            else
                return new Cactus(_random,lineOfGround, speed);
        }
    }
}
