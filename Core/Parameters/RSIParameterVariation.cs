using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Parameters
{
    public class RSIParameterVariation : AbstractParameterVariation
    {
        private readonly Random random = new Random();

        public decimal BuyThreshold
        {
            get { return (decimal)this.parameters["BuyThreshold"]; }
            set { this.parameters["BuyThreshold"] = value; }
        }

        public decimal SellThreshold
        {
            get { return (decimal)this.parameters["SellThreshold"]; }
            set { this.parameters["SellThreshold"] = value; }
        }

        public int Period
        {
            get { return (int)this.parameters["Period"]; }
            set { this.parameters["Period"] = value; }
        }

        public override AbstractParameterVariation Derive()
        {
            Dictionary<string, object> newParameters = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> parameter in this.parameters)
            {
                newParameters[parameter.Key] = parameter.Key == "Period"
                    ? this.DeriveRSIPeriod((int)parameter.Value)
                    : this.DeriveThreshold((decimal)parameter.Value);
            }

            return new RSIParameterVariation()
            {
                parameters = newParameters
            };
        }

        private object DeriveThreshold(decimal value)
        {
            return value + (random.Next(-2, 3)) * 0.5m;
        }

        private object DeriveRSIPeriod(int value)
        {
            return value + (random.Next(-1,2));
        }
    }
}
