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

        public void SetStartCharacteristics()
        {
            _currentScore = 0;
            _speedOfEntities = 8;

            if (_scoresHistory.Count == 0)
                ObjectHandler.InitializeStartWindow(_currentScore);
            else
                ObjectHandler.InitializeStartWindow(_currentScore, _scoresHistory.Max());
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (!ObjectHandler.CheckCollision())
            {
                ObjectHandler.UpdateEntities();
                CalculateScore();
                ObjectHandler.IncrementGameSpeed(_currentScore);
                ObjectHandler.IncreaseSpeed(0.00001);
            }
            else
            {
                ObjectHandler.HandleDinoDeath();
                EndGame();
                _scoresHistory.Add(_currentScore);
            }  
        }

        private void CalculateScore() => _currentScore += _speedOfEntities;

        private void EndGame()
        {
            _gameTimer.Stop();
            _scoresHistory.Add(_currentScore);
        }
    }
}
