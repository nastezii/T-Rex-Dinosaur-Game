using ChromeDinoGame.Globals;
using ChromeDinoGame.Services;

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

            (Sprite, Width, Height) = SpriteMemoizer.SetSpriteCharacteristics(GetRandomCactusPath(), false);
            RenderEntity();
        }

        private string GetRandomCactusPath() => $"pack://application:,,,/Resources/cactus_{GlobalRandom.Instance.Next(1, 7)}.png";
    }
}
