using App.Core.Parameters;
using App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Event.LifeCycleEvents
{
    public class StrategyReadyEvent : IEvent
    {
        public StrategyParameters StrategyParameters { get; set; }

        public StrategyReadyEvent(StrategyParameters strategy)
        {
            this.StrategyParameters = strategy;
        }
    }
}
