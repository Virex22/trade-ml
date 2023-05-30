using App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Event
{
    public class TradeEvent : IEvent
    {
        public bool IsWinTrade { get; set; }
    }
}
