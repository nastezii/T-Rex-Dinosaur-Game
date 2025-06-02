using System.Windows.Threading;
using ChromeDinoGame.Entities;
using ChromeDinoGame.Globals;

namespace ChromeDinoGame.Services
{
    class GameManager
    {
        private DispatcherTimer _gameTimer;
        private Action _endGameCallback;
        private EntityHandler _entityHandler;
        private ScoreManager _scoreManager;
        private UIManager _uiManager;

        private double _currentSpeedOfEntities = Characteristics.SpeedOfEntities;
        private const double SpeedInc = Characteristics.SpeedInc;

        public GameManager(Action endGameCallBack)
        {
            _endGameCallback = endGameCallBack;
            _entityHandler = new EntityHandler(EndGame);
            _scoreManager = new ScoreManager();
            _uiManager = new UIManager();
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(3);
            _gameTimer.Tick += GameLoop;
        }

        public void StartGame()
        {
            Dino.Instance.Run();
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

        public void DeclareVictory()
        {
            Dino.Instance.SetWinState();
            _uiManager.DisplayVictoryBlock();
            _gameTimer.Stop();
            _endGameCallback.Invoke();
        }

        public void RestartGame()
        {
            _currentSpeedOfEntities = Characteristics.SpeedOfEntities;
            GlobalCanvas.GameArea.Children.Clear();
            _entityHandler.SetReplayCharacteristics();
            _entityHandler.InitializeStartWindow();
            _scoreManager.SetScores();
            _uiManager.UpdateScoreBlock(_scoreManager.CurrentScore, _scoreManager.HighestScore);
            _uiManager.DisplayPauseInfBlock();
            Dino.Instance.ReviveDino();
            _gameTimer.Start();
        }

        public void TogglePause(bool isPaused)
        {
            Dino.Instance.ToggleDinoPause(isPaused);

            if (isPaused)
                _gameTimer.Stop();
            else
                _gameTimer.Start();
        }

        public void EndGame()
        {
            _gameTimer.Stop();
            Dino.Instance.SetDeadCharacteristics();
            _uiManager.UpdateReplayInfoBlock(true);
            _endGameCallback.Invoke();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            _currentSpeedOfEntities += SpeedInc;
            _entityHandler.UpdateEntities();
            _scoreManager.UpdateScores(_currentSpeedOfEntities);
            _uiManager.UpdateScoreBlock(_scoreManager.CurrentScore, _scoreManager.HighestScore);

            if(_scoreManager.CurrentScore >= Characteristics.ScoreToWin)
                DeclareVictory();
        }
    }
}
