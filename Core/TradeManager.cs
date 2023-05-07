using App.Core.DataSet;
using App.Core.Parameters;
using App.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core
{
    public class TradeManager : IObserver<AbstractDataSet>
    {
        private List<Trade> activeTrades = new List<Trade>();
        private List<Trade> closedTrades = new List<Trade>();
        private Wallet wallet;
        GlobalParameterVariation globalParameter;

        public IReadOnlyList<Trade> ActiveTrades => activeTrades.AsReadOnly();
        public IReadOnlyList<Trade> ClosedTrades => closedTrades.AsReadOnly();

        public TradeManager(Wallet wallet, GlobalParameterVariation globalParameter)
        {
            this.wallet = wallet;
            this.globalParameter = globalParameter;
        }

        public void OpenTrade(Trade.TradeType type, decimal entryPrice, decimal stopLossPrice, decimal takeProfitPrice)
        {
            decimal AmountToTrade = globalParameter.TradeAmountPercentage * wallet.Balance / 100;
            
            Trade trade = new Trade(type, entryPrice, stopLossPrice, takeProfitPrice, AmountToTrade);

            activeTrades.Add(trade);
        }


        public void OnNext(AbstractDataSet marketData)
        {
            foreach (Trade trade in activeTrades.ToList())
            {
                if (trade.HasReachedStopLoss(marketData.CurrentPrice))
                {
                    trade.Close(trade.StopLossPrice);
                    activeTrades.Remove(trade);
                    closedTrades.Add(trade);
                }
                else if (trade.HasReachedTakeProfit(marketData.CurrentPrice))
                {
                    trade.Close(trade.TakeProfitPrice);
                    activeTrades.Remove(trade);
                    closedTrades.Add(trade);
                }
            }
        }

        public void OnCompleted()
        {
            // Ignored
        }

        public void OnError(System.Exception error)
        {
            // Ignored
        }
    }
}
