
namespace ChromeDinoGame.Services
{
    public class ScoreManager
    {
        private List<double> _scoresHistory = new List<double>();

        public double HighestScore { get; private set; }
        public double CurrentScore { get; private set; }

        public void SetScores()
        {
            _scoresHistory.Add(HighestScore);
            CurrentScore = 0;

            if (_scoresHistory.Count == 0)
                HighestScore = CurrentScore;
            else
                HighestScore = _scoresHistory.Max();
        }

        public void UpdateScores(double speed)
        {
            CurrentScore += speed;

             if (HighestScore <= CurrentScore)
                HighestScore = CurrentScore;
        }
    }
}
