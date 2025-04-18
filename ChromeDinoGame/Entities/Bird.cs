
namespace ChromeDinoGame.Entities
{
    class Bird : Obstacle
    {
        public Bird(Random random, double canvasWidth, double speed)
        {
            SetSpriteCharacteristics("pack://application:,,,/Resources/bird_fly.gif", true);

            PosY = random.Next(80, 100);
            PosX = canvasWidth - Width;
            Speed = speed;
        }
    }
}
