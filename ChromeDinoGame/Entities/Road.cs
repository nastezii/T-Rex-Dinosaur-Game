
namespace ChromeDinoGame.Entities
{
    class Road : Entity
    {
        public Road(Random random, double speed, double x, double y)
        {
            SetSpriteCharacteristics($"pack://application:,,,/Resources/road_{random.Next(1, 4)}.png", false);

            PosX = x; 
            PosY = y;
            Speed = speed;
        }
    }
}
