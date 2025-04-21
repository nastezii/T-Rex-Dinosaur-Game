using System.Windows.Controls;
using System.Windows.Threading;

namespace ChromeDinoGame.Services
{
    class GameManager
    {
        private Canvas _canvas;
        private Random _random;
        private DispatcherTimer _gameTimer;
        private List<double> _scoresList;
        private double _score = 0;
        private readonly double _lineOfGround = 16;
        private double _speedOfEntities = 8;
        public ObjectHandler ObjectHandler { get; private set; }

        public GameManager(Canvas canvas) 
        {
            _canvas = canvas;
            _random = new Random();
            _gameTimer = new DispatcherTimer();
            ObjectHandler = new ObjectHandler(_random, _canvas, _speedOfEntities, _lineOfGround);
            _scoresList = new List<double>();

            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(3);
            _gameTimer.Tick += GameLoop;
        }

        public void StartGame() => _gameTimer.Start();

        public void SetStartCharacteristics()
        {
            _score = 0;
            _speedOfEntities = 8;

            if (_scoresList.Count == 0)
                ObjectHandler.InitializeStartWindow(_score);
            else
                ObjectHandler.InitializeStartWindow(_score, _scoresList.Max());
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (!ObjectHandler.CheckCollision())
            {
                ObjectHandler.UpdateEntitites();
                CalculateScore();
                ObjectHandler.UpdateScore(_score);
                ObjectHandler.IncreaseSpeed(0.00001);
            }
            else
            {
                ObjectHandler.DinoDead();
                EndGame();
                _scoresList.Add(_score);
            }  
        }

        private void CalculateScore() => _score += _speedOfEntities;

        private void EndGame()
        {
            _gameTimer.Stop();
            _scoresList.Add(_score);
        }
    }
}
