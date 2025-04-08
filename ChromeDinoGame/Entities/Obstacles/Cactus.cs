using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChromeDinoGame.Entities.Obstacles
{
    class Cactus : Obstacle
    {
        public Cactus(Random random, double canvasWidth, double canvasHeight, double lineOfGround)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri($"pack://application:,,,/Resources/cactus_{random.Next(1, 7)}.png"));

            Sprite = new Image
            {
                Source = bitmapImage,
            };

            Width = bitmapImage.Width;
            Height = bitmapImage.Height;
            PosX = canvasWidth - Width;
            PosY = canvasHeight - lineOfGround - Height;
        }
    }
}
