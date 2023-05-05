using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Parameters
{
    public class GlobalParameterVariation : AbstractParameterVariation
    {
        public decimal BuyRatioToTrade;
        public decimal SellRatioToTrade;

        public override AbstractParameterVariation Derive()
        {
            return new GlobalParameterVariation()
            {
                BuyRatioToTrade = this.DeriveRatio(this.BuyRatioToTrade),
                SellRatioToTrade = this.DeriveRatio(this.SellRatioToTrade)
            };
        }

        private decimal DeriveRatio(decimal ratio)
        {
            decimal variation = this.random.Next(-5, 6) / 10;

            decimal derivedRatio = ratio + variation;

            derivedRatio = Math.Max(Math.Min(derivedRatio, 100), 0);

            return derivedRatio;
        }
    }
}
