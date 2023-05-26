using App.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Parameters.ParameterVariations
{
    public class ATRParameterVariation : AbstractParameterVariation
    {
        public int Period { get; set; }

        /**
         * example : 0.5 means 50% of the ATR average value (100 in average for period, 150 for strong)
         */
        public decimal StrongThreshold { get; set; }
        /**
         * example : 0.5 means 50% of the ATR average value (100 in average for period, 50 for weak)
         */
        public decimal WeakThreshold { get; set; }

        public override AbstractParameterVariation Derive()
        {
            return new ATRParameterVariation()
            {
                Period = (int)DeriveDecimal(Period, 1, 1, 10, 50),
                StrongThreshold = DeriveDecimal(StrongThreshold, 0.1m, 0.1m, 0.1m, 0.9m),
                WeakThreshold = DeriveDecimal(WeakThreshold, 0.1m, 0.1m, 0.1m, 0.9m)
            };
        }
    }
}
