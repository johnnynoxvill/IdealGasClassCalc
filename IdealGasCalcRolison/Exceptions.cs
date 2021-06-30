using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idealGasCalculatorClass
{
    class GasFormatException : Exception
    {
        public GasFormatException() 
        {
            Console.WriteLine("Not a valid gas input.");
        }
    }
}
