
namespace ChromeDinoGame.Entities
{
    class Cloud : Entity
    {
        public Cloud(Random random, double x, double y, double speed)
        {
            SetSpriteCharacteristics($"pack://application:,,,/Resources/road_{random.Next(1, 4)}.png", false);

            PosX = x;
            PosY = y;
            Speed = speed;
        }
    }
}
