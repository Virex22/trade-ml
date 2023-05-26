using App.Core.DataSet;
using App.Core.Indicator;
using App.Core.Parameters;
using App.Enumerator;
using App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Predictor.Trend
{
    public class EMAPredictor : AbstractTrendPredictor
    {
        public EMAPredictor(EMAIndicator indicator,DecisionMaker decisionMaker, EMAParameterVariation parameters) : base(indicator, decisionMaker, parameters)
        {
        }        
        
        public override ETrend GetTrend()
        {
            EMAIndicator smaIndicator = (EMAIndicator)indicator;
            EMAParameterVariation parameters = (EMAParameterVariation)this.parameters;
            decimal ShortEMA = smaIndicator.Calculate(GetLastCandles(parameters.MiddlePeriod - parameters.GapBetweenMiddles));
            decimal MiddleEMA = smaIndicator.Calculate(GetLastCandles(parameters.MiddlePeriod));
            decimal LongEMA = smaIndicator.Calculate(GetLastCandles(parameters.MiddlePeriod + parameters.GapBetweenMiddles));

            if (ShortEMA > MiddleEMA && MiddleEMA > LongEMA)
                return ETrend.Bullish;
            else if (ShortEMA < MiddleEMA && MiddleEMA < LongEMA)
                return ETrend.Bearish;
            else
                return ETrend.Range;

        }
    }
}
