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

        private readonly Random _random = new Random();

        public Neuron()
        {
            Bias = _random.NextDouble();
        }
    }
}
