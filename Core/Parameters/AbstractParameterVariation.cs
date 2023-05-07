using App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Parameters
{
    public abstract class AbstractParameterVariation : IDerivable<AbstractParameterVariation>
    {
        protected readonly Random random = new Random();
        public abstract AbstractParameterVariation Derive();

        public decimal DeriveDecimal(decimal number, decimal step, decimal amplitude, decimal min = 0, decimal max = 100)
        {
            decimal variation = ((decimal)new Random().NextDouble() * 2 * amplitude) - amplitude; // Génère une variation aléatoire dans l'amplitude spécifiée
            decimal derivedValue = number + step * variation;

            derivedValue = Math.Max(Math.Min(derivedValue, max), min);
            derivedValue = Math.Round(derivedValue / step) * step;

            return derivedValue;
        }
    }
}
