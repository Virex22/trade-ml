using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entity
{
    public class Candle
    {
        public DateTimeOffset open_time;
        public double open;
        public double high;
        public double low;
        public double close;
        public DateTimeOffset close_time;

        public Candle()
        {
        }

    }
}
