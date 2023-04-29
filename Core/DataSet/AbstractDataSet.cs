using App.Entity;
using App.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DataSet
{
    abstract public class AbstractDataSet : IObservable<AbstractDataSet>
    {
        private List<IObserver<AbstractDataSet>> observers = new List<IObserver<AbstractDataSet>>();
        public List<Candle> Data { get; set; }

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

        protected void Notify()
        {
            foreach (var observer in observers)
                observer.OnNext(this);
        }

        abstract public void Load();
    }
}
