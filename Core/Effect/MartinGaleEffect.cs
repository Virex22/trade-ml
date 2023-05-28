using App.Core.Parameters;
using App.Core.Parameters.ParameterVariations;
using App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Effect
{
    public class MartinGaleEffect : IEffect<decimal>
    {
        private StrategyParameters strategyParameters;
        private int lossesCount = 0;

        public MartinGaleEffect(StrategyParameters strategy) {
            this.strategyParameters = strategy;
        }
        public decimal UseEffect(decimal initialValue, DecisionMaker decisionMaker) {
            MartinGaleParameterVariation parameter = (MartinGaleParameterVariation)strategyParameters.Get("MartinGale");

            if (lossesCount >= parameter.MaxRepetitions)
                return initialValue;
            if (lossesCount == 0)
                return initialValue;

            decimal amount = initialValue * parameter.Multiplier;
            if (amount > decisionMaker.Balance * parameter.MaxLossesPercentage / 100)
               return initialValue;
               
            return amount;
        }

        public void triggerFinishedTrade(bool isWinTrade)
        {
            if (!isWinTrade)
                lossesCount++;
            else
                lossesCount = 0;
        }
    }
}
