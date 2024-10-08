﻿namespace App.Core.Parameters
{
    public class GlobalParameterVariation : AbstractParameterVariation
    {
        public decimal BuyRatioToTrade;
        
        public decimal SellRatioToTrade;

        public decimal TradeAmountPercentage;

        public decimal PayOffRatio;

        public override AbstractParameterVariation Derive()
        {
            return new GlobalParameterVariation()
            {
                BuyRatioToTrade = this.DeriveDecimal(this.BuyRatioToTrade, 0.5m, 2),
                SellRatioToTrade = this.DeriveDecimal(this.SellRatioToTrade, 0.5m, 2),
                TradeAmountPercentage = this.DeriveDecimal(this.TradeAmountPercentage, 0.05m, 10, 0.1m, 5.0m),
                PayOffRatio = this.DeriveDecimal(this.PayOffRatio, 0.05m, 3, 0.5m, 5.0m)
            };
        }
    }
}
