using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Parameters.ParameterVariations
{
    public class RSIParameterVariation : AbstractParameterVariation
    {
        public decimal BuyThreshold { get; set; }

        public decimal SellThreshold { get; set; }

        public int Period { get; set; }

        public override AbstractParameterVariation Derive()
        {
            return new RSIParameterVariation()
            {
                BuyThreshold = DeriveDecimal(BuyThreshold, 0.5m, 2),
                SellThreshold = DeriveDecimal(SellThreshold, 0.5m, 2),
                Period = (int)DeriveDecimal(Period, 1, 1)
            };
        }
    }
}
