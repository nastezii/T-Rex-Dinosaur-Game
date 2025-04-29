using ChromeDinoGame.Services;
using System.Windows;
using System.Windows.Input;

namespace ChromeDinoGame
{
    public partial class MainWindow : Window
    {
        private GameManager _gameManager;
        private bool _isGameStarted = false;

        public MainWindow()
        {
            InitializeComponent();

            KeyUp += MainWindow_KeyUp;
            KeyDown += MainWindow_KeyDown;
            Focusable = true;
            Focus();

            _gameManager = new GameManager(GameCanvas);
            _gameManager.SetStartCharacteristics();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_gameManager.ObjectHandler.Dino.IsVinner)
            {
                if (e.Key == Key.Enter && !_isGameStarted)
                {
                    _isGameStarted = true;
                    _gameManager.StartGame();
                    _gameManager.ObjectHandler.Run();
                }
                else if (e.Key == Key.Enter && !_gameManager.ObjectHandler.Dino.IsAlive)
                {
                    _gameManager.RestartGame();
                }
                else if (e.Key == Key.Up && _isGameStarted)
                {
                    _gameManager.ObjectHandler.Jump();
                }
                else if (e.Key == Key.Down && _isGameStarted)
                {
                    _gameManager.ObjectHandler.Crouch();
                }
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down && _isGameStarted && !_gameManager.ObjectHandler.Dino.IsVinner)
            {
                _gameManager.ObjectHandler.Run(); 
            }
        }
    }
}
