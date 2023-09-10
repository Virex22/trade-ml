using App.Entity;
using App.Interface;
using App.Provider;

namespace App.Core.Parameters
{
    public abstract class AbstractParameterVariation : IDerivable<AbstractParameterVariation>
    {
        protected readonly Random random = new Random();
        public abstract AbstractParameterVariation Derive();

        public decimal DeriveDecimal(decimal number, decimal step, decimal amplitude, decimal min = 0, decimal max = 100)
        {
            amplitude = amplitude * ConfigProvider.GetConfig().VariationAmplitudeCoef;

            decimal variation = CalculateRandomVariation(amplitude);
            decimal derivedValue = number + step * variation;

            derivedValue = ClampValue(derivedValue, min, max);
            derivedValue = RoundValue(derivedValue, step);

            return derivedValue;
        }

        private decimal CalculateRandomVariation(decimal amplitude)
        {
            return ((decimal)random.NextDouble() * 2 * amplitude) - amplitude;
        }

        private decimal ClampValue(decimal value, decimal min, decimal max)
        {
            return Math.Max(Math.Min(value, max), min);
        }

        private decimal RoundValue(decimal value, decimal step)
        {
            return Math.Round(value / step) * step;
        }
    }
}
