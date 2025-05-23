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
        private EntityHandler _entityHandler;
        private ScoreManager _scoreManager;
        private UIManager _uiManager;

        private double _currentSeedOfEntities = 10;
        private const double INITIAL_SPEED_OF_ENTITIES = 10;
        private const double SPEED_INC = 0.00001;
        private const double LINE_OF_GROUND = 16;

        public GameManager(Canvas canvas, Action endGameCallBack)
        {
            _canvas = canvas;
            Dino = new Dino(_canvas, LINE_OF_GROUND, _currentSeedOfEntities);
            _random = new Random();
            _endGameCallback = endGameCallBack;
            _entityHandler = new EntityHandler(_canvas, Dino, _random, EndGame, _currentSeedOfEntities, LINE_OF_GROUND, SPEED_INC);
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

        public void DeclareVictory()
        {
            Dino.SetWinState();
            _uiManager.DisplayVictoryBlock();
            _gameTimer.Stop();
            _endGameCallback.Invoke();
        }

        public void RestartGame()
        {
            _currentSeedOfEntities = INITIAL_SPEED_OF_ENTITIES;
            _canvas.Children.Clear();
            _entityHandler.SetReplayCharacteristics();
            _entityHandler.InitializeStartWindow();
            _scoreManager.SetScores();
            _uiManager.UpdateScoreBlock(_scoreManager.CurrentScore, _scoreManager.HighestScore);
            _uiManager.DisplayPauseInfBlock();
            Dino.ReviveDino();
            _gameTimer.Start();
        }

        public void TogglePause(bool isPaused)
        {
            Dino.ToggleDinoPause(isPaused);

            if (isPaused)
                _gameTimer.Stop();
            else
                _gameTimer.Start();
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
            _currentSeedOfEntities += SPEED_INC;
            _entityHandler.UpdateEntities();
            _scoreManager.UpdateScores(_currentSeedOfEntities);
            _uiManager.UpdateScoreBlock(_scoreManager.CurrentScore, _scoreManager.HighestScore);

            if(_scoreManager.CurrentScore >= 1000000)
                DeclareVictory();
        }
    }
}
