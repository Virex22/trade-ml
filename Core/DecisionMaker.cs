using App.Core.DataSet;
using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core
{
    public class DecisionMaker : IObserver<AbstractDataSet>
    {
        public DecisionMaker()
        {

        }

        public void OnCompleted()
        {
            // No more data to process
        }

        public void OnError(System.Exception error)
        {
            // Error is not handled
        }

        public void OnNext(AbstractDataSet value)
        {
            
        }

        public Decision MakeDecision()
        {
            int weightsSum = 0;
            int indicatorsCount = 0;

            foreach (Indicator indicator in indicators)
            {
                weightsSum += indicator.Weight;
                indicatorsCount++;
            }

            int averageWeight = 0;
            if (indicatorsCount > 0)
            {
                averageWeight = weightsSum / indicatorsCount;
            }

            return new Decision(averageWeight);
        }
    }
}
