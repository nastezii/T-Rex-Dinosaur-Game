using System.Windows;
using System.Windows.Controls;

namespace ChromeDinoGame.Entities
{
    class Dino : Entity
    {
        public bool IsRunning { get; private set; } = false;
        public bool IsJumping { get; private set; } = false;
        public bool IsCrouching { get; private set; } = false;
        public bool IsAlive { get; private set; } = true;
        public bool IsVinner { get; private set; } = false;
        public Rect CollisionBox { get; private set; }

        private readonly double _lineOfGround;
        private double _initialJumpSpeed;
        private double _gravity = 1;

        public Dino(Canvas canvas, double lineOfGround, double speed)
        {
            _canvas = canvas;
            _lineOfGround = lineOfGround;
            _speed = _initialJumpSpeed = speed * 2;
            PosY = lineOfGround;
            PosX = 50;

            SetIdleSprite();
        }

        public void Jump()
        {
            if (!IsJumping && IsAlive)
            {
                IsRunning = false;
                IsCrouching = false;
                IsJumping = true;

                RemoveEntity();
                SetIdleSprite();
                RenderEntity();
            }
        }

        public void Crouch()
        {
            if (!IsCrouching && !IsJumping && IsAlive)
            {
                IsRunning = false;
                IsCrouching = true;

                RemoveEntity();
                SetCrouchingSprite();
                RenderEntity();
            }
        }

        public void Run()
        {
            if (!IsRunning && IsAlive)
            {
                IsCrouching = false;
                IsRunning = true;

                RemoveEntity();
                SetRunningSprite();
                RenderEntity();
            }
        }

        public void SetIdleState()
        {
            SetIdleSprite();
            PosY = _lineOfGround;
            PosX = 50;
        }

        public void SetDeadCharacteristics()
        {
            IsCrouching = false;
            IsJumping = false;
            IsRunning = false;
            IsAlive = false;
            RemoveEntity() ;
            SetDeadSprite();
            RenderEntity();
        }

        public void ReviveDino()
        {
            IsAlive = true;
            PosY = _lineOfGround;
            Run();
        }

        public override void MoveEntity()
        {
            if (IsJumping)
            {
                if (PosY + _speed <= _lineOfGround)
                {
                    IsJumping = false;
                    _speed = _initialJumpSpeed;
                    PosY = _lineOfGround;
                    Canvas.SetBottom(Sprite, PosY);
                    Run();
                }
                else
                {
                    PosY += _speed;
                    _speed -= _gravity;
                    Canvas.SetBottom(Sprite, PosY);
                }
            }
        }

        public void SetCollisionBox() => CollisionBox = new Rect(PosX, PosY, Width - 20, Height - 20);
        private void SetRunningSprite() => SetSpriteCharacteristics("pack://application:,,,/Resources/dino_run.gif", true);
        private void SetCrouchingSprite() => SetSpriteCharacteristics("pack://application:,,,/Resources/dino_crouch.gif", true);
        private void SetIdleSprite() => SetSpriteCharacteristics("pack://application:,,,/Resources/dino_start.png", false);
        private void SetDeadSprite() => SetSpriteCharacteristics("pack://application:,,,/Resources/dino_dead.png", false);
    }
}
