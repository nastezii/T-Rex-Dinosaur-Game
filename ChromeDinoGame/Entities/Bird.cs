
namespace ChromeDinoGame.Entities
{
    class Bird : Entity
    {
        public Bird(Random random, double canvasWidth, double canvasHeight)
        {
            SetSpriteCharacteristics("pack://application:,,,/Resources/bird_fly.gif", true);

            PosY = random.Next(150, 200);
            PosX = canvasWidth - Width;
        }
    }
}
