using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChromeDinoGame.Entities
{
    class Cloud : MovableEntity
    {
        public Cloud(double x, double y, Random random)
        {
            SetSpriteCharacteristics($"pack://application:,,,/Resources/road_{random.Next(1, 4)}.png", false);

            PosX = x;
            PosY = y;
        }
    }
}
