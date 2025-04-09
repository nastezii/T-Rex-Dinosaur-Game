using System.Windows;
using System.Windows.Controls;
using ChromeDinoGame.Entities.Obstacles;

namespace ChromeDinoGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddBirdToCanvas();
        }

        private void AddBirdToCanvas()
        {
            ObstaclesGenerator gen = new ObstaclesGenerator(600, 350, 31);
            for (int i = 0; i < 3; i++)
            {
                Obstacle obj = gen.GenerateObstacle();
                GameCanvas.Children.Add(obj.Sprite);

                Canvas.SetLeft(obj.Sprite, obj.PosX);
                Canvas.SetTop(obj.Sprite, obj.PosY);
            }
        }
    }
}