using System.Windows.Controls;
using System.Windows.Threading;
using ChromeDinoGame.Entities;

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
        private double _backGroundObjSpeed = 5;

        public GameManager(Canvas canvas) 
        {
            _canvas = canvas;
            _random = new Random();
            _gameTimer = new DispatcherTimer();
            _objectHandler = new ObjectHandler(_random, _canvas, new Dino(_lineOfGround, _canvas.Width, _canvas.Height), _backGroundObjSpeed, _lineOfGround);
            _highScores = new List<double>();
        }

        public void StartGame()
        {
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            InitializeStartWindow();
            _gameTimer.Tick += GameLoop;
            _gameTimer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (!_objectHandler.CheckCollision())
            {
                _objectHandler.UpdateEntitites();
            }
            else
                EndGame();
        }

        private void InitializeStartWindow()
        {
            _objectHandler.AddRoad();
            _objectHandler.AddCloud();
        }

        private void CalculateScore() => _score += _backGroundObjSpeed;

        public void EndGame()
        { 
            PauseGame();
            _highScores.Add(_score);
            _score = 0;
        }

        public void PauseGame() => _gameTimer.Stop();
    }
}
