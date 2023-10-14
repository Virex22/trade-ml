using App.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Interface
{
    public interface IDelivrable
    {
        public List<Candle> deliver(int maxHistoriqueDay = 365, int dayCount = 10);
    }
}
