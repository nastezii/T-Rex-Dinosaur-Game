using ChromeDinoGame.Globals;

namespace ChromeDinoGame.Entities
{
    class Cloud : Entity
    {
        public Cloud(double speed)
        {
            PosX = GlobalCanvas.GameArea.Width;
            PosY = GlobalRandom.Instance.Next(180, 300);
            _speed = speed;

            SetSpriteCharacteristics($"pack://application:,,,/Resources/cloud.png", false);
        }
    }
}
