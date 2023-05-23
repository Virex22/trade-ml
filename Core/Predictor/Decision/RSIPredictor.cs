using App.Core.DataSet;
using App.Core.Indicator;
using App.Core.Parameters;
using App.Entity;
using App.Enumerator;
using System.Data;

namespace App.Core.Predictor.Decision
{
    public class RSIPredictor : AbstractDecisionPredictor
    {
        private readonly RSIParameterVariation parameters;

        public RSIPredictor(DecisionMaker decisionMaker, RSIIndicator indicator, RSIParameterVariation parameters) : base(indicator, decisionMaker)
        {
            this.parameters = parameters;
        }

        public override EDecision GetDecision()
        {
            RSIIndicator rsiIndicator = (RSIIndicator)indicator;
            decimal RSIValue = rsiIndicator.Calculate(GetLastCandles(parameters.Period));

            if (parameters.SellThreshold < RSIValue)
                return EDecision.SELL;
            else if (parameters.BuyThreshold > RSIValue)
                return EDecision.BUY;
            else
                return EDecision.HOLD;
        }
    }
}
