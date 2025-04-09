using System.Windows.Controls;
using WpfAnimatedGif;
using System.Windows.Media.Imaging;

namespace ChromeDinoGame.Entities
{
    public abstract class GameEntity
    {
        public Image Sprite { get; protected set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double PosX { get; protected set; }
        public double PosY { get; protected set; }

        protected void SetSpriteCharacteristics(string path, bool isGif)
        {
            Sprite = new Image();

            if (isGif)
            {
                var gifImage = new BitmapImage(new Uri(path));
                ImageBehavior.SetAnimatedSource(Sprite, gifImage);

                Width = gifImage.PixelWidth;
                Height = gifImage.PixelHeight;
            }
            else
            {
                var bitmapImage = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                Sprite.Source = bitmapImage;

                Width = bitmapImage.Width;
                Height = bitmapImage.Height;
            }
        }

    }
}
