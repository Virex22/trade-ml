using App.Core.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core
{
    public class DecisionMaker : IObserver<AbstractDataSet>
    {
        public void OnCompleted()
        {
            Console.WriteLine("fin de la periode de test");
        }

        public void OnError(System.Exception error)
        {
            // Error is not handled
        }

        public void OnNext(AbstractDataSet value)
        {
            
        }
    }
}
