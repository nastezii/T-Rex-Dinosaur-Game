using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace ChromeDinoGame.Services
{
    public static class SpriteMemoizer
    {
        private static Dictionary<(string path, bool isGif),
            (Image image, double width, double height)> _cache = new();

        public static (Image image, double width, double height) SetSpriteCharacteristics(string path, bool isGif)
        {
            if (_cache.TryGetValue((path, isGif), out var cached))
            {
                return cached;
            }

            var sprite = new Image();
            var bitmap = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));

            if (isGif)
                ImageBehavior.SetAnimatedSource(sprite, bitmap);
            else
                sprite.Source = bitmap;

            double width = bitmap.Width;
            double height = bitmap.Height;

            var result = (sprite, width, height);
            _cache[(path, isGif)] = result;
            return result;
        }
    }
}
