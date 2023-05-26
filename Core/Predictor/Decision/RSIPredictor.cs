using App.Core.Indicator;
using App.Core.Parameters.ParameterVariations;
using App.Enumerator;

namespace App.Core.Predictor.Decision
{
    public class RSIPredictor : AbstractDecisionPredictor
    {
        public RSIPredictor(RSIIndicator indicator, DecisionMaker decisionMaker, RSIParameterVariation parameters) : base(indicator, decisionMaker, parameters)
        {
        }

        public override EDecision GetDecision()
        {
            RSIIndicator rsiIndicator = (RSIIndicator)indicator;
            RSIParameterVariation parameters = (RSIParameterVariation)this.parameters;

            decimal RSIValue = rsiIndicator.Calculate(GetLastCandles(parameters.Period));

            if (parameters.SellThreshold < RSIValue)
                return EDecision.Sell;
            else if (parameters.BuyThreshold > RSIValue)
                return EDecision.Buy;
            else
                return EDecision.Hold;
        }
    }
}
