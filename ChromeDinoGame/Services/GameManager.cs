using System.Windows.Controls;
using System.Windows.Threading;

namespace ChromeDinoGame.Services
{
    class GameManager
    {
        private Canvas _canvas;
        private Random _random;
        private DispatcherTimer _gameTimer;
        private List<double> _scoresHistory;
        private double _currentScore = 0;
        private readonly double _lineOfGround = 16;
        private double _speedOfEntities = 8;
        public ObjectHandler ObjectHandler { get; private set; }

        public GameManager(Canvas canvas) 
        {
            _canvas = canvas;
            _random = new Random();
            _gameTimer = new DispatcherTimer();
            ObjectHandler = new ObjectHandler(_random, _canvas, _speedOfEntities, _lineOfGround);
            _scoresHistory = new List<double>();
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(3);
            _gameTimer.Tick += GameLoop;
        }

        public void StartGame() => _gameTimer.Start();

        public void SetStartCharacteristics() => ObjectHandler.InitializeStartWindow(_currentScore);

        public void RestartGame()
        {
            _currentScore = 0;
            ObjectHandler.RestartGame(_currentScore, _scoresHistory.Max());
            _gameTimer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (!ObjectHandler.CheckCollision())
            {
                ObjectHandler.UpdateEntities();
                CalculateScore();

                if (_scoresHistory.Count != 0)
                    ObjectHandler.UpdateGameScores(_currentScore, _scoresHistory.Max());
                else
                    ObjectHandler.UpdateGameScores(_currentScore);

                _speedOfEntities += 0.00001;
                ObjectHandler.UpdateSpeed(_speedOfEntities);
            }
            else
            {
                ObjectHandler.HandleDinoDeath();
                _gameTimer.Stop();
                _scoresHistory.Add(_currentScore);
            }  
        }

        private void CalculateScore() => _currentScore += _speedOfEntities;
    }
}
