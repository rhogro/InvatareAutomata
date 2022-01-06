using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_Run
{
    class Program
    {
        static void Main(string[] args)
        {
            //NeuralNetwork_XOR.Computer neuralXOR = new NeuralNetwork_XOR.Computer();
            //var watch = Stopwatch.StartNew();
            //neuralXOR.Compute();
            //watch.Stop();
            //Console.WriteLine("Working time: {0} milliseconds", watch.ElapsedMilliseconds);
            //Console.ReadKey();

            NeuralNetwork_Points.Computer neuralPoints = new NeuralNetwork_Points.Computer(3, 2000);
            neuralPoints.Train();
            Console.WriteLine("The network has a accuracy of {0:00.##}% on validation data", neuralPoints.CheckAccuracy("Validation"));
            Console.WriteLine("The network has a accuracy of {0:00.##}% on training data", neuralPoints.CheckAccuracy("Training"));
            Console.ReadKey();
        }
    }
}
