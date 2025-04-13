using ChromeDinoGame.Services;
using System.Windows;

namespace ChromeDinoGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GameManager gmm = new GameManager(GameCanvas);
            gmm.StartGame();
        }
    }
}