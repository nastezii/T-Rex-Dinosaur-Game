using System.Media;
using System.Windows;

namespace ChromeDinoGame.Services
{
    public static class SoundManager
    {
        private static readonly SoundPlayer JumpSound;
        private static readonly SoundPlayer DeathSound;

        public enum SoundType
        {
            Jump,
            Death
        }

        static SoundManager()
        {
            try
            {
                var jumpStream = Application.GetResourceStream(
                    new Uri("pack://application:,,,/Resources/jump_sound.wav"))?.Stream;
                if (jumpStream != null)
                {
                    JumpSound = new SoundPlayer(jumpStream);
                    JumpSound.Load();
                }

                var deathStream = Application.GetResourceStream(
                    new Uri("pack://application:,,,/Resources/die_sound.wav"))?.Stream;
                if (deathStream != null)
                {
                    DeathSound = new SoundPlayer(deathStream);
                    DeathSound.Load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred while loading sounds:\n{ex.Message}",
                    "Sound Load Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public static void PlaySound(SoundType type)
        {
            try
            {
                switch (type)
                {
                    case SoundType.Jump:
                        JumpSound.Stop();
                        JumpSound.Play();
                        break;
                    case SoundType.Death:
                        DeathSound.Stop();
                        DeathSound.Play();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred while playing the {type.ToString().ToLower()} sound:\n{ex.Message}",
                    $"{type} Sound Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
