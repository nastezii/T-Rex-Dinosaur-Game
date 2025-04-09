using System.Windows.Controls;
using WpfAnimatedGif;
using System.Windows.Media.Imaging;

namespace ChromeDinoGame.Entities.Obstacles
{
    class Bird : MovableEntity
    {
        public Bird(Random random, double canvasWidth, double canvasHeight)
        {
            SetSpriteCharacteristics("pack://application:,,,/Resources/bird_fly.gif", true);

            PosY = random.Next(150, 200);
            PosX = canvasWidth - Width;
        }
    }
}
