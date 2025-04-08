using System.Windows.Controls;
using WpfAnimatedGif;
using System.Windows.Media.Imaging;

namespace ChromeDinoGame.Entities.Obstacles
{
    class Bird : Obstacle
    {
        public Bird(Random random, double canvasWidth, double canvasHeight)
        {
            Sprite = new Image();

            var gifUri = new Uri("pack://application:,,,/Resources/bird_fly.gif");
            var image = new BitmapImage(gifUri);
            ImageBehavior.SetAnimatedSource(Sprite, image);

            Width = image.PixelWidth;
            Height = image.PixelHeight;
            PosY = random.Next(150, 200);
            PosX = canvasWidth - Width;
        }
    }
}
