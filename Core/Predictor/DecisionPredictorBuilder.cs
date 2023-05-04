using App.Core.Indicator;
using App.Core.Parameters;
using App.Core.Predictor.ConcretePredictor;
using App.Interface;

namespace App.Core.Predictor
{
    public static class DecisionPredictorBuilder
    {
        public static DecisionPredictor Build(StrategyParameters strategyParameters)
        {
            List<AbstractPredictor> predictors = new List<AbstractPredictor>
            {
                new RSIPredictor(new RSIIndicator(new List<decimal>()), (RSIParameterVariation)strategyParameters.GetParameterVariation("RSI"))
            };

            return new DecisionPredictor(predictors);
        }
    }
}
