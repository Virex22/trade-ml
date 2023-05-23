using App.Core.Indicator;
using App.Core.Parameters;
using App.Core.Predictor.Decision;
using App.Interface;

namespace App.Core.Predictor
{
    public static class PredictorBuilder
    {
        public static PredictorCollection Build(StrategyParameters strategyParameters, DecisionMaker decisionMaker)
        {
            List<AbstractPredictor> predictors = new()
            {
                new RSIPredictor(decisionMaker, new RSIIndicator(), (RSIParameterVariation)strategyParameters.GetParameterVariation("RSI"))
            };

            return new PredictorCollection(predictors);
        }
    }
}
