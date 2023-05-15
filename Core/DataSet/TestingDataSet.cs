﻿using App.Entity;
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
        public override DateTimeOffset GetCurrentTime()
        {
            return this.Data[this.CurrentIndex].CloseTime;
        }

        public override void Load()
        {
            TrainingDataProvider trainingDataProvider = new TrainingDataProvider();
            this.Data = trainingDataProvider.getRandomDayCandleValue(150,30); 
        }

        public override void Start()
        {
            int initialIndex = Config.GetInstance().GetConfig("histMinDataBufferLen");

            if (this.Data.Count < initialIndex)
                throw new InvalidOperationException("Not enough data to start the simulation. Please diminuate the histMinDataBufferLen parameter in the config file.");

            for (this.CurrentIndex = initialIndex; this.CurrentIndex < this.Data.Count; this.CurrentIndex++)
                Notify();
            CurrentIndex--;

            NotifyComplete();
        }
    }
}
