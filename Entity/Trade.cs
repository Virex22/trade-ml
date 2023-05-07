﻿using System;
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
        public DateTime EntryTime { get; private set; }
        public decimal StopLossPrice { get; private set; }
        public decimal TakeProfitPrice { get; private set; }
        public DateTime CloseTime { get; private set; }
        public decimal? ClosePrice { get; private set; }
        public decimal Amount { get; private set; }
        public decimal? ProfitLoss { get; private set; }

        public Trade(TradeType type, decimal entryPrice, decimal stopLossPrice, decimal takeProfitPrice , decimal Amount)
        {
            Type = type;
            EntryPrice = entryPrice;
            StopLossPrice = stopLossPrice;
            TakeProfitPrice = takeProfitPrice;
            EntryTime = DateTime.Now;
            this.Amount = Amount;
        }

        // return end trade amount
        public decimal Close(decimal closePrice)
        {
            CloseTime = DateTime.Now;
            ClosePrice = closePrice;

            decimal profitLoss = 0;

            if (Type == TradeType.Buy)
                profitLoss = (closePrice - EntryPrice) * (Amount / EntryPrice);
            else
                profitLoss = (EntryPrice - closePrice) * (Amount / EntryPrice);

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
