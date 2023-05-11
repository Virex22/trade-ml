using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Parameters
{
    public static class StrategyParametersBuilder
    {
        public static StrategyParameters BuildRandom()
        {
            Random random = new Random();
            StrategyParameters parameters = new StrategyParameters();
            decimal basedTradeAmountPercentage = (decimal)Config.GetInstance().GetConfig("basedTradeAmountPercentage");

            parameters.AddParameterVariation("Global", new GlobalParameterVariation()
            {
                BuyRatioToTrade = random.Next(28, 33),
                SellRatioToTrade = random.Next(28, 33),
                TradeAmountPercentage = random.Next(-1, 2) + basedTradeAmountPercentage,
                PayOffRatio = (decimal)random.Next(15, 25) / 10
            });

            parameters.AddParameterVariation("RSI", new RSIParameterVariation() {
                Period = random.Next(13,16), 
                SellThreshold = random.Next(65, 75), 
                BuyThreshold = random.Next(25, 35) });     

            return parameters;
        }
    }
}
