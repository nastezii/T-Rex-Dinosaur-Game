﻿using ChromeDinoGame.Globals;
using ChromeDinoGame.Services;
using System.Windows.Controls;

namespace ChromeDinoGame.Entities
{
    class Dino : Entity
    {
        private static Dino _dino;
        private const double LineOfGround = Characteristics.LineOfGround;
        private const double JumpSpeed = Characteristics.JumpSpeed; 
        private const double Gravity = Characteristics.Gravity;

        public bool IsRunning { get; private set; } = false;
        public bool IsJumping { get; private set; } = false;
        public bool IsCrouching { get; private set; } = false;
        public bool IsWinner { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public bool IsAlive { get; set; } = true;

        private Dino()
        {
            _speed = JumpSpeed;
            PosY = LineOfGround;
            PosX = Characteristics.PlayerPosX;
            _renderDepth = Characteristics.DinoRenderDepth;

            SetIdleSprite();
        }

        public static Dino Instance
        {
            get
            {
                if (_dino == null)
                    _dino = new Dino();

                return _dino;
            }
        }

        public void Jump()
        {
            if (!IsJumping && IsActive)
            {
                IsRunning = false;
                IsCrouching = false;
                IsJumping = true;

                RemoveEntity();
                SetIdleSprite();
                RenderEntity();
                SoundManager.PlaySound(SoundManager.SoundType.Jump);
            }
        }

        public void Crouch()
        {
            if (!IsCrouching && !IsJumping && IsActive)
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
            if (!IsRunning && IsActive)
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
            RemoveEntity();
            SetIdleSprite();
            RenderEntity();
        }

        public void SetDeadCharacteristics()
        {
            IsCrouching = false;
            IsJumping = false;
            IsRunning = false;
            IsActive = false;
            IsAlive = false;
            RemoveEntity() ;
            SetDeadSprite();
            RenderEntity();
        }

        public void ToggleDinoPause(bool isPaused)
        {
            if (isPaused)
            {
                IsActive = false;
                SetIdleState();
            }
            else 
            {
                if (IsRunning)
                {
                    RemoveEntity();
                    SetRunningSprite();
                    RenderEntity();
                }
                IsActive = true;
            }
        }

        public void SetWinState()
        {
            IsWinner = true;
            IsActive = false;
            ToggleDinoPause(true);
        }

        public void ReviveDino()
        {
            IsActive = true;
            IsAlive = true;
            PosY = LineOfGround;
            Run();
        }

        public override void MoveEntity()
        {
            if (IsJumping)
            {
                if (PosY + _speed <= LineOfGround)
                {
                    IsJumping = false;
                    _speed = JumpSpeed;
                    PosY = LineOfGround;
                    Canvas.SetBottom(Sprite, PosY);
                    Run();
                }
                else
                {
                    PosY += _speed;
                    _speed -= Gravity;
                    Canvas.SetBottom(Sprite, PosY);
                }
            }
        }

        private void SetRunningSprite() => (Sprite, Width, Height) = SpriteMemoizer.SetSpriteCharacteristics("pack://application:,,,/Resources/dino_run.gif", true);
        private void SetCrouchingSprite() => (Sprite, Width, Height) = SpriteMemoizer.SetSpriteCharacteristics("pack://application:,,,/Resources/dino_crouch.gif", true);
        private void SetIdleSprite() => (Sprite, Width, Height) = SpriteMemoizer.SetSpriteCharacteristics("pack://application:,,,/Resources/dino_start.png", false);
        private void SetDeadSprite() => (Sprite, Width, Height) = SpriteMemoizer.SetSpriteCharacteristics("pack://application:,,,/Resources/dino_dead.png", false);
    }
}
