using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChromeDinoGame.Entities.Obstacles
{
    class Cactus : Obstacle
    {
        private static Random _random = new Random();

        public Cactus(double x, double y) : base(x, y)
        {
            int num = _random.Next(1, 7);

            Sprite = new Image
            {
                Source = new BitmapImage(new Uri($"pack://application:,,,/Resources/cactus_{num}.png"))
            };
        }
    }
}
