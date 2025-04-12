
namespace ChromeDinoGame.Entities
{
    class Cactus : Entity
    {
        public Cactus(Random random, double canvasWidth, double canvasHeight, double lineOfGround)
        {
            SetSpriteCharacteristics($"pack://application:,,,/Resources/cactus_{random.Next(1, 7)}.png", false);

            PosX = canvasWidth - Width;
            PosY = canvasHeight - lineOfGround - Height;

            isObstacle = true;
        }
    }
}
