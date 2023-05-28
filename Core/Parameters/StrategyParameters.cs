using App.Interface;

namespace App.Core.Parameters
{
    public class StrategyParameters : IDerivable<StrategyParameters>
    {
        [Newtonsoft.Json.JsonProperty("ParameterVariations")]
        private Dictionary<string, AbstractParameterVariation> parameterVariations = new Dictionary<string, AbstractParameterVariation>();

        public StrategyParameters Add(string predictorName, AbstractParameterVariation variation)
        {
            parameterVariations[predictorName] = variation;
            return this;
        }

        public StrategyParameters Derive()
        {
            StrategyParameters strategyParameters = new StrategyParameters();
            foreach (var parameterVariation in parameterVariations)
                strategyParameters.Add(parameterVariation.Key, parameterVariation.Value.Derive());
            return strategyParameters;
        }

        public AbstractParameterVariation Get(string predictorName)
        {
            if (parameterVariations.ContainsKey(predictorName))
                return parameterVariations[predictorName];
            else
                throw new ArgumentException($"No parameter variation found for predictor '{predictorName}'.");
        }

        public void Remove(string predictorName)
        {
            parameterVariations.Remove(predictorName);
        }
    }
}
