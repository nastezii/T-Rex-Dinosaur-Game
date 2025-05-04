using System.Windows.Controls;

namespace ChromeDinoGame.Entities
{
    class Cloud : Entity
    {
        public Cloud(Canvas canvas, double x, double y, double speed)
        {
            _canvas = canvas;
            PosX = x;
            PosY = y;
            _speed = speed;

            SetSpriteCharacteristics($"pack://application:,,,/Resources/cloud.png", false);
        }
    }
}
