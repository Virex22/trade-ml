using App.Entity;
using App.Provider;
using App.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DataSet
{
    /**
     * The DataSet class represents a set of data that is used to make a decision. this class is used by the DecisionMaker class.
     * This class can by interpreted like an advanced iterator
     */
    public class TestingDataSet : AbstractDataSet
    {
        public override void Load()
        {
            TrainingDataProvider trainingDataProvider = new TrainingDataProvider();
            this.Data = trainingDataProvider.getRandomDayCandleValue(150,10);
        }
    }
}
