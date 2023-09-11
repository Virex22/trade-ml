using App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Event.LifeCycleEvents
{
    public class DecisionMakerReadyEvent : IEvent
    {
        public DecisionMaker DecisionMaker { get; set; }

        public DecisionMakerReadyEvent(DecisionMaker decisionMaker)
        {
            DecisionMaker = decisionMaker;
        }
    }
}
