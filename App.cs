using App.Core;
using App.Entity;
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
            DecisionMaker decisionMaker = new DecisionMaker();
            TestingDataSet dataSet = new TestingDataSet();  
        }
    }
}
