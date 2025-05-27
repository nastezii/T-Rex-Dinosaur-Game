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

            _gameManager = new GameManager(EndGame);
            GlobalCanvas.Initialize(GameCanvas);
            _gameManager.InitializeGame();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                HandleEnterKey();
            else if (e.Key == Key.Up)
                HandleUpKey();
            else if (e.Key == Key.Down)
                HandleDownKey();
            else if (e.Key == Key.P)
                HandlePauseKey();
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down && _isGameActive && !_isPaused)
                _gameManager.Dino.Run();
        }

        private void HandleEnterKey()
        {
            if (!_isGameStarted)
            {
                _isGameStarted = true;
                _isGameActive = true;
                _gameManager.StartGame();
            }
            else if (!_isGameActive && !_isPaused)
            {
                _isGameActive = true;
                _gameManager.RestartGame();
            }
        }

        private void HandleUpKey()
        {
            if (_isGameActive && !_isPaused)
                _gameManager.Dino.Jump();
        }

        private void HandleDownKey()
        {
            if (_isGameActive && !_isPaused)
                _gameManager.Dino.Crouch();
        }

        private void HandlePauseKey()
        {
            if (_isGameStarted && _gameManager.Dino.IsAlive)
            {
                if (_isPaused)
                {
                    _isPaused = false;
                    _isGameActive = true;
                }
                else
                {
                    _isPaused = true;
                    _isGameActive = false;
                }
                _gameManager.TogglePause(_isPaused);
            }
        }

        private void EndGame()
        {
            _isGameActive = false ; 
            _isPaused = false ;
        }
    }
}
