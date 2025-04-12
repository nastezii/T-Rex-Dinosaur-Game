
namespace ChromeDinoGame.Entities
{
    class Dino : Entity
    {
        private const string _rinningGifPath = "pack://application:,,,/Resources/dino_run.gif";
        private const string _crouchingGifPath = "pack://application:,,,/Resources/dino_crouch.gif";
        private const string _startImagePath = "pack://application:,,,/Resources/dino_start.png";
        private const string _endImagePath = "pack://application:,,,/Resources/dino_dead.png";
        private readonly double _minDinoYValue;
        private bool _isJumping = false;
        private double _gravity = 0.05;
        

        public Dino(double lineOfGround, double canvasWidth, double speed)
        {
            SetRunningSprite();

            _minDinoYValue = canvasWidth - lineOfGround - Height;

            PosY = _minDinoYValue;
            PosX = 50;
            Speed = speed;
        }

        public void Jump()
        {
            if (!_isJumping)
            { 
                _isJumping = true;

                MoveObject();
            }
        }

        public override void MoveObject()
        {
            if (PosY >= _minDinoYValue)
            {
                _isJumping = false;
                PosY = _minDinoYValue;
            }

            if (_isJumping)
            {
                PosY += Speed;
                Speed += _gravity;
            }
        }

        public void SetRunningSprite() => SetSpriteCharacteristics(_rinningGifPath, true);
        public void SetCrouchSprite() => SetSpriteCharacteristics(_crouchingGifPath, true);
        public void SetStartSprite() => SetSpriteCharacteristics(_startImagePath, false);
        public void SetEndSprite() => SetSpriteCharacteristics(_endImagePath, false);
    }
}
