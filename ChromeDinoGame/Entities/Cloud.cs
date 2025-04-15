
namespace ChromeDinoGame.Entities
{
    class Cloud : Entity
    {
        public Cloud(double x, double y, double speed)
        {
            SetSpriteCharacteristics($"pack://application:,,,/Resources/cloud.png", false);

            PosX = x;
            PosY = y;
            Speed = speed;
        }
    }
}
