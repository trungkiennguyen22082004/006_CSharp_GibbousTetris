namespace GibbousTetris
{
    public class ScoreSubject : Subject
    {
        private int _score;

        public int Score
        { 
            get 
            {
                return _score;
            } 
            set 
            { 
                _score = value;
                this.Notify();
            } 
        }
    }
}
