using ChromeDinoGame.Services;
using System.Windows;
using System.Windows.Input;

namespace ChromeDinoGame
{
    public partial class MainWindow : Window
    {
        private GameManager _gameManager;

        public MainWindow()
        {
            InitializeComponent();

            _gameManager = new GameManager(GameCanvas);
            _gameManager.StartGame();

            KeyUp += MainWindow_KeyUp;
            KeyDown += MainWindow_KeyDown;
            Focusable = true;
            Focus(); 
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                _gameManager.Jump();
            }
            else if (e.Key == Key.Down)
            {
                _gameManager.Crouch();
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                _gameManager.Run(); 
            }
        }
    }
}
