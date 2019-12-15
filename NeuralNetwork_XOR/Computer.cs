using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_XOR
{
    public class Computer
    {
        private List<XOR> XORs;
        private const int HiddenLayerNumber = 5;
        private const int OutputLayerNumber = 2;
        private List<Entry<int>> outsideEntries;
        private readonly Random _random;
        private readonly double _target = Math.Pow(10, -24);
        private double Error = 1;
        private Entry<double>[] HiddenLayerNodes;
        private double[] OutputNodes;
        private double LearningRate = 1;

        public Computer()
        {
            XORs = new List<XOR>
            {
                new XOR(0, 0, 0),
                new XOR(0, 1, 1),
                new XOR(1, 0, 1),
                new XOR(1, 1, 0)
            };
            outsideEntries = new List<Entry<int>>();
            _random = new Random();
            HiddenLayerNodes = new Entry<double>[HiddenLayerNumber];
            OutputNodes = new double[OutputLayerNumber];
        }

        public void Compute()
        {
            while (Error > _target)
            {
                foreach (var xor in XORs)
                {
                     // 1. Feed Forward
                    outsideEntries.Add(new Entry<int>(xor.X1, _random.NextDouble()));
                    outsideEntries.Add(new Entry<int>(xor.X2, _random.NextDouble()));

                    CalculateHiddenNodesValues();

                    CalculateOutputNodesValues();

                    CalculateTotalError(xor.Result);

                    // 2. Backpropagation

                    

                }

                //end of era
                outsideEntries.Clear();
                LearningRate /= 2;
            }

        }

        private void CalculateTotalError(int realOutput)
        {
            Error = 0;
            for(int i = 0; i < OutputLayerNumber; i++)
            {
                Error += Math.Pow(OutputNodes[i] - realOutput, 2); // TODO if wrong: realoutput is a list of possible out values
            }
        }

        private void CalculateOutputNodesValues()
        {
            for(int i=0;i< OutputLayerNumber; i++)
            {
                double bias = _random.NextDouble();
                for (int j = 0; j < HiddenLayerNumber; j++)
                {
                    OutputNodes[i] = HiddenLayerNodes[j].Value * HiddenLayerNodes[j].Weight;
                }
                OutputNodes[i] += bias;
            }
        }

        private void CalculateHiddenNodesValues()
        {
            for (int i = 0; i < HiddenLayerNumber; i++)
            {
                double bias = _random.NextDouble();
                foreach (var entry in outsideEntries)
                {
                    HiddenLayerNodes[i].Value = entry.Value * entry.Weight;
                }
                HiddenLayerNodes[i].Value += bias;
                HiddenLayerNodes[i].Value = 1 / (1 + Math.Pow(Math.E, -HiddenLayerNodes[i].Value));
                HiddenLayerNodes[i].Weight = _random.NextDouble();
            }
        }
    }
}
