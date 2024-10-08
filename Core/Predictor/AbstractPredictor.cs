﻿using App.Core.DataSet;
using App.Core.Parameters;
using App.Entity;
using App.Interface;

namespace App.Core.Predictor
{
    public abstract class AbstractPredictor
    {
        protected readonly IIndicator indicator;
        protected readonly DecisionMaker decisionMaker;
        protected readonly AbstractParameterVariation parameters;

        public AbstractPredictor(IIndicator indicator, DecisionMaker decisionMaker, AbstractParameterVariation parameters)
        {
            this.indicator = indicator;
            this.decisionMaker = decisionMaker;
            this.parameters = parameters;
        }

        protected List<Candle> GetLastCandles(int period)
        {
            AbstractDataSet? dataSet = decisionMaker.SubscribedDataSet;
            if (dataSet == null)
                throw new InvalidOperationException("DataSet is not subscribed");

            int startIndex = dataSet.CurrentIndex - period;
            if (startIndex < 0 || startIndex + period > dataSet.Data.Count)
                throw new ArgumentException($"Cannot extract {period} candles from the current index of {dataSet.CurrentIndex} with the available data size of {dataSet.Data.Count} candles.");

            return dataSet.Data.GetRange(startIndex, period);
        }
    }
}
