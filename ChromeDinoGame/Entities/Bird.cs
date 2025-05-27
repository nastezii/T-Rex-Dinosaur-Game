using ChromeDinoGame.Services;

namespace ChromeDinoGame.Entities
{
    class Bird : Obstacle
    {
        public Bird(double speed) 
        {
            PosY = GlobalRandom.Instance.Next(70, 100);
            PosX = GlobalCanvas.GameArea.Width - Width;
            _speed = speed;

            SetSpriteCharacteristics("pack://application:,,,/Resources/bird_fly.gif", true);
        }
    }
}
