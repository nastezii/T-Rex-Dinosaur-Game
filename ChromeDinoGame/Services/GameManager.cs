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
        private readonly double _lineOfGround = 16;
        private double _SpeedOfEntities = 8;

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
            _gameTimer.Interval = TimeSpan.FromMilliseconds(3);
            _objectHandler.InitializeStartWindow();
            _gameTimer.Tick += GameLoop;
            _gameTimer.Start();
        }

        public void Jump() => _objectHandler.Jump();
        public void Run() => _objectHandler.Run();
        public void Crouch() => _objectHandler.Crouch();

        private void GameLoop(object sender, EventArgs e)
        {
            if (!_objectHandler.CheckCollision())
            {
                _objectHandler.UpdateEntitites();
                CalculateScore();
                _objectHandler.IncreaseSpeed(0.00001);
            }
            else
            {
                _objectHandler.DinoDead();
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
        }
    }
}
