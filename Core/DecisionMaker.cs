using App.Core.DataSet;
using App.Core.Parameters;
using App.Core.Predictor;
using App.Entity;
using App.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core
{
    public class DecisionMaker : IObserver<AbstractDataSet>
    {
        private DecisionPredictor predictor;

        public DecisionMaker(StrategyParameters strategy)
        {
            this.predictor = DecisionPredictorBuilder.Build(strategy);
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

        public EDecision MakeDecision()
        {
            return EDecision.HOLD;
        }
    }
}
