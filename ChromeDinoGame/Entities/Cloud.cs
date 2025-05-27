using System.Windows.Controls;

namespace ChromeDinoGame.Entities
{
    class Cloud : Entity
    {
        public Cloud(double x, double y, double speed)
        {
            PosX = x;
            PosY = y;
            _speed = speed;

            SetSpriteCharacteristics($"pack://application:,,,/Resources/cloud.png", false);
        }
    }
}
