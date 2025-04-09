using ChromeDinoGame.Entities;

namespace ChromeDinoGame.Services
{
    class ObstaclesGenerator
    {
        private static Random _random = new Random();
        private readonly double _canvasWidth;
        private readonly double _canvasHeight;
        private readonly double _lineOfGround;

        public ObstaclesGenerator(double canvasWidth, double canvasHeight, double lineOfGround)
        {
            _canvasHeight = canvasHeight;
            _canvasWidth = canvasWidth;
            _lineOfGround = lineOfGround;
        }

        public Entity GenerateObstacle()
        {
            if (_random.Next(0, 2) == 1)
                return new Cactus(_random, _canvasWidth, _canvasHeight, _lineOfGround);
            else
                return new Bird(_random, _canvasWidth, _canvasHeight);
        }
    }
}
