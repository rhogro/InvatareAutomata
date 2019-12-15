using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_XOR
{
    public class Entry<T>
    {
        public Entry(T value, double weight)
        {
            Value = value;
            Weight = weight;
        }
        public T Value { get; set; }
        public double Weight { get; set; }
    }
}
