﻿using App.Core;
using App.Core.DataSet;
using App.Core.Parameters;
using App.Core.Predictor;
using App.Core.Reporting;
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
            StrategyParameters strategy = StrategyParametersBuilder.BuildRandom();
            DecisionMaker trader = new DecisionMaker(strategy);

            TestingDataSet market = new TestingDataSet();
            trader.SetSubscribedDataSet(market);

            market.Load();
            market.Start();

            ReportGenerator.GenerateSingleTestReport(trader,strategy);

            trader.GetResults().Debug();
        }
    }
}
