using ChromeDinoGame.Entities;
using ChromeDinoGame.Globals;

namespace ChromeDinoGame.Services
{
    class ObstacleSpawner
    {
        private const int BirdChance = 3;

        public Obstacle GenerateObstacle(double lineOfGround, double speed)
        {
            bool shouldSpawnBird = GlobalRandom.Instance.Next(BirdChance) == 0;
            return shouldSpawnBird ? new Bird(speed) : new Cactus(speed);
        }
    }
}

