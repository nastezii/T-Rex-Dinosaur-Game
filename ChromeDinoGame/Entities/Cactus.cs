using ChromeDinoGame.Services;

namespace ChromeDinoGame.Entities
{
    class Cactus : Obstacle
    {
        public Cactus(double lineOfGround, double speed)
        {
            PosX = GlobalCanvas.GameArea.Width - Width;
            PosY = lineOfGround;
            _speed = speed;

            SetSpriteCharacteristics($"pack://application:,,,/Resources/cactus_{GlobalRandom.Instance.Next(1, 7)}.png", false);
        }
    }
}
