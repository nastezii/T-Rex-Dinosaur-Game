using System.Windows.Controls;
using System.Windows.Threading;
using ChromeDinoGame.Entities;

namespace ChromeDinoGame.Services
{
    class GameManager
    {
        public Dino Dino { get; protected set; }
        private Canvas _canvas;
        private Random _random;
        private DispatcherTimer _gameTimer;
        private Action _endGameCallback;
        EntityHandler _entityHandler;
        ScoreManager _scoreManager;
        UIManager _uiManager;

        private double _speedOfEntities = 10;
        private double _speedInc = 0.00001;
        private double _lineOfGround = 16;

        public GameManager(Canvas canvas, Action endGameCallBack)
        {
            _canvas = canvas;
            Dino = new Dino(_canvas, _lineOfGround, _speedOfEntities);
            _random = new Random();
            _endGameCallback = endGameCallBack;
            _entityHandler = new EntityHandler(_canvas, Dino, _random, EndGame, _speedOfEntities, _lineOfGround, _speedInc);
            _scoreManager = new ScoreManager();
            _uiManager = new UIManager(_canvas);
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(3);
            _gameTimer.Tick += GameLoop;
        }

        public void StartGame()
        {
            Dino.Run();
            _uiManager.UpdateStartInfoBlock(false);
            _gameTimer.Start();
        }

        public void InitializeGame()
        { 
            _entityHandler.InitializeStartWindow();
            _scoreManager.SetScores();
            _uiManager.DisplayPauseInfBlock();
            _uiManager.UpdateScoreBlock(_scoreManager.CurrentScore, _scoreManager.HighestScore);
            _uiManager.UpdateStartInfoBlock(true);
        }

        public void RestartGame()
        {
            _canvas.Children.Clear();
            _entityHandler.SetReplayCharacteristics();
            _entityHandler.InitializeStartWindow();
            _scoreManager.SetScores();
            _uiManager.UpdateScoreBlock(_scoreManager.CurrentScore, _scoreManager.HighestScore);
            _uiManager.DisplayPauseInfBlock();
            Dino.ReviveDino();
            _gameTimer.Start();
        }

        public void togglePause(bool isPaused)
        {
            Dino.ToggleDinoPause(isPaused);

            if (isPaused)
            {
                _gameTimer.Stop();
            }
            else
            {
                _gameTimer.Start();
            }
        }

        public void EndGame()
        {
            _gameTimer.Stop();
            Dino.SetDeadCharacteristics();
            _uiManager.UpdateReplayInfoBlock(true);
            _endGameCallback.Invoke();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            _speedOfEntities += _speedInc;
            _entityHandler.UpdateEntities();
            _scoreManager.UpdateScores(_speedOfEntities);
            _uiManager.UpdateScoreBlock(_scoreManager.CurrentScore, _scoreManager.HighestScore);
        }
    }
}
