using App.Core.Indicator;
using App.Core.Parameters;
using App.Core.Parameters.ParameterVariations;
using App.Core.Predictor.Decision;

namespace App.Core.Predictor
{
    public static class PredictorBuilder
    {
        public static PredictorCollection Build(StrategyParameters strategyParameters, DecisionMaker decisionMaker)
        {
            List<AbstractPredictor> predictors = new()
            {
                new RSIPredictor(new RSIIndicator(), decisionMaker, (RSIParameterVariation)strategyParameters.GetParameterVariation("RSI"))
            };

            return new PredictorCollection(predictors);
        }
    }
}
