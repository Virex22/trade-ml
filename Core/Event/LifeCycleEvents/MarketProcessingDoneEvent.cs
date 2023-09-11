using App.Core.DataSet;
using App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Event.LifeCycleEvents
{
    public class MarketProcessingDoneEvent : IEvent
    {
        public AbstractDataSet Market { get; set; }

        public MarketProcessingDoneEvent(AbstractDataSet market)
        {
            Market = market;
        }
    }
}
