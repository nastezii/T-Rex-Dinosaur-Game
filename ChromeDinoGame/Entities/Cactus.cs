namespace ChromeDinoGame.Entities
{
    class Cactus : Obstacle
    {
        public Cactus(Random random, double canvasWidth, double canvasHeight, double lineOfGround, double speed)
        {
            SetSpriteCharacteristics($"pack://application:,,,/Resources/cactus_{random.Next(1, 7)}.png", false);

            PosX = canvasWidth - Width;
            PosY = lineOfGround;
            Speed = speed;
        }
    }
}
