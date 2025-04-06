using System.Windows.Controls;
using WpfAnimatedGif;
using System.Windows.Media.Imaging;

namespace ChromeDinoGame.Entities.Obstacles
{
    class Bird : Obstacle
    {
        public Bird(double x, double y) : base(x, y) 
        {
            Sprite = new Image();

            var gifUri = new Uri("pack://application:,,,/Resources/bird_fly.gif");
            var image = new BitmapImage(gifUri);

            ImageBehavior.SetAnimatedSource(Sprite, image);
        }
    }
}
