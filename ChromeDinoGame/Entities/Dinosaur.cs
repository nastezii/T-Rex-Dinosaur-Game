using System.Windows.Controls;
using WpfAnimatedGif;
using System.Windows.Media.Imaging;

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
            SetSpriteCharacteristics( _startImagePath, false);

            PosY = lineOfGround;
            PosX = 50;
        }
    }
}
