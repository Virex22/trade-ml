using App.Core.Parameters;
using App.Enumerator;
using App.Interface;

namespace App.Core.Predictor.Decision
{
    abstract public class AbstractDecisionPredictor : AbstractPredictor
    {
        public AbstractDecisionPredictor(IIndicator indicator, DecisionMaker decisionMaker,AbstractParameterVariation parameters) : base(indicator, decisionMaker, parameters)
        {
        }

        abstract public EDecision GetDecision();
    }
}
