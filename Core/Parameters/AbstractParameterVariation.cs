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
        protected Dictionary<string, object> parameters;

        public AbstractParameterVariation()
        {
            this.parameters = parameters = new Dictionary<string, object>();
        }

        public abstract AbstractParameterVariation Derive();
    }
}
