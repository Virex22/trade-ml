using App.Enumerator;
using App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Predictor.Decision
{
    abstract public class AbstractDecisionPredictor : AbstractPredictor
    {
        public AbstractDecisionPredictor(IIndicator indicator, DecisionMaker decisionMaker) : base(indicator, decisionMaker)
        {
        }

        abstract public EDecision GetDecision();
    }
}
