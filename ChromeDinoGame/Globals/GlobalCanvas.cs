using System.Windows.Controls;

namespace ChromeDinoGame.Globals
{
    public static class GlobalCanvas
    {
        public static Canvas GameArea { get; private set; }

        public static void Initialize(Canvas canvas)
        {
            if (GameArea != null)
                throw new InvalidOperationException("GlobalArea has already been initialized.");

            GameArea = canvas;
        }
    }
}
