
namespace ChromeDinoGame.Entities
{
    class Cloud : Entity
    {
        public Cloud(double x, double y, Random random)
        {
            SetSpriteCharacteristics($"pack://application:,,,/Resources/road_{random.Next(1, 4)}.png", false);

            PosX = x;
            PosY = y;
        }
    }
}
