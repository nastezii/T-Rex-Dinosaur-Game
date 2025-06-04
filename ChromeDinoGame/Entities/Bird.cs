using ChromeDinoGame.Globals;
using ChromeDinoGame.Services;

namespace ChromeDinoGame.Entities
{
    class Bird : Obstacle
    {
        public Bird(double speed) 
        {
            PosY = GlobalRandom.Instance.Next(50, 100);
            PosX = GlobalCanvas.GameArea.Width - Width;
            _speed = speed;
            _renderDepth = Characteristics.ObstacleRenderDepth;

            (Sprite, Width, Height) = SpriteMemoizer.SetSpriteCharacteristics("pack://application:,,,/Resources/bird_fly.gif", true);
            RenderEntity();
        }
    }
}
