using App.Core.Parameters.ParameterVariations;
using App.Entity;
using App.Provider;

namespace App.Core.Parameters
{
    public static class StrategyParametersBuilder
    {
        public static StrategyParameters BuildRandom()
        {
            Random random = new Random();
            StrategyParameters parameters = new StrategyParameters();
            decimal basedTradeAmountPercentage = ConfigProvider.GetConfig().BasedTradeAmountPercentage;

            parameters.Add("Global", new GlobalParameterVariation()
            {
                BuyRatioToTrade = random.Next(28, 33),
                SellRatioToTrade = random.Next(28, 33),
                TradeAmountPercentage = random.Next(0, 2) + basedTradeAmountPercentage,
                PayOffRatio = (decimal)random.Next(15, 25) / 10
            })
            .Add("RSI", new RSIParameterVariation()
            {
                Period = random.Next(13, 16),
                SellThreshold = random.Next(65, 75),
                BuyThreshold = random.Next(25, 35)
            })
            .Add("SMA", new SMAParameterVariation()
            {
                MiddlePeriod = random.Next(10, 20),
                GapBetweenMiddles = random.Next(1, 10)
            })
            .Add("EMA", new EMAParameterVariation()
            {
                MiddlePeriod = random.Next(10, 20),
                GapBetweenMiddles = random.Next(1, 10)
            })
            .Add("ATR",new ATRParameterVariation()
            {
                Period = random.Next(10, 20),
                StrongThreshold = (decimal)random.Next(10, 50) / 100,
                WeakThreshold = (decimal)random.Next(10, 50) / 100
            })
            .Add("MartinGale", new MartinGaleParameterVariation()
            {
                MaxLossesPercentage = random.Next(5, 20),
                MaxRepetitions = random.Next(1, 10),
                Multiplier = random.Next(1, 5)
            })
            ;

            return parameters;
        }
    }
}
