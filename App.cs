using App.Core;
using App.Core.DataSet;
using App.Entity;
using App.Enumerator;
using App.Provider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class App
    {
       public void Run()
        {
            DecisionMaker trader = new DecisionMaker();

            TestingDataSet market = new TestingDataSet();
            market.Load();
            market.Subscribe(trader);
        }
    }
}
