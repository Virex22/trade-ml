using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Parameters
{
    public class RSIParameterVariation : AbstractParameterVariation
    {
        public RSIParameterVariation()
        {
        }

        public decimal RsiBuyThreshold
        {
            get { return (decimal)this.parameters["RsiBuyThreshold"]; }
            set { this.parameters["RsiBuyThreshold"] = value; }
        }

        public decimal RsiSellThreshold
        {
            get { return (decimal)this.parameters["RsiSellThreshold"]; }
            set { this.parameters["RsiSellThreshold"] = value; }
        }

        public int RSIPeriod
        {
            get { return (int)this.parameters["RSIPeriod"]; }
            set { this.parameters["RSIPeriod"] = value; }
        }

        public override AbstractParameterVariation Derive()
        {
            Dictionary<string, object> newParameters = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> parameter in this.parameters)
            {
                if (parameter.Key == "RSIPeriod")
                {
                    newParameters[parameter.Key] = this.DeriveRSIPeriod((int)parameter.Value);
                }
                else
                {
                    newParameters[parameter.Key] = this.DeriveThreshold((decimal)parameter.Value);
                }
            }

            return new RSIParameterVariation()
            {
                parameters = newParameters
            };
        }

        private object DeriveThreshold(decimal value)
        {
            return value + (new Random().Next(-2, 3)) * 0.5m;
        }

        private object DeriveRSIPeriod(int value)
        {
            return value + (new Random().Next(-1,2));
        }
    }
}
