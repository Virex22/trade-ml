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
            decimal basedTradeAmountPercentage = Config.GetInstance().Get<decimal>("basedTradeAmountPercentage");

            parameters.AddParameterVariation("Global", new GlobalParameterVariation()
            {
                BuyRatioToTrade = random.Next(28, 33),
                SellRatioToTrade = random.Next(28, 33),
                TradeAmountPercentage = random.Next(-1, 2) + basedTradeAmountPercentage,
                PayOffRatio = (decimal)random.Next(15, 25) / 10
            })
            .AddParameterVariation("RSI", new RSIParameterVariation()
            {
                Period = random.Next(13, 16),
                SellThreshold = random.Next(65, 75),
                BuyThreshold = random.Next(25, 35)
            })
            .AddParameterVariation("SMA", new SMAParameterVariation()
            {
                MiddlePeriod = random.Next(10, 20),
                GapBetweenMiddles = random.Next(1, 10)
            })
            .AddParameterVariation("EMA", new EMAParameterVariation()
            {
                MiddlePeriod = random.Next(10, 20),
                GapBetweenMiddles = random.Next(1, 10)
            })
            .AddParameterVariation("ATR",new ATRParameterVariation()
            {
                Period = random.Next(10, 20),
                StrongThreshold = (decimal)random.Next(10, 50) / 100,
                WeakThreshold = (decimal)random.Next(10, 50) / 100
            })
            ;

            return parameters;
        }
    }
}
