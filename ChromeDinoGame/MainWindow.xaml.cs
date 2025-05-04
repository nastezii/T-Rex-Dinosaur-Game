using ChromeDinoGame.Services;
using System.Windows;
using System.Windows.Input;

namespace ChromeDinoGame
{
    public partial class MainWindow : Window
    {
        private GameManager _gameManager;

        private bool _isGameStarted = false;
        private bool _isGameActive = false;
        private bool _isPaused = false;

        public MainWindow()
        {
            InitializeComponent();

            KeyUp += MainWindow_KeyUp;
            KeyDown += MainWindow_KeyDown;
            Focusable = true;
            Focus();

            _gameManager = new GameManager(GameCanvas, EndGame);
            _gameManager.InitializeGame();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!_isGameStarted)
                {
                    _isGameStarted = true;
                    _isGameActive = true;
                    _gameManager.StartGame();
                }
                else if (_isGameStarted && !_isGameActive)
                {
                    _isGameActive = true;
                    _gameManager.RestartGame();
                }
            }
            else if (e.Key == Key.Up && _isGameActive && !_isPaused)
            {
                _gameManager.Dino.Jump();
            }
            else if (e.Key == Key.Down && _isGameActive && !_isPaused)
            {
                _gameManager.Dino.Crouch();
            }
            else if (e.Key == Key.P)
            {
                if (_isPaused)
                    _isPaused = false;
                else
                    _isPaused = true;
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down && _isGameActive && !_isPaused)
            {
                _gameManager.Dino.Run();
            }
        }

        private void EndGame()
        {
            _isGameActive = false ; 
            _isPaused = false ;
        }
    }
}
