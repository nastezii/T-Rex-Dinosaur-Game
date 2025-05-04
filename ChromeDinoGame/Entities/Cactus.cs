using System.Windows.Controls;

namespace ChromeDinoGame.Entities
{
    class Cactus : Obstacle
    {
        public Cactus(Canvas canvas, Random random, double canvasWidth, double canvasHeight, double lineOfGround, double speed)
        {
            _canvas = canvas;
            PosX = canvasWidth - Width;
            PosY = lineOfGround;
            _speed = speed;

            SetSpriteCharacteristics($"pack://application:,,,/Resources/cactus_{random.Next(1, 7)}.png", false);
            SetCollisionBox();
        }
    }
}
