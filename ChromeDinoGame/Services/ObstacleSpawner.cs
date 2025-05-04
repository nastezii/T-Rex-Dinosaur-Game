using System.Windows.Controls;
using ChromeDinoGame.Entities;

namespace ChromeDinoGame.Services
{
    class ObstacleSpawner
    {
        private static Random _random = new Random();

        public Obstacle GenerateObstacle(Canvas canvas, double lineOfGround, double speed)
        {
            if (_random.Next(0, 5) == 0)
                return new Bird(canvas, _random, canvas.Width, speed);
            else
                return new Cactus(canvas, _random, canvas.Width, canvas.Height, lineOfGround, speed);
        }
    }
}
