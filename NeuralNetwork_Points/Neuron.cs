using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork_Points
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
