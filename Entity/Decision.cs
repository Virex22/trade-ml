using App.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entity
{
    public class Decision
    {
        private EDecisionType DecisionType;
        private int DecisionScore;

        public Decision(int decisionScore)
        {
            this.SetDecisionScore(decisionScore);
        }

        public void SetDecisionScore(int score)
        {
            DecisionScore = score;

            if (score > 75)
                DecisionType = EDecisionType.Buy;
            else if (score < 25)
                DecisionType = EDecisionType.Sell;
            else
                DecisionType = EDecisionType.None;
        }

        public int GetDecisionScore()
        {
            return DecisionScore;
        }

        public EDecisionType GetDecisionType()
        {
            return DecisionType;
        }
    }
}
