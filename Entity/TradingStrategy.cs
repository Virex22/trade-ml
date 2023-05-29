using App.Core.Predictor.Decision;
using App.Core.Predictor.Strength;
using App.Core.Predictor.Trend;
using App.Enumerator;
using App.Interface;

namespace App.Entity
{
    public class TradingStrategy
    {
        private List<IEffect> effects;
        private List<AbstractTrendPredictor> trends;
        private List<AbstractDecisionPredictor> decisions;
        private List<AbstractStrengthPredictor> strengths;

        public TradingStrategy()
        {
            effects = new List<IEffect>();
            trends = new List<AbstractTrendPredictor>();
            decisions = new List<AbstractDecisionPredictor>();
            strengths = new List<AbstractStrengthPredictor>();
        }

        public TradingStrategy AddEffect(IEffect effect)
        {
            effects.Add(effect);
            return this;
        }

        public TradingStrategy AddTrend(AbstractTrendPredictor trend)
        {
            trends.Add(trend);
            return this;
        }

        public TradingStrategy AddDecision(AbstractDecisionPredictor decision)
        {
            decisions.Add(decision);
            return this;
        }

        public TradingStrategy AddStrength(AbstractStrengthPredictor strength)
        {
            strengths.Add(strength);
            return this;
        }

        public List<EDecision> GetDecisions()
        {
            List<EDecision> decisions = new List<EDecision>();

            foreach (AbstractDecisionPredictor decision in this.decisions)
                decisions.Add(decision.GetDecision());

            return decisions;
        }
    }
}
