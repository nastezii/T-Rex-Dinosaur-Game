using System.IO;
using System.Windows;
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
                try
                {
                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    bitmap.Freeze();

                    _bitmapCache[(path, isGif)] = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Failed to load sprite from path:\n\"{path}\"\n\nPlease check that the path is correct and the file exists.\n\nError: {ex.Message}",
                        "Sprite Load Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
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
