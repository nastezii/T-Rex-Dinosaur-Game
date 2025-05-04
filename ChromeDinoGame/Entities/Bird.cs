using System.Windows.Controls;

namespace ChromeDinoGame.Entities
{
    class Bird : Obstacle
    {
        public Bird(Canvas canvas, Random random, double canvasWidth, double speed) 
        {
            _canvas = canvas;
            PosY = random.Next(80, 100);
            PosX = canvasWidth - Width;
            _speed = speed;

            SetSpriteCharacteristics("pack://application:,,,/Resources/bird_fly.gif", true);
        }
    }
}
