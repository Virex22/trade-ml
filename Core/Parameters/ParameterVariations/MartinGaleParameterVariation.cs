using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Parameters.ParameterVariations
{
    public class MartinGaleParameterVariation : AbstractParameterVariation
    {
        public int Multiplier { get; set; }
        public int MaxRepetitions { get; set; }
        public int MaxLossesPercentage { get; set; }

        public override AbstractParameterVariation Derive()
        {
            return new MartinGaleParameterVariation()
            {
                Multiplier = (int)DeriveDecimal(Multiplier, 1, 1, 1, 5),
                MaxRepetitions = (int)DeriveDecimal(MaxRepetitions, 1, 1, 1, 10),
                MaxLossesPercentage = (int)DeriveDecimal(MaxLossesPercentage, 1, 1, 5, 20)
            };
        }
    }
}
