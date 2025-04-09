using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChromeDinoGame.Entities
{
    class Cloud : MovableEntity
    {
        public Cloud(double x, double y, Random random)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri($"pack://application:,,,/Resources/road_{random.Next(1, 4)}.png"));

            Sprite = new Image
            {
                Source = bitmapImage,
            };

            PosX = x;
            PosY = y;
        }
    }
}
