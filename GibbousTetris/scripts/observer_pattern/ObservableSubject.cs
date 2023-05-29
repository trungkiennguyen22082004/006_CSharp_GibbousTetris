namespace GibbousTetris
{
    public abstract class ObservableSubject
    {
        // The list of observers which are waiting for sth to happen
        List<IObserver> _observers = new List<IObserver>();

        public void Notify()
        {
            for (int i = 0; i < _observers.Count; i++)
            {
                // Notify all observers even though some may not be interested in what has happened
                // Each observer should check if it is interested in this event
                _observers[i].OnNotify();
            }
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}