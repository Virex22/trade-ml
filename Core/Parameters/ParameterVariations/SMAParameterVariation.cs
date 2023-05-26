using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Parameters.ParameterVariations
{
    public class SMAParameterVariation : AbstractParameterVariation
    {
        public int MiddlePeriod { get; set; }
        public int GapBetweenMiddles { get; set; }

        public override AbstractParameterVariation Derive()
        {
            int newMiddlePeriod = (int)DeriveDecimal(MiddlePeriod, 1, 1, 5, 40);
            int newGapBetweenMiddles = (int)DeriveDecimal(GapBetweenMiddles, 1, 1, 1, 30);

            if (newMiddlePeriod - newGapBetweenMiddles < 2)
                newGapBetweenMiddles = newMiddlePeriod - 2;

            return new SMAParameterVariation()
            {
                MiddlePeriod = newMiddlePeriod,
                GapBetweenMiddles = newGapBetweenMiddles
            };
        }
    }
}
