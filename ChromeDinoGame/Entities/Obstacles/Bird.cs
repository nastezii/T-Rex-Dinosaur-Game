using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChromeDinoGame.Entities.Obstacles
{
    class Bird : Obstacle
    {
        public Bird(double x, double y) : base(x, y) 
        {
            Sprite = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Resources/bird_fly.gif"))
            };
        } 
    }
}
