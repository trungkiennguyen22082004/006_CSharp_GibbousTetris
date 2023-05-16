namespace GibbousTetris
{
    public abstract class Subject
    {
        // The list of observers which are waiting for sth to happen
        List<IObserver> observers = new List<IObserver>();

        public void Notify()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                // Notify all observers even though some may not be interested in what has happened
                // Each observer should check if it is interested in this event
                observers[i].OnNotify();
            }
        }

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }
    }
}
