using App.Enumerator;
using App.Interface;

namespace App.Core.Predictor.Strength
{
    abstract public class StrengthPredictor : AbstractPredictor
    {
        public StrengthPredictor(IIndicator indicator, DecisionMaker decisionMaker) : base(indicator, decisionMaker)
        {
        }

        abstract public EMarketForce GetStrength();
    }
}
