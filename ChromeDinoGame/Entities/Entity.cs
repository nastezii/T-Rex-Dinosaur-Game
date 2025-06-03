using System.Windows.Controls;
using WpfAnimatedGif;
using System.Windows.Media.Imaging;
using ChromeDinoGame.Globals;

namespace ChromeDinoGame.Entities
{
    public abstract class Entity
    {
        protected double _speed;
        protected int _renderDepth;
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double PosX { get; protected set; }
        public double PosY { get; protected set; }
        public Image Sprite { get; protected set; }

        public bool IsInWindow() => PosX > - Width;

        public virtual void MoveEntity()
        {
            PosX -= _speed;
            Canvas.SetLeft(Sprite, PosX);
        }

        public void RenderEntity()
        {
            if (!GlobalCanvas.GameArea.Children.Contains(Sprite))
            {
                GlobalCanvas.GameArea.Children.Add(Sprite);
                Canvas.SetLeft(Sprite, PosX);
                Canvas.SetBottom(Sprite, PosY);
                Canvas.SetZIndex(Sprite, _renderDepth);
            }
        }

        public void RemoveEntity() => GlobalCanvas.GameArea.Children.Remove(Sprite);

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
                var bitmapImage = new BitmapImage(new Uri(path));
                Sprite.Source = bitmapImage;

                Width = bitmapImage.Width;
                Height = bitmapImage.Height;
            }
        }
    }
}
