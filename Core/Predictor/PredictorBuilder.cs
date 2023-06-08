using App.Core.Effect;
using App.Core.Indicator;
using App.Core.Parameters;
using App.Core.Parameters.ParameterVariations;
using App.Core.Predictor.Decision;
using App.Entity;

namespace App.Core.Predictor
{
    public static class PredictorBuilder
    {
        public static TradingStrategy Build(StrategyParameters strategyParameters, DecisionMaker decisionMaker)
        {
            TradingStrategy tradingStrategy = new TradingStrategy();

            tradingStrategy.AddDecision(
                new RSIPredictor(new RSIIndicator(), decisionMaker, (RSIParameterVariation)strategyParameters.Get("RSI"))
            )
            ;

            return tradingStrategy;
        }
    }
}
