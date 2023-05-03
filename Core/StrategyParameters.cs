using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Parameters;

namespace App.Core
{
    public class StrategyParameters
    {
        private Dictionary<string, AbstractParameterVariation> parameterVariations = new Dictionary<string, AbstractParameterVariation>();

        public void AddParameterVariation(string predictorName, AbstractParameterVariation variation)
        {
            parameterVariations[predictorName] = variation;
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
            if (parameterVariations.ContainsKey(predictorName))
                parameterVariations.Remove(predictorName);
        }
    }
}
