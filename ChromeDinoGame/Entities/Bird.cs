using System.Windows.Controls;
using ChromeDinoGame.Services;

namespace ChromeDinoGame.Entities
{
    class Bird : Obstacle
    {
        public Bird(Random random, double speed) 
        {
            PosY = random.Next(70, 100);
            PosX = GlobalCanvas.GameArea.Width - Width;
            _speed = speed;

            SetSpriteCharacteristics("pack://application:,,,/Resources/bird_fly.gif", true);
        }
    }
}
