using ChromeDinoGame.Globals;
using ChromeDinoGame.Services;

namespace ChromeDinoGame.Entities
{
    class Cloud : Entity
    {
        public Cloud(double speed)
        {
            PosX = GlobalCanvas.GameArea.Width;
            PosY = GlobalRandom.Instance.Next(180, 300);
            _speed = speed;
            _renderDepth = Characteristics.CloudRenderDepth;

            (Sprite, Width, Height) = SpriteMemoizer.SetSpriteCharacteristics($"pack://application:,,,/Resources/cloud.png", false);
            RenderEntity();
        }
    }
}
