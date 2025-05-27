using System.Windows.Controls;

namespace ChromeDinoGame.Services
{
    public static class GlobalCanvas
    {
        public static Canvas GameArea { get; private set; }

        public static void Initialize(Canvas canvas)
        {
            if (GameArea != null)
                throw new InvalidOperationException("GameArea has already been initialized.");

            GameArea = canvas;
        }
    }
}
