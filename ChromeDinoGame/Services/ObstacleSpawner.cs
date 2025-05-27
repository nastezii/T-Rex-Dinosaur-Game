using ChromeDinoGame.Entities;

namespace ChromeDinoGame.Services
{
    class ObstacleSpawner
    {
        public Obstacle GenerateObstacle(double lineOfGround, double speed)
        {
            if (GlobalRandom.Instance.Next(0, 4) == 0)
                return new Bird(speed);
            else
                return new Cactus(lineOfGround, speed);
        }
    }
}
