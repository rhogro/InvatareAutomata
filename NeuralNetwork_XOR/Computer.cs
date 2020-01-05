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
        private const int OutputLayerNumber = 1;
        private List<Entry<int>> outsideEntries;
        private readonly Random _random;
        private readonly double _target = Math.Pow(10, -6); //-24
        private double Error = 1;
        private Entry<Neuron>[] HiddenLayerNodes;
        private Neuron[] OutputNodes;
        private double LearningRate = 0.7;
        private double AgeError = 1;
        private int Age = 1;

        public Computer()
        {
            XORs = new List<XOR>
            {
                new XOR(0, 0, 0.1),
                new XOR(0, 1, 0.9),
                new XOR(1, 0, 0.9),
                new XOR(1, 1, 0.1)
            };
            outsideEntries = new List<Entry<int>>();
            _random = new Random();

            List<double> weights = new List<double>();
            for (int j = 0; j < OutputLayerNumber; j++)
            {
                weights.Add(0);
            }

            HiddenLayerNodes = new Entry<Neuron>[HiddenLayerNumber];
            for(int i = 0; i < HiddenLayerNumber; i++)
            {
                HiddenLayerNodes[i] = new Entry<Neuron>(new Neuron(), weights);
            }

            OutputNodes = new Neuron[OutputLayerNumber];

            for(int i = 0; i < OutputLayerNumber; i++)
            {
                OutputNodes[i] = new Neuron();
            }
        }

        public void Compute()
        {
            while (AgeError > _target)
            {
                AgeError = 0;
                foreach (var xor in XORs)
                {
                    Console.WriteLine("Input [{0}, {1}]", xor.X1, xor.X2);
                    List<double> entryWeights = new List<double>();
                    for(int i = 0; i < HiddenLayerNumber; i++)
                    {
                        entryWeights.Add(_random.NextDouble());  
                    }
                     // 1. Feed Forward
                    outsideEntries.Add(new Entry<int>(xor.X1, entryWeights)); //they're random, it doesn't matter if they are the same
                    outsideEntries.Add(new Entry<int>(xor.X2, entryWeights));

                    CalculateHiddenNodesValues();

                    CalculateOutputNodesValues();

                    CalculateTotalError(xor.Result);

                    AgeError += Error;

                    // 2. Backpropagation

                    RecomputeOutputLayerBiases(xor.Result);
                    RecomputeHiddenLayerWeights(xor.Result);
                    RecomputeHiddenLayerBiases(xor.Result);
                    RecomputeInputWeights(xor.Result);

                    outsideEntries.Clear();
                }

                Console.WriteLine("Age {0}, error {1}", Age++, AgeError);

                //end of era
            }

        }

        private void RecomputeInputWeights(double result)
        {
            foreach(Entry<int> input in outsideEntries)
            {
                int i = 0;
                foreach (Entry<Neuron> neuron in HiddenLayerNodes)
                {
                    double sum = 0;
                    for (int j = 0; j < OutputLayerNumber; j++)
                    {
                        sum += (OutputNodes[j].Value - result) * FDerived(OutputNodes[j].Value) * neuron.Weights[j];
                    }
                    double derivateVal = 2 * sum * FDerived(neuron.Value.Value) * input.Value;
                    input.Weights[i] = input.Weights[i] - LearningRate * derivateVal;
                    i++;
                }
            }
        }

        private void RecomputeHiddenLayerBiases(double result)
        {
            foreach(Entry<Neuron> neuron in HiddenLayerNodes)
            {
                double sum = 0;
                for(int i = 0; i < OutputLayerNumber; i++)
                {
                    sum += (OutputNodes[i].Value - result) * FDerived(OutputNodes[i].Value) * neuron.Weights[i];
                }
                double derivateVal = 2 * sum * FDerived(neuron.Value.Value);
                neuron.Value.Bias = neuron.Value.Bias - LearningRate * derivateVal;
            }
        }

        private void RecomputeHiddenLayerWeights(double result)
        {
            foreach(Entry<Neuron> neuron in HiddenLayerNodes)
            {
               
                for(int i = 0; i < OutputLayerNumber; i++)
                {
                    double derivateVal = 2 * (OutputNodes[i].Value - result) * FDerived(OutputNodes[0].Value) * neuron.Value.Value;
                    neuron.Weights[i] = neuron.Weights[i] - LearningRate * derivateVal;
                }
            }
        }

        private void RecomputeOutputLayerBiases(double result)
        {
            foreach(Neuron neuron in OutputNodes)
            {
                double derivateVal = 2 * (neuron.Value - result) * FDerived(neuron.Value);
                neuron.Bias = neuron.Bias - LearningRate * derivateVal;
            }
        }

        private double FDerived(double value)
        {
            return (1 / (1 + Math.Pow(Math.E, -value))) * (1 - 1 / (1 + Math.Pow(Math.E, -value)));
        }

        private void CalculateTotalError(double realOutput)
        {
            Error = 0;
            for(int i = 0; i < OutputLayerNumber; i++)
            {
                Error += Math.Pow(OutputNodes[i].Value - realOutput, 2);
            }
        }

        private void CalculateOutputNodesValues()
        {
            for(int i=0;i< OutputLayerNumber; i++)
            {
                double sum = 0;
                for (int j = 0; j < HiddenLayerNumber; j++)
                {
                    sum += HiddenLayerNodes[j].Value.Value * HiddenLayerNodes[j].Weights[i];
                }
                OutputNodes[i].Value = sum + OutputNodes[i].Bias;
                Console.WriteLine("Output: {0}", OutputNodes[i].Value);
            }
        }

        private void CalculateHiddenNodesValues()
        {
            for (int i = 0; i < HiddenLayerNumber; i++)
            {
                int j = 0;
                double sum = 0;
                foreach (var entry in outsideEntries)
                {
                    sum = entry.Value * entry.Weights[j++];
                }
                HiddenLayerNodes[i].Value.Value = sum + HiddenLayerNodes[i].Value.Bias;
                HiddenLayerNodes[i].Value.Value = 1 / (1 + Math.Pow(Math.E, -HiddenLayerNodes[i].Value.Value));
                for(int k = 0; k < OutputLayerNumber; k++)
                {
                    HiddenLayerNodes[i].Weights[k] = _random.NextDouble();
                }
            }
        }
    }
}
