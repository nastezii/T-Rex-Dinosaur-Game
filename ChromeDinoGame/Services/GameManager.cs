using System.Windows.Controls;
using System.Windows.Threading;

namespace ChromeDinoGame.Services
{
    class GameManager
    {
        private Canvas _canvas;
        private Random _random;
        private DispatcherTimer _gameTimer;
        private ObjectHandler _objectHandler;
        private List<double> _highScores;
        private double _score = 0;
        private readonly double _lineOfGround = 15;
        private double _SpeedOfEntities = 5;

        public GameManager(Canvas canvas) 
        {
            _canvas = canvas;
            _random = new Random();
            _gameTimer = new DispatcherTimer();
            _objectHandler = new ObjectHandler(_random, _canvas, _SpeedOfEntities, _lineOfGround);
            _highScores = new List<double>();
        }

        public void StartGame()
        {
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(15);
            _objectHandler.InitializeStartWindow();
            _gameTimer.Tick += GameLoop;
            _gameTimer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (!_objectHandler.CheckCollision())
            {
                _objectHandler.UpdateEntitites();
                CalculateScore();
            }
            else
            {
                EndGame();
                _highScores.Add(_score);
            }  
        }

        private void CalculateScore() => _score += _SpeedOfEntities;

        private void PauseGame() => _gameTimer.Stop();

        private void EndGame()
        { 
            PauseGame();
            _highScores.Add(_score);
            _score = 0;
        }
    }
}
