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
    }
}
