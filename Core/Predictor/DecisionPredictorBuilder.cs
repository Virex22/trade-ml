using App.Core.Indicator;
using App.Core.Parameters;
using App.Core.Predictor.ConcretePredictor;
using App.Interface;

namespace App.Core.Predictor
{
    public static class DecisionPredictorBuilder
    {
        public static DecisionPredictor Build(StrategyParameters strategyParameters, DecisionMaker decisionMaker)
        {
            List<AbstractPredictor> predictors = new List<AbstractPredictor>
            {
                new RSIPredictor(decisionMaker, new RSIIndicator(), (RSIParameterVariation)strategyParameters.GetParameterVariation("RSI"))
            };

            return new DecisionPredictor(predictors);
        }
    }
}
