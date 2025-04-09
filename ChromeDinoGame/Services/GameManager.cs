using System.Windows.Controls;
using System.Windows.Threading;
using ChromeDinoGame.Entities.Obstacles;

namespace ChromeDinoGame.Services
{
    class GameManager
    {
        private Canvas _canvas;
        private DispatcherTimer _gameTimer;
        private ObstaclesGenerator _obstaclesGenerator;

        public GameManager(Canvas canvas) 
        {
            _canvas = canvas;
        }
    }
}
