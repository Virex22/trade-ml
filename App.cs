using App.Entity;
using App.Provider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade_ml.Utils;

namespace App
{
    public class App
    {
       public void Run()
        {
            Configuration config = Configuration.GetInstance();
            config.Get("pair");

        }
    }
}
