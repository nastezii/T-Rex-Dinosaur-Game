
namespace ChromeDinoGame.Entities
{
    class Dino : GameEntity
    {
        private const string _rinningGifPath = "pack://application:,,,/Resources/dino_run.gif";
        private const string _crouchingGifPath = "pack://application:,,,/Resources/dino_crouch.gif";
        private const string _startImagePath = "pack://application:,,,/Resources/dino_start.png";
        private const string _endImagePath = "pack://application:,,,/Resources/dino_dead.png";

        public Dino(double lineOfGround)
        {
            SetRunningSprite();

            PosY = lineOfGround;
            PosX = 50;
        }

        public void SetRunningSprite() => SetSpriteCharacteristics(_rinningGifPath, true);
        public void SetCrouchSprite() => SetSpriteCharacteristics(_crouchingGifPath, true);
        public void SetStartSprite() => SetSpriteCharacteristics(_startImagePath, false);
        public void SetEndSprite() => SetSpriteCharacteristics(_endImagePath, false);
    }
}
