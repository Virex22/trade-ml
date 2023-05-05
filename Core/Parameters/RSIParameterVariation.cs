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
                BuyThreshold = (decimal)this.DeriveThreshold(this.BuyThreshold),
                SellThreshold = (decimal)this.DeriveThreshold(this.SellThreshold),
                Period = (int)this.DeriveRSIPeriod(this.Period)
            };
        }

        private decimal DeriveThreshold(decimal value)
        {
            return value + (this.random.Next(-2, 3)) * 0.5m;
        }

        private int DeriveRSIPeriod(int value)
        {
            return value + (this.random.Next(-1,2));
        }
    }
}
