using App.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DataSet
{
    public class DataSet : AbstractDataSet
    {
        public override DateTimeOffset GetCurrentTime()
        {
            return DateTimeOffset.Now;
        }

        public override void Load()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }
    }
}
