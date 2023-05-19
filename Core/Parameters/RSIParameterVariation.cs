using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Parameters
{
    public class RSIParameterVariation : AbstractParameterVariation
    {
        public decimal BuyThreshold { get; set;}

        public decimal SellThreshold { get; set; }

        public int Period { get; set; }

        public override AbstractParameterVariation Derive()
        {
            return new RSIParameterVariation()
            {
                BuyThreshold = this.DeriveDecimal(this.BuyThreshold, 0.5m, 2),
                SellThreshold = this.DeriveDecimal(this.SellThreshold, 0.5m, 2),
                Period = (int) this.DeriveDecimal(this.Period, 1, 1)
            };
        }
    }
}
