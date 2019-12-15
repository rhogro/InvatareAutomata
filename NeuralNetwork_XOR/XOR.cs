using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_XOR
{
    public class XOR
    {
        public int X1 { get; set; }
        public int X2 { get; set; }
        public int Result { get; set; }

        public XOR(int x1, int x2, int result)
        {
            X1 = x1;
            X2 = x2;
            Result = result;
        }
    }
}
