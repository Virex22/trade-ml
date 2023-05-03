using App.Core.Indicator;
using App.Core.Parameters;
using App.Core.Predictor;
using App.Interface;

namespace App.Core
{
    public static class DecisionPredictorBuilder
    {
        public static List<AbstractPredictor> Build(StrategyParameters strategyParameters)
        {
            List<AbstractPredictor> predictors = new List<AbstractPredictor>
            {
                new RSIPredictor(new RSIIndicator(new List<decimal>()), (RSIParameterVariation)strategyParameters.GetParameterVariation("RSI"))
            };

            return predictors;
        }
    }
}
