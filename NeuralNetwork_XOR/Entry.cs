using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_XOR
{
    public class Entry<T>
    {
        public T Value { get; set; }
        public List<double> Weights { get; set; }

        public Entry(T value, List<double> weights)
        {
            Value = value;
            Weights = weights.ConvertAll(w => new double());
        }

    }
}
