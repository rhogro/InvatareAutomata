using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Linq;

namespace NeuralNetwork_Points
{
    public class Computer
    {
        private List<Point> Points;
        private List<Point> TrainingPoints;
        private List<Point> ValidationPoints;
        private int HiddenLayerNumber = 3;
        private const int OutputLayerNumber = 4; // 4 zone
        private const int InputsNumber = 2;
        private Entry<double>[] Inputs;
        private readonly Random _random;
        private readonly double _target = Math.Pow(10, -4); //-24
        private int _maxAge = 2000;
        private Entry<Neuron>[] HiddenLayerNodes;
        private Neuron[] OutputNodes;
        private double LearningRate = 1;
        private double AgeError = 1;
        private int Age = 1;

        public Computer(int hiddenLayerNumber, int ages)
        {
            HiddenLayerNumber = hiddenLayerNumber;
            _maxAge = ages;

            _random = new Random();

            Points = ReadTrainingPointsFromFile();
            TrainingPoints = GetTrainingPoints();
            ValidationPoints = GetValidationPoints();
            Console.WriteLine("Training points count:" + TrainingPoints.Count);
            Console.WriteLine("Validation points count:" + ValidationPoints.Count);
            Inputs = new Entry<double>[InputsNumber]; //x1 si x2

            //ponderile intrarilor
            double[] entryWeights = new double[HiddenLayerNumber];
            for (int i = 0; i < HiddenLayerNumber; i++)
            {
                entryWeights[i] = _random.NextDouble();
            }
            //fiecare intrare are cate (nr neuroni strat ascuns) ponderi
            //nr total ponderi = nr intrari * nr neuroni din stratul ascuns
            for (int i = 0; i < InputsNumber; i++)
            {
                Inputs[i] = new Entry<double>(0, entryWeights);
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
            for (int i = 0; i < HiddenLayerNumber; i++)
            {
                HiddenLayerNodes[i] = new Entry<Neuron>(new Neuron(), hiddenLayerWeights);
            }

            //neuroni iesire
            OutputNodes = new Neuron[OutputLayerNumber];
            for (int i = 0; i < OutputLayerNumber; i++)
            {
                OutputNodes[i] = new Neuron();
            }
        }

        public double CheckAccuracy(string datasetString)
        {
            Console.WriteLine("Validating...");
            int correctAnswers = 0;
            List<Point> dataset;
            int numberOfPoints;
            if(datasetString.Equals("Validation"))
            {
                dataset = ValidationPoints;
                numberOfPoints = ValidationPoints.Count;
            }
            else
            {
                dataset = TrainingPoints;
                numberOfPoints = TrainingPoints.Count;
            }
            foreach(var point in dataset)
            {
                Inputs[0].Value = point.X;
                Inputs[1].Value = point.Y;
                //Console.WriteLine("Expected: " + point.Zone);
                CalculateHiddenNodesValues();
                CalculateOutputNodesValues();
                string output = ReadNetworkOutput();
                //Console.WriteLine("Actual: " + output);
                if(string.Equals(output, point.Zone))
                {
                    correctAnswers++;
                }
            }

            return ((double)correctAnswers / numberOfPoints) * 100;
        }

        public void ComputePointsFromFile()
        {
            File.Delete(Directory.GetCurrentDirectory() + @"\neural predicted points.txt");
            StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + @"\neural predicted points.txt", true);

            List<Point> points = ReadPointsFromFile();

            foreach (var point in points)
            {
                int x = (int)(point.X * 400);
                int y = (int)(point.Y * 400);
                string output = PredictPoint(x, y);
                WriteToFile(sw, x, y, output);
            }

            sw.Close();
        }

        public string PredictPoint(int x, int y)
        {
            double xEntry = x / 400.0;
            double yEntry = y / 400.0;

            Inputs[0].Value = x;
            Inputs[1].Value = y;
            CalculateHiddenNodesValues();
            CalculateOutputNodesValues();
            return ReadNetworkOutput();
        }

        private string ReadNetworkOutput()
        {
            string output = "";
            double max = 0;
            for(int o = 0; o < OutputLayerNumber; o++)
            {
                if (OutputNodes[o].Value > max) {
                    max = OutputNodes[o].Value;
                    output = "Zone"+ (o+1);
                }
            }
            return output;
        }

        public void Train()
        {
            while (AgeError > _target && Age < _maxAge)
            {
                AgeError = 0;
                foreach (var point in Points)
                {
                    //Console.WriteLine("Input [{0}, {1}]", xor.X1, xor.X2);

                    //atribuim intrararile
                    Inputs[0].Value = point.X;
                    Inputs[1].Value = point.Y;
                    //Console.Write("Expected output: ");
                    //for(int i = 0; i < point.Result.Length;i++)
                    //{
                    //    Console.Write(point.Result[i] + " ");
                    //}
                    //Console.WriteLine("");

                    // 1. Feed Forward
                    CalculateHiddenNodesValues();

                    CalculateOutputNodesValues();

                    AgeError += CalculateTotalError(point.Result);

                    // 2. Backpropagation
                    RecomputeOutputLayerBiases(point.Result); // relatia (7)
                    RecomputeHiddenLayerBiases(point.Result); // relatia (9)
                    RecomputeInputWeights(point.Result); // relatia (10)
                    RecomputeHiddenLayerWeights(point.Result); // relatia (8)

                }
                Age++;
                if(Age%100 == 0)
                    Console.WriteLine("Age {0}, error {1}", Age, AgeError);

                //end of era
            }
            Console.WriteLine("Age {0}, error {1}", Age, AgeError);

        }

        private void RecomputeInputWeights(int[] result)
        {       // relatia (10)
            for (int i = 0; i < InputsNumber; i++)
            {
                for (int h = 0; h < HiddenLayerNumber; h++)
                {
                    double sum = 0;
                    for (int o = 0; o < OutputLayerNumber; o++)
                    {
                        sum += (OutputNodes[o].Value - result[o]) * FDerived(OutputNodes[o].Value) * HiddenLayerNodes[h].Weights[o];
                    }
                    double derivateVal = 2 * sum * FDerived(HiddenLayerNodes[h].Value.Value) * Inputs[i].Value;
                    //ponderea dintre intrarea i catre neuronul ascuns j
                    Inputs[i].Weights[h] = Inputs[i].Weights[h] - LearningRate * derivateVal;
                }
            }
        }

        private void RecomputeHiddenLayerBiases(int[] result)
        {       // relatia (9)
            for (int h = 0; h < HiddenLayerNumber; h++)
            {
                double sum = 0;
                for (int o = 0; o < OutputLayerNumber; o++)
                {
                    sum += (OutputNodes[o].Value - result[o]) * FDerived(OutputNodes[o].Value) * HiddenLayerNodes[h].Weights[o];
                }
                double derivateVal = 2 * sum * FDerived(HiddenLayerNodes[h].Value.Value);
                HiddenLayerNodes[h].Value.Bias = HiddenLayerNodes[h].Value.Bias - LearningRate * derivateVal;
            }
        }

        private void RecomputeHiddenLayerWeights(int[] result)
        {       // relatia (8)
            foreach (Entry<Neuron> neuron in HiddenLayerNodes)
            {
                for (int o = 0; o < OutputLayerNumber; o++)
                {
                    double derivateVal = 2 * (OutputNodes[o].Value - result[o]) * FDerived(OutputNodes[o].Value) * neuron.Value.Value;
                    neuron.Weights[o] = neuron.Weights[o] - LearningRate * derivateVal;
                }
            }
        }

        private void RecomputeOutputLayerBiases(int[] result)
        {   // relatia  (7)
            for (int o = 0; o < OutputLayerNumber; o++)
            {
                double derivateVal = 2 * (OutputNodes[o].Value - result[o]) * FDerived(OutputNodes[o].Value);
                OutputNodes[o].Bias = OutputNodes[o].Bias - LearningRate * derivateVal;
            }
        }

        private double FDerived(double value)
        {
            double ret;
            ret = value * (1 - value);
            return ret;
        }

        private double CalculateTotalError(int[] realOutput)
        {
            double error = 0;
            for (int i = 0; i < OutputLayerNumber; i++)
            {
                error += Math.Pow(OutputNodes[i].Value - realOutput[i], 2);
            }
            return error;
        }

        private void CalculateOutputNodesValues()
        {
            //Console.Write("Output: ");
            for (int i = 0; i < OutputLayerNumber; i++)
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
                //Console.Write(OutputNodes[i].Value + " ");
            }
            //Console.WriteLine("");
        }

        private void CalculateHiddenNodesValues()
        {
            for (int h = 0; h < HiddenLayerNumber; h++)
            {
                double sum = 0;
                for (int i = 0; i < InputsNumber; i++)
                {
                    //pentru fiecare neuron din stratul ascuns
                    //insumam produsul valorilor tuturor intrarilor
                    //cu ponderile intrarilor referitoare la nodul ascuns curent
                    sum += Inputs[i].Value * Inputs[i].Weights[h];
                }
                //valoarea neuronului ascuns e suma dintre suma si prag
                HiddenLayerNodes[h].Value.Value = sum + HiddenLayerNodes[h].Value.Bias;
                //valoarea e rezultatul functiei de activare in functie de valoarea obtinuta anterior
                HiddenLayerNodes[h].Value.Value = 1 / (1 + Math.Exp(-HiddenLayerNodes[h].Value.Value));
            }
        }

        private List<Point> ReadTrainingPointsFromFile()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("X_Value", typeof(int));
            dt.Columns.Add("Y_Value", typeof(int));
            dt.Columns.Add("Zone", typeof(string));

            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"..\..\..\..\Chart\bin\Debug\points.txt");

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] strarr = line.Split(',');
                dt.Rows.Add(strarr[0], strarr[1], strarr[2]);
            }

            sr.Close();

            List<Point> points = new List<Point>();

            foreach (DataRow row in dt.Rows)
            {
                points.Add(new Point((int)row["X_Value"], (int)row["Y_Value"], (string)row["Zone"]));
            }

            return points;
        }

        private List<Point> ReadPointsFromFile()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("X_Value", typeof(int));
            dt.Columns.Add("Y_Value", typeof(int));

            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"..\..\..\..\Chart\bin\Debug\points.txt");

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] strarr = line.Split(',');
                dt.Rows.Add(strarr[0], strarr[1]);
            }

            sr.Close();

            List<Point> points = new List<Point>();

            foreach (DataRow row in dt.Rows)
            {
                points.Add(new Point((int)row["X_Value"], (int)row["Y_Value"], ""));
            }

            return points;
        }

        private List<Point> GetTrainingPoints()
        {
            int totalPoints = Points.Count;
            return Points.GetRange(0, (int)(0.7 * totalPoints));
        }

        private List<Point> GetValidationPoints()
        {
            int totalPoints = Points.Count;
            return Points.GetRange((int)(0.7 * totalPoints), (int)(0.3 * totalPoints));
        }

        private void WriteToFile(StreamWriter sw, int coordX, int coordY, string currentZoneName)
        {
            sw.WriteLine(coordX.ToString() + ',' + coordY + ',' + currentZoneName);
        }
    }
}
