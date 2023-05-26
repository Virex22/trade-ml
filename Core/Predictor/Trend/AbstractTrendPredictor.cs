using App.Interface;
using App.Enumerator;
using App.Core.Parameters;

namespace App.Core.Predictor.Trend
{
    abstract public class AbstractTrendPredictor : AbstractPredictor
    {
        public AbstractTrendPredictor(IIndicator indicator, DecisionMaker decisionMaker, AbstractParameterVariation parameters) : base(indicator, decisionMaker, parameters)
        {
        }

        abstract public ETrend GetTrend();
    }
}
