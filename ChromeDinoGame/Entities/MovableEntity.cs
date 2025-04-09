
using ChromeDinoGame.Interfaces;

namespace ChromeDinoGame.Entities
{
    public abstract class MovableEntity : GameEntity, IMovable, IWindowAware
    {
        public double Speed { get; protected set; }

        public bool IsInWindow() => PosX + Sprite.ActualWidth < 0;
       
        public void MoveObject() => PosX -= Speed;
    }
}
