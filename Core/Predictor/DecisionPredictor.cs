using App.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Predictor
{
    public class DecisionPredictor
    {
        private List<AbstractPredictor> predictors;

        public DecisionPredictor(List<AbstractPredictor> predictors)
        {
            this.predictors = predictors;
        }

        public List<EDecision> MakeDecision()
        {
            List<EDecision> decisions = new List<EDecision>();

            foreach (AbstractPredictor predictor in predictors)
                decisions.Add(predictor.MakeDecision());

            return decisions;
        }
    }
}
