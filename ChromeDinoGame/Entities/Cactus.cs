using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChromeDinoGame.Entities.Obstacles
{
    class Cactus : MovableEntity
    {
        public Cactus(Random random, double canvasWidth, double canvasHeight, double lineOfGround)
        {
            SetSpriteCharacteristics($"pack://application:,,,/Resources/cactus_{random.Next(1, 7)}.png", false);

            PosX = canvasWidth - Width;
            PosY = canvasHeight - lineOfGround - Height;
        }
    }
}
