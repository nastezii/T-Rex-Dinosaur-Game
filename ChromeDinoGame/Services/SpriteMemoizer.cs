using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace ChromeDinoGame.Services
{
    public static class SpriteMemoizer
    {
        private static readonly Dictionary<(string path, bool isGif), BitmapImage> _bitmapCache = new();

        public static (Image image, double width, double height) SetSpriteCharacteristics(string path, bool isGif)
        {
            if (!_bitmapCache.TryGetValue((path, isGif), out var bitmap))
            {
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze(); 

                _bitmapCache[(path, isGif)] = bitmap;
            }

            var sprite = new Image();

            if (isGif)
                ImageBehavior.SetAnimatedSource(sprite, bitmap);
            else
                sprite.Source = bitmap;

            return (sprite, bitmap.Width, bitmap.Height);
        }
    }
}
