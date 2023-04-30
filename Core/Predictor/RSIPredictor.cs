using App.Core.Indicator;
using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Predictor
{
    public class RSIPredictor : AbstractPredictor<RSIIndicator>
    {
        public RSIPredictor(RSIIndicator indicator) : base(indicator)
        {
        }

        public override Decision MakeDecision()
        {
            throw new NotImplementedException();
        }
    }
}
