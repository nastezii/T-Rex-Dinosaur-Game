using ChromeDinoGame.Globals;

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

            SetSpriteCharacteristics($"pack://application:,,,/Resources/road.png", false);
            RenderEntity();
        }

        public bool IsNearWindowEnd() => PosX < - Width + GlobalCanvas.GameArea.Width + 20;
    }
}
