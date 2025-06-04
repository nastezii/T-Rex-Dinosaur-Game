using ChromeDinoGame.Services;

namespace ChromeDinoGame.Entities
{
    public abstract class Obstacle : Entity
    {
        public void CheckCollision() => CollisionChecker.CheckAndReact(this);
    }
}
