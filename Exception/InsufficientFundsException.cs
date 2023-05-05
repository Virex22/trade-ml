using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exception
{
    public class InsufficientFundsException : System.Exception
    {
        public InsufficientFundsException() : base() { }

        public InsufficientFundsException(string message) : base(message) { }
    }
}
