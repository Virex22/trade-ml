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
            StrategyParameters parameters = new StrategyParameters();

            parameters.AddParameterVariation("RSI", new RSIParameterVariation() {Period = 14, SellThreshold = 70, BuyThreshold = 30 });

            return parameters;
        }
    }
}
