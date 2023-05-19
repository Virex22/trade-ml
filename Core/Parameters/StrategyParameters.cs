using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Interface;

namespace App.Core.Parameters
{
    public class StrategyParameters : IDerivable<StrategyParameters>
    {
        [Newtonsoft.Json.JsonProperty("ParameterVariations")]
        private Dictionary<string, AbstractParameterVariation> parameterVariations = new Dictionary<string, AbstractParameterVariation>();

        public StrategyParameters AddParameterVariation(string predictorName, AbstractParameterVariation variation)
        {
            parameterVariations[predictorName] = variation;
            return this;
        }

        public StrategyParameters Derive()
        {
            StrategyParameters strategyParameters = new StrategyParameters();
            foreach (var parameterVariation in parameterVariations)
                strategyParameters.AddParameterVariation(parameterVariation.Key, parameterVariation.Value.Derive());
            return strategyParameters;
        }

        public AbstractParameterVariation GetParameterVariation(string predictorName)
        {
            if (parameterVariations.ContainsKey(predictorName))
                return parameterVariations[predictorName];
            else
                throw new ArgumentException($"No parameter variation found for predictor '{predictorName}'.");
        }

        public void RemoveParameterVariation(string predictorName)
        {
            parameterVariations.Remove(predictorName);
        }
    }
}
