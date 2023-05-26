using App.Core.Predictor.Decision;
using App.Enumerator;

namespace App.Core.Predictor
{
    /**
     * Class that contains a list of predictors. in the future, this class will be used to run the multiple predictors type.
     */
    public class PredictorCollection
    {
        private List<AbstractPredictor> predictors;

        public PredictorCollection(List<AbstractPredictor> predictors)
        {
            this.predictors = predictors;
        }

        public List<EDecision> GetDecision()
        {
            List<EDecision> decisions = new();

            foreach (AbstractPredictor predictor in predictors)
                if (predictor is AbstractDecisionPredictor decisionPredictor)
                    decisions.Add(decisionPredictor.GetDecision());

            return decisions;
        }

    }
}
