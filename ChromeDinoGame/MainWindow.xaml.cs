using ChromeDinoGame.Services;
using System.Windows;
using System.Windows.Input;

namespace ChromeDinoGame
{
    public partial class MainWindow : Window
    {
        private ObjectHandler _objectHandler;

        public MainWindow()
        {
            InitializeComponent();
            GameManager _gameManager = new GameManager(GameCanvas);
            _gameManager.StartGame();
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                _objectHandler.Jump();
            else if (e.Key == Key.Down)
                _objectHandler.Crouch();
            else
                _objectHandler.Run();
        }
    }
}