
namespace ChromeDinoGame.Services
{
    public class ScoreManager
    {
        private List<double> _scoresHistory;
        private double _speedInc;

        public double HighestScore { get; private set; }
        public double CurrentScore { get; private set; }

        public ScoreManager(double speedInc)
        {
            _speedInc = speedInc;
            _scoresHistory = new List<double>();
        }

        public void SetScores()
        {
            CurrentScore = 0;

            if (_scoresHistory.Count == 0)
                HighestScore = CurrentScore;
            else
                HighestScore = _scoresHistory.Max();
        }

        public void UpdateScores()
        {
            CurrentScore += _speedInc;

             if (HighestScore <= CurrentScore)
                HighestScore = CurrentScore;
        }

        public void SaveHighestScore() => _scoresHistory.Add(HighestScore);
    }
}
