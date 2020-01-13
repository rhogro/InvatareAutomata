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
        private const int HiddenLayerNumber = 2;
        private const int OutputLayerNumber = 1;
        private const int OutsideEntriesNumber = 2;
        private Entry<int>[] outsideEntries;
        private readonly Random _random;
        private readonly double _target = Math.Pow(10, -11); //-24
        private Entry<Neuron>[] HiddenLayerNodes;
        private Neuron[] OutputNodes;
        private double LearningRate = 1;
        private double AgeError = 1;
        private int Age = 1;

        public Computer()
        {
            _random = new Random();

            XORs = new List<XOR>
            {
                new XOR(0, 0, 0.1),
                new XOR(0, 1, 0.9),
                new XOR(1, 0, 0.9),
                new XOR(1, 1, 0.1)
            };

            outsideEntries = new Entry<int>[OutsideEntriesNumber]; //x1 si x2

            //ponderile intrarilor
            double[] entryWeights = new double[HiddenLayerNumber];
            for (int i = 0; i < HiddenLayerNumber; i++)
            {
                entryWeights[i] = _random.NextDouble();
            }
            //fiecare intrare are cate (nr neuroni strat ascuns) ponderi
            //nr total ponderi = nr intrari * nr neuroni din stratul ascuns
            for(int i = 0; i < OutsideEntriesNumber; i++)
            {
                outsideEntries[i] = new Entry<int>(0, entryWeights);
            }
            
            //ponderile neuronilor de pe stratul ascuns
            double[] hiddenLayerWeights = new double[OutputLayerNumber];
            for (int j = 0; j < OutputLayerNumber; j++)
            {
                hiddenLayerWeights[j] = _random.NextDouble();
            }
            //fiecare neuron are cate (nr neuroni iesire) ponderi
            //nr total ponderi = nr neuroni strat intermediar * nr neuroni iesire
            HiddenLayerNodes = new Entry<Neuron>[HiddenLayerNumber];
            for(int i = 0; i < HiddenLayerNumber; i++)
            {
                HiddenLayerNodes[i] = new Entry<Neuron>(new Neuron(), hiddenLayerWeights);
            }

            //neuroni iesire
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

                    //atribuim intrararile
                    outsideEntries[0].Value = xor.X1;
                    outsideEntries[1].Value = xor.X2;

                    // 1. Feed Forward
                    CalculateHiddenNodesValues();

                    CalculateOutputNodesValues();

                    AgeError += CalculateTotalError(xor.Result);

                    // 2. Backpropagation
                    RecomputeOutputLayerBiases(xor.Result); // relatia (7)
                    RecomputeHiddenLayerBiases(xor.Result); // relatia (9)
                    RecomputeInputWeights(xor.Result); // relatia (10)
                    RecomputeHiddenLayerWeights(xor.Result); // relatia (8)

                }

                Console.WriteLine("Age {0}, error {1}", Age++, AgeError);

                //end of era
            }

        }

        private void RecomputeInputWeights(double result)
        {       // relatia (10)
            for (int i = 0; i < OutsideEntriesNumber; i++)
            {
                for(int h = 0; h < HiddenLayerNumber; h++)
                {
                    double sum = 0;
                    for(int o = 0; o < OutputLayerNumber; o++)
                    {
                        sum += (OutputNodes[o].Value - result) * FDerived(OutputNodes[o].Value) * HiddenLayerNodes[h].Weights[o];
                    }
                    double derivateVal = 2 * sum * FDerived(HiddenLayerNodes[h].Value.Value) * outsideEntries[i].Value;
                    //ponderea dintre intrarea i catre neuronul ascuns j
                    outsideEntries[i].Weights[h] = outsideEntries[i].Weights[h] - LearningRate * derivateVal;
                }
            }
        }

        private void RecomputeHiddenLayerBiases(double result)
        {       // relatia (9)
            foreach(Entry<Neuron> neuron in HiddenLayerNodes)
            {
                double sum = 0;
                for(int o = 0; o < OutputLayerNumber; o++)
                {
                    sum += (OutputNodes[o].Value - result) * FDerived(OutputNodes[o].Value) * neuron.Weights[o];
                }
                double derivateVal = 2 * sum * FDerived(neuron.Value.Value);
                neuron.Value.Bias = neuron.Value.Bias - LearningRate * derivateVal;
            }
        }

        private void RecomputeHiddenLayerWeights(double result)
        {       // relatia (8)
            foreach(Entry<Neuron> neuron in HiddenLayerNodes)
            {
                for(int o = 0; o < OutputLayerNumber; o++)
                {
                    double derivateVal = 2 * (OutputNodes[o].Value - result) * FDerived(OutputNodes[0].Value) * neuron.Value.Value;
                    neuron.Weights[o] = neuron.Weights[o] - LearningRate * derivateVal;
                }
            }
        }

        private void RecomputeOutputLayerBiases(double result)
        {   // relatia  (7)
            foreach(Neuron neuron in OutputNodes)
            {
                double derivateVal = 2 * (neuron.Value - result) * FDerived(neuron.Value);
                neuron.Bias = neuron.Bias - LearningRate * derivateVal;
            }
        }

        private double FDerived(double value)
        {
            double ret;
            ret = value * (1 - value);
            return ret;
        }

        private double CalculateTotalError(double realOutput)
        {
            double error = 0;
            for(int i = 0; i < OutputLayerNumber; i++)
            {
                error += Math.Pow(OutputNodes[i].Value - realOutput, 2);
            }
            return error;
        }

        private void CalculateOutputNodesValues()
        {
            for(int i=0;i< OutputLayerNumber; i++)
            {
                double sum = 0;
                for (int j = 0; j < HiddenLayerNumber; j++)
                {
                    //pentru fiecare neuron de iesire
                    //insumam produsul valorilor neuronilor din stratul ascuns 
                    //cu ponderile stratului ascuns referitoare la nodul de iesire curent
                    sum += HiddenLayerNodes[j].Value.Value * HiddenLayerNodes[j].Weights[i];
                }
                //valoarea este suma dintre suma si prag
                OutputNodes[i].Value = sum + OutputNodes[i].Bias;
                //valoarea e rezultatul functiei de activare in functie de rezultatul obtinut anterior
                OutputNodes[i].Value = 1 / (1 + Math.Exp(-OutputNodes[i].Value));
                Console.WriteLine("Output: {0}", OutputNodes[i].Value);
            }
        }

        private void CalculateHiddenNodesValues()
        {
            for (int h = 0; h < HiddenLayerNumber; h++)
            {
                double sum = 0;
                for (int i=0;i<OutsideEntriesNumber;i++)
                {
                    //pentru fiecare neuron din stratul ascuns
                    //insumam produsul valorilor tuturor intrarilor
                    //cu ponderile intrarilor referitoare la nodul ascuns curent
                    sum += outsideEntries[i].Value * outsideEntries[i].Weights[h];
                }
                //valoarea neuronului ascuns e suma dintre suma si prag
                HiddenLayerNodes[h].Value.Value = sum + HiddenLayerNodes[h].Value.Bias;
                //valoarea e rezultatul functiei de activare in functie de valoarea obtinuta anterior
                HiddenLayerNodes[h].Value.Value = 1 / (1 + Math.Exp(-HiddenLayerNodes[h].Value.Value));
            }
        }
    }
}
