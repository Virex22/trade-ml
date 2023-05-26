using App.Entity;
using App.Utils;

namespace App.Core.DataSet
{
    abstract public class AbstractDataSet : IObservable<AbstractDataSet>
    {
        private readonly List<IObserver<AbstractDataSet>> observers = new List<IObserver<AbstractDataSet>>();

        public List<Candle> Data { get; set; }
        public int CurrentIndex { get; protected set; }
        public decimal CurrentPrice => Data[CurrentIndex].Close;

        public AbstractDataSet()
        {
            Data = new List<Candle>();
        }

        public IDisposable Subscribe(IObserver<AbstractDataSet> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return EmptyDisposable.Instance;
        }

        protected void Notify() => NotifyObservers(observer => observer.OnNext(this));
        protected void NotifyComplete() => NotifyObservers(observer => observer.OnCompleted());

        protected void NotifyObservers(Action<IObserver<AbstractDataSet>> action)
        {
            foreach (var observer in observers)
                action(observer);
        }

        abstract public void Load();
        abstract public void Start();
        abstract public DateTimeOffset GetCurrentTime();
    }
}
