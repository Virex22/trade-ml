using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Interface
{
    public interface IIndicator {}

    public interface IIndicator<T> : IIndicator
    {
        T Calculate();
    }
}
