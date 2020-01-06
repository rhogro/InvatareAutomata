using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_XOR
{
    public class Neuron
    {
        public double Value { get; set; }
        public double Bias { get; set; }

        public Neuron()
        {
            Random _random = new Random();
            Bias = _random.NextDouble();
        }
    }
}
