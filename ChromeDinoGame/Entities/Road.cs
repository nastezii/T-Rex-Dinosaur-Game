using ChromeDinoGame.Globals;
using ChromeDinoGame.Services;

namespace ChromeDinoGame.Entities
{
    class Road : Entity
    {
        public Road(double speed, double x, double y)
        {
            PosX = x; 
            PosY = y;
            _speed = speed;
            _renderDepth = Characteristics.RoadRenderDepth;

            (Sprite, Width, Height) = SpriteMemoizer.SetSpriteCharacteristics($"pack://application:,,,/Resources/road.png", false);
            RenderEntity();
        }

        public bool IsNearWindowEnd() => PosX < - Width + GlobalCanvas.GameArea.Width + 20;
    }
}
