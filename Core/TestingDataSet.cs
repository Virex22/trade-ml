using App.Interface;
using App.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core
{
    /**
     * The DataSet class represents a set of data that is used to make a decision. this class is used by the DecisionMaker class.
     * This class can by interpreted like an advanced iterator
     */
    public class TestingDataSet : IObservable<IDataSet>, IDataSet
    {
        private List<IObserver<IDataSet>> observers = new List<IObserver<IDataSet>>();

        public IDisposable Subscribe(IObserver<IDataSet> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return EmptyDisposable.Instance;
        }
    }
}
