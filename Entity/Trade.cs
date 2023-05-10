using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entity
{
    public class Trade
    {
        public enum TradeType
        {
            Buy,
            Sell
        }

        public TradeType Type { get; private set; }
        public decimal EntryPrice { get; private set; }
        public DateTimeOffset EntryTime { get; private set; }
        public decimal StopLossPrice { get; private set; }
        public decimal TakeProfitPrice { get; private set; }
        public DateTimeOffset CloseTime { get; private set; }
        public decimal? ClosePrice { get; private set; }
        public decimal Amount { get; private set; }
        public decimal? ProfitLoss { get; private set; }

        public Trade(TradeType type, Candle candle, decimal stopLossPrice, decimal takeProfitPrice , decimal Amount)
        {
            Type = type;
            EntryPrice = candle.Close;
            StopLossPrice = stopLossPrice;
            TakeProfitPrice = takeProfitPrice;
            EntryTime = candle.CloseTime;
            this.Amount = Amount;
        }

        // return end trade amount  
        public decimal Close(decimal closePrice, DateTimeOffset time)
        {
            CloseTime = time;
            ClosePrice = closePrice;

            decimal profitLoss = 0;

            if (Type == TradeType.Buy)
                profitLoss = (closePrice - EntryPrice) * (Amount / EntryPrice);
            else
                profitLoss = (EntryPrice - closePrice) * (Amount / EntryPrice);

            ProfitLoss = profitLoss;

            return Amount + profitLoss;
        }
        public decimal Close(Candle candle)
        {
            CloseTime = candle.CloseTime;
            ClosePrice = candle.Close;

            decimal profitLoss = 0;

            if (Type == TradeType.Buy)
                profitLoss = (candle.Close - EntryPrice) * (Amount / EntryPrice);
            else
                profitLoss = (EntryPrice - candle.Close) * (Amount / EntryPrice);

            ProfitLoss = profitLoss;

            return Amount + profitLoss;
        }

        public bool HasReachedStopLossOrTakeProfit(decimal currentPrice)
        {
            return HasReachedStopLoss(currentPrice) || HasReachedTakeProfit(currentPrice);
        }

        public bool HasReachedStopLoss(decimal currentPrice)
        {
            if (Type == TradeType.Buy)
                return currentPrice <= StopLossPrice;
            else
                return currentPrice >= StopLossPrice;
        }

        public bool HasReachedTakeProfit(decimal currentPrice)
        {
            if (Type == TradeType.Buy)
                return currentPrice >= TakeProfitPrice;
            else
                return currentPrice <= TakeProfitPrice;
        }
    }
}
