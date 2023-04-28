using App.Interface;
using App.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core
{
    public class DataSet : IDataSet
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
