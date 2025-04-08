using System.Windows.Controls;

namespace ChromeDinoGame.Entities
{
    public abstract class GameEntity
    {
        public Image Sprite { get; protected set; }
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public double PosX { get; protected set; }
        public double PosY { get; protected set; }
    }
}
