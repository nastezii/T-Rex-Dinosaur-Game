using System.Windows.Controls;
using System.Windows.Threading;

namespace ChromeDinoGame.Services
{
    class GameManager
    {
        private Canvas _canvas;
        private Random _random;
        private DispatcherTimer _gameTimer;
        private Action _endGameCallback;
        EntityHandler _entityHandler;
        ScoreManager _scoreManager;
        UIManager _uiManager;

        private double _speedOfEntities = 8;
        private double _speedInc = 0.00001;
        private double _lineOfGround = 20;

        public GameManager(Canvas canvas, Action endGameCallBack)
        {
            _canvas = canvas;
            _random = new Random();
            _endGameCallback = endGameCallBack;
            _entityHandler = new EntityHandler(_random, _canvas, EndGame, _speedOfEntities, _lineOfGround, _speedInc);
            _scoreManager = new ScoreManager(_speedInc);
            _uiManager = new UIManager(_canvas);
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(3);
            _gameTimer.Tick += GameLoop;
        }

        public void StartGame()
        {
            _uiManager.UpdateStartInfoBlock(false);
            _gameTimer.Start();
        }

        public void InitializeGame()
        { 
            _entityHandler.InitializeStartWindow();
            _scoreManager.SetScores();
            _uiManager.UpdateScoreBlock(_scoreManager.CurrentScore, _scoreManager.HighestScore);
            _uiManager.UpdateStartInfoBlock(true);
        }

        public void RestartGame()
        {
            _canvas.Children.Clear();
            _entityHandler.InitializeStartWindow();
            _scoreManager.SetScores();
            _uiManager.UpdateScoreBlock(_scoreManager.CurrentScore, _scoreManager.HighestScore);
            _gameTimer.Start();
        }

        public void EndGame()
        { 
            _gameTimer.Stop();
            _uiManager.UpdateReplayInfoBlock(true);
            _scoreManager.SaveHighestScore();
            _endGameCallback.Invoke();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            _entityHandler.UpdateEntities();
            _scoreManager.UpdateScores();
            _uiManager.UpdateScoreBlock(_scoreManager.CurrentScore, _scoreManager.HighestScore);
        }
    }
}
