using App.Core.Indicator;
using App.Core.Parameters.ParameterVariations;
using App.Enumerator;

namespace App.Core.Predictor.Trend
{
    public class SMAPredictor : AbstractTrendPredictor
    {
        public SMAPredictor(SMAIndicator indicator,DecisionMaker decisionMaker, SMAParameterVariation parameters) : base(indicator, decisionMaker, parameters)
        {
        }

        public override ETrend GetTrend()
        {
            SMAIndicator smaIndicator = (SMAIndicator)indicator;
            SMAParameterVariation parameters = (SMAParameterVariation)this.parameters;
            decimal ShortSMA = smaIndicator.Calculate(GetLastCandles(parameters.MiddlePeriod - parameters.GapBetweenMiddles));
            decimal MiddleSMA = smaIndicator.Calculate(GetLastCandles(parameters.MiddlePeriod));
            decimal LongSMA = smaIndicator.Calculate(GetLastCandles(parameters.MiddlePeriod + parameters.GapBetweenMiddles));

            if (ShortSMA > MiddleSMA && MiddleSMA > LongSMA)
                return ETrend.Bullish;
            else if (ShortSMA < MiddleSMA && MiddleSMA < LongSMA)
                return ETrend.Bearish;
            else
                return ETrend.Range;

        }
    }
}
