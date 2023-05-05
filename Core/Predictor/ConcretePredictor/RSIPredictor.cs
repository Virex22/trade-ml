using App.Core.DataSet;
using App.Core.Indicator;
using App.Core.Parameters;
using App.Entity;
using App.Enumerator;
using System.Data;

namespace App.Core.Predictor.ConcretePredictor
{
    public class RSIPredictor : AbstractPredictor
    {
        RSIParameterVariation parameters;
        DecisionMaker decisionMaker;

        public RSIPredictor(DecisionMaker decisionMaker ,RSIIndicator indicator, RSIParameterVariation parameters) : base(indicator)
        {
            this.parameters = parameters;
            this.decisionMaker = decisionMaker;
        }

        public override EDecision MakeDecision()
        {
            RSIIndicator rsiIndicator = (RSIIndicator)indicator;

            

            decimal RSIValue = rsiIndicator.Calculate(GetCandles());

            if (parameters.SellThreshold < RSIValue)
                return EDecision.SELL;
            else if (parameters.BuyThreshold > RSIValue)
                return EDecision.BUY;
            else
                return EDecision.HOLD;
        }

        private List<Candle> GetCandles()
        {
            AbstractDataSet? dataSet = this.decisionMaker.subscribedDataSet;
            if (dataSet == null)
                throw new InvalidOperationException("DataSet is not subscribed");

            int startIndex = dataSet.CurrentIndex - parameters.Period;
            if (startIndex < 0 || startIndex + parameters.Period > dataSet.Data.Count)
                throw new ArgumentException($"Cannot extract {parameters.Period} candles from the current index of {dataSet.CurrentIndex} with the available data size of {dataSet.Data.Count} candles.");

            return dataSet.Data.GetRange(startIndex, parameters.Period); 
        }
    }
}
