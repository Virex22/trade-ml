using App.Core.Parameters;
using App.Enumerator;
using App.Interface;

namespace App.Core.Predictor.Strength
{
    abstract public class AbstractStrengthPredictor : AbstractPredictor
    {
        public AbstractStrengthPredictor(IIndicator indicator, DecisionMaker decisionMaker, AbstractParameterVariation parameters) : base(indicator, decisionMaker, parameters)
        {
        }

        abstract public EMarketForce GetStrength();
    }
}
