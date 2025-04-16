
namespace ChromeDinoGame.Entities
{
    class Dino : Entity
    {
        private const string _rinningGifPath = "pack://application:,,,/Resources/dino_run.gif";
        private const string _crouchingGifPath = "pack://application:,,,/Resources/dino_crouch.gif";
        private const string _startImagePath = "pack://application:,,,/Resources/dino_start.png";
        private const string _endImagePath = "pack://application:,,,/Resources/dino_dead.png";
        public bool IsRunning { get; private set; } = false;
        public bool IsJumping { get; private set; } = false;
        public bool IsCrouching { get; private set; } = false;

        private readonly double _lineOfGround;
        private double _gravity = 0.05;

        public Dino(double canvasWidth, double canvasHeight, double lineOfGround, double speed)
        {
            SetStartSprite();

            Speed = speed;
            PosY = lineOfGround;
            PosX = 50;
        }

        public override void MoveObject()
        {
            if (IsJumping)
            {
                if (PosY >= _lineOfGround)
                {
                    IsJumping = false;
                    PosY = _lineOfGround;
                }
                else
                {
                    PosY += Speed;
                    Speed -= _gravity;
                }
            }
        }

        public void Jump()
        {
            IsRunning = false;
            IsCrouching = false;

            if (!IsJumping)
            {
                IsJumping = true;
                SetStartSprite();

                MoveObject();
            }
        }

        public void Crouch()
        {
            IsJumping = false;
            IsRunning = false;
            SetCrouchSprite();
        }

        public void Run()
        {
            IsCrouching = false;
            IsJumping = false;
            SetRunningSprite();
        }

        public void Dead()
        {
            IsCrouching = false;
            IsJumping = false;
            IsRunning = false;
            SetEndSprite();
        }

        private void SetRunningSprite() => SetSpriteCharacteristics(_rinningGifPath, true);
        private void SetCrouchSprite() => SetSpriteCharacteristics(_crouchingGifPath, true);
        private void SetStartSprite() => SetSpriteCharacteristics(_startImagePath, false);
        private void SetEndSprite() => SetSpriteCharacteristics(_endImagePath, false);
    }
}
