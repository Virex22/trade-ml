using App.Core.Parameters;
using App.Entity;
using App.Enumerator;
using App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Predictor
{
    public abstract class AbstractPredictor
    {
        protected readonly IIndicator indicator;

        public AbstractPredictor(IIndicator indicator)
        {
            this.indicator = indicator;
        }

        public abstract EDecision MakeDecision();
    }
}
