namespace GibbousTetris
{
    public class ScoreDisplay : IObserver
    {
        private ScoreSubject _scoreSubject;

        public ScoreDisplay(ScoreSubject subject) 
        {
            _scoreSubject = subject;
            _scoreSubject.AddObserver(this);
        }

        public void OnNotify()
        {
            int score = _scoreSubject.Score;
        }
    }
}
