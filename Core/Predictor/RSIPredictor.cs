using App.Core.Indicator;
using App.Core.Parameters;
using App.Entity;
using App.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Predictor
{
    public class RSIPredictor : AbstractPredictor<RSIIndicator>
    {
        RSIParameterVariation parameters;

        public RSIPredictor(RSIIndicator indicator, RSIParameterVariation parameters) : base(indicator)
        {
            this.parameters = parameters;
        }

        public override EDecision MakeDecision()
        {
            decimal RSIValue = this.indicator.Calculate();

            if (this.parameters.RsiSellThreshold < RSIValue)
                return EDecision.SELL;
            else if (this.parameters.RsiBuyThreshold > RSIValue)
                return EDecision.BUY;
            else
                return EDecision.HOLD;
        }
    }
}
