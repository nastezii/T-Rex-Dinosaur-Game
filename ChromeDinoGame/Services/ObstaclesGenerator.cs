using ChromeDinoGame.Entities;

namespace ChromeDinoGame.Services
{
    class ObstaclesGenerator
    {
        private static Random _random = new Random();
        private readonly double _canvasWidth;
        private readonly double _canvasHeight;
        private readonly double _lineOfGround;
        private double _speed;

        public ObstaclesGenerator(double speed, double canvasWidth, double canvasHeight, double lineOfGround)
        {
            _speed = speed;
            _canvasHeight = canvasHeight;
            _canvasWidth = canvasWidth;
            _lineOfGround = lineOfGround;
        }

        public Obstacle GenerateObstacle()
        {
            if (_random.Next(0, 2) == 1)
                return new Cactus(_random, _canvasWidth, _canvasHeight, _lineOfGround, _speed);
            else
                return new Bird(_random, _canvasWidth,  _speed);
        }
    }
}
