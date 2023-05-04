using App.Core.Indicator;
using App.Core.Parameters;
using App.Enumerator;

namespace App.Core.Predictor.ConcretePredictor
{
    public class RSIPredictor : AbstractPredictor
    {
        RSIParameterVariation parameters;

        public RSIPredictor(RSIIndicator indicator, RSIParameterVariation parameters) : base(indicator)
        {
            this.parameters = parameters;
        }

        public override EDecision MakeDecision()
        {
            RSIIndicator rsiIndicator = (RSIIndicator)indicator;
            decimal RSIValue = rsiIndicator.Calculate();

            if (parameters.SellThreshold < RSIValue)
                return EDecision.SELL;
            else if (parameters.BuyThreshold > RSIValue)
                return EDecision.BUY;
            else
                return EDecision.HOLD;
        }
    }
}
