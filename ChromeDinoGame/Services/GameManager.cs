using System.Diagnostics;
using System.Windows.Media;
using ChromeDinoGame.Entities;
using ChromeDinoGame.Globals;

namespace ChromeDinoGame.Services
{
    class GameManager
    {
        private Action _endGameCallback;
        private EntityHandler _entityHandler;
        private ScoreManager _scoreManager;
        private UIManager _uiManager;
        private Stopwatch _stopwatch;
        private TimeSpan _lastFrameTime;

        private double _currentSpeedOfEntities = Characteristics.SpeedOfEntities;
        private const double SpeedInc = Characteristics.SpeedInc;

        private const double TargetFPS = 60;
        private const double FrameTimeCap = 1.0 / TargetFPS;

        public GameManager(Action endGameCallback)
        {
            _endGameCallback = endGameCallback;
            _entityHandler = new EntityHandler(EndGame);
            _scoreManager = new ScoreManager();
            _uiManager = new UIManager();
            _stopwatch = new Stopwatch();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            TimeSpan currentTime = _stopwatch.Elapsed;
            double deltaTime = (currentTime - _lastFrameTime).TotalSeconds;

            if (deltaTime < FrameTimeCap)
                return;

            _lastFrameTime = currentTime;

            _currentSpeedOfEntities += SpeedInc * deltaTime;

            _entityHandler.UpdateEntities();
            _scoreManager.UpdateScores(_currentSpeedOfEntities);
            _uiManager.UpdateScoreBlock(_scoreManager.CurrentScore, _scoreManager.HighestScore);

            if (_scoreManager.CurrentScore >= Characteristics.ScoreToWin)
                DeclareVictory();
        }

        public void StartGame()
        {
            Dino.Instance.Run();
            _uiManager.UpdateStartInfoBlock(false);
            _lastFrameTime = TimeSpan.Zero;
            CompositionTarget.Rendering += GameLoop;
            _stopwatch.Restart();
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
            CompositionTarget.Rendering -= GameLoop;
            _stopwatch.Stop();
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
            _lastFrameTime = TimeSpan.Zero;
            CompositionTarget.Rendering += GameLoop;
            _stopwatch.Restart();
        }

        public void TogglePause(bool isPaused)
        {
            Dino.Instance.ToggleDinoPause(isPaused);

            if (isPaused)
            {
                CompositionTarget.Rendering -= GameLoop;
                _stopwatch.Stop();
            }
            else
            {
                _lastFrameTime = TimeSpan.Zero;
                CompositionTarget.Rendering += GameLoop;
                _stopwatch.Start();
            }
        }

        public void EndGame()
        {
            CompositionTarget.Rendering -= GameLoop;
            _stopwatch.Stop();
            Dino.Instance.SetDeadCharacteristics();
            _uiManager.UpdateReplayInfoBlock(true);
            _endGameCallback.Invoke();
        }
    }
}
