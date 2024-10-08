﻿using App.Core.Indicator;
using App.Core.Parameters.ParameterVariations;
using App.Enumerator;

namespace App.Core.Predictor.Strength
{
    public class ATRStrengthPredictor : AbstractStrengthPredictor
    {

        public ATRStrengthPredictor(ATRIndicator indicator, DecisionMaker decisionMaker, ATRParameterVariation parameters)
            : base(indicator, decisionMaker, parameters)
        {
        }

        public override EMarketForce GetStrength()
        {
            ATRIndicator atrIndicator = (ATRIndicator)indicator;
            ATRParameterVariation atrParameters = (ATRParameterVariation)parameters;

            decimal atrValue = atrIndicator.Calculate(GetLastCandles(atrParameters.Period));
            decimal atrAverage = atrIndicator.ATRAverage;

            decimal strongThreshold = atrAverage * atrParameters.StrongThreshold;
            decimal weakThreshold = atrAverage * atrParameters.WeakThreshold;

            if (atrValue >= strongThreshold)
                return EMarketForce.Strong;
            else if (atrValue <= weakThreshold)
                return EMarketForce.Weak;
            else
                return EMarketForce.Neutral;
        }
    }
}
