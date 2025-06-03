using ChromeDinoGame.Globals;

namespace ChromeDinoGame.Entities
{
    class Cactus : Obstacle
    {
        public Cactus(double speed)
        {
            PosX = GlobalCanvas.GameArea.Width - Width;
            PosY = Characteristics.LineOfGround;
            _speed = speed;
            _renderDepth = Characteristics.ObstacleRenderDepth;

            SetSpriteCharacteristics($"pack://application:,,,/Resources/cactus_{GlobalRandom.Instance.Next(1, 7)}.png", false);
            RenderEntity();
        }
    }
}
