using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_Run
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork_XOR.Computer neuralXOR = new NeuralNetwork_XOR.Computer();
            neuralXOR.Compute();
            Console.ReadKey();
        }
    }
}
