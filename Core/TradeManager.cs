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
        private AbstractDataSet? marketData;
        GlobalParameterVariation globalParameter;

        public IReadOnlyList<Trade> ActiveTrades => activeTrades.AsReadOnly();
        public IReadOnlyList<Trade> ClosedTrades => closedTrades.AsReadOnly();

        public TradeManager(Wallet wallet, GlobalParameterVariation globalParameter)
        {
            this.wallet = wallet;
            this.globalParameter = globalParameter;
        }

        public void OpenTrade(Trade.TradeType type, Candle candle, decimal stopLossPrice, decimal takeProfitPrice)
        {
            if (!this.IsEligible(type))
                return;

            decimal AmountToTrade = globalParameter.TradeAmountPercentage * wallet.Balance / 100;
            
            Trade trade = new Trade(type, candle, stopLossPrice, takeProfitPrice, AmountToTrade);

            activeTrades.Add(trade);

            wallet.Withdraw(AmountToTrade);
        }

        // new trade is eligible if there is no active trade of the same type
        private bool IsEligible(Trade.TradeType type)
        {
            if (type == Trade.TradeType.Buy)
                return !activeTrades.Any(t => t.Type == Trade.TradeType.Buy);
            else
                return !activeTrades.Any(t => t.Type == Trade.TradeType.Sell);
        }


        public void OnNext(AbstractDataSet marketData)
        {
            if (this.marketData == null)
                this.marketData = marketData;
            DateTimeOffset time = marketData.GetCurrentTime();
            foreach (Trade trade in activeTrades.ToList())
            {
                if (trade.HasReachedStopLoss(marketData.CurrentPrice))
                {
                    decimal tradeAmount = trade.Close(trade.StopLossPrice, time);
                    wallet.Deposit(tradeAmount);
                    activeTrades.Remove(trade);
                    closedTrades.Add(trade);
                }
                else if (trade.HasReachedTakeProfit(marketData.CurrentPrice))
                {
                    decimal tradeAmount = trade.Close(trade.TakeProfitPrice, time);
                    wallet.Deposit(tradeAmount);
                    activeTrades.Remove(trade);
                    closedTrades.Add(trade);
                }
            }
        }

        public void OnCompleted()
        {
            DateTimeOffset time = marketData?.GetCurrentTime() ?? DateTimeOffset.Now;
            
            // close all active trades but let in active list for reporting
            foreach (Trade trade in activeTrades.ToList())
            {
                decimal tradeAmount = trade.Close(trade.EntryPrice, time);
                wallet.Deposit(tradeAmount);
            }
        }

        public void OnError(System.Exception error)
        {
            Debug.WriteLine(error.Message);
        }

        public int GetTotalTrades(bool onlyClosedTrade = true)
        {
            return onlyClosedTrade ? closedTrades.Count : activeTrades.Count + closedTrades.Count;
        }
    }
}
