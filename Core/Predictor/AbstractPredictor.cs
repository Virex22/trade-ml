using App.Entity;
using App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Predictor
{
    public abstract class AbstractPredictor<T> 
        where T : IIndicator
    {
        protected readonly T indicator;

        public AbstractPredictor(T indicator)
        {
            this.indicator = indicator;
        }

        public abstract Decision MakeDecision();
    }
}
