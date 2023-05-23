using App.Interface;
using App.Enumerator;

namespace App.Core.Predictor.Trend
{
    abstract public class AbstractTrendPredictor : AbstractPredictor
    {
        public AbstractTrendPredictor(IIndicator indicator, DecisionMaker decisionMaker) : base(indicator, decisionMaker)
        {
        }

        abstract public ETrend GetTrend();
    }
}
