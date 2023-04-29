﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exception
{
    public class ApiException : System.Exception
    {
        public ApiException(string message, string result) : base(GenerateMessage(message,result))
        {
            // call parent constructor

        }

        private static string GenerateMessage(string message, string result)
        {
            return "message : " + message + "\r\n" + "result : " + result;
        }
    }
}
