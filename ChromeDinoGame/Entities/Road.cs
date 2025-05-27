using System.Windows.Controls;

namespace ChromeDinoGame.Entities
{
    class Road : Entity
    {
        public Road(Random random, double speed, double x, double y)
        {
            PosX = x; 
            PosY = y;
            _speed = speed;

            SetSpriteCharacteristics($"pack://application:,,,/Resources/road_{random.Next(1, 4)}.png", false);
        }

        public override bool IsInWindow() => PosX > - Width + 25;
    }
}
