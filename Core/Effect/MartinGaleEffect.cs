using App.Core.Event;
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
    public class MartinGaleEffect : IEffect
    {
        private StrategyParameters strategyParameters;
        private int lossesCount = 0;

        public MartinGaleEffect(StrategyParameters strategy) {
            this.strategyParameters = strategy;
            EventBus.GetInstance().Subscribe<TradeEvent>(triggerFinishedTrade);
        }
        public decimal UseEffect(decimal initialValue, DecisionMaker decisionMaker) {
            MartinGaleParameterVariation parameter = (MartinGaleParameterVariation)strategyParameters.Get("MartinGale");

            if (lossesCount >= parameter.MaxRepetitions)
                return initialValue;
            if (lossesCount == 0)
                return initialValue;

            decimal amount = initialValue * (parameter.Multiplier * lossesCount);
            if (amount > decisionMaker.Balance * parameter.MaxLossesPercentage / 100)
               return initialValue;

               
            return amount;
        }

        public void triggerFinishedTrade(TradeEvent tradeEvent)
        {
            if (!tradeEvent.IsWinTrade)
                lossesCount++;
            else
                lossesCount = 0;
        }
    }
}
