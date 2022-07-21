using System.Linq;
using MonoGameWindowsDesktopApplication1.Utils;

namespace MonoGameWindowsDesktopApplication1.AI
{
    public class Neuron
    {
        public double[] Weights { get; }
        //public double[] Inputs { get; }//uncomment to learn
        private NeuronType NeuronType { get; set; }
        public double Output { get; private set; }

        public Neuron(int inputCount, NeuronType neuronType = NeuronType.Hidden)
        {
            Weights = new double[inputCount];
            //Inputs = new double[inputCount];
            NeuronType = neuronType;
        }

        public void SetRandomWeightsFromArray(double[] array)
        {
            for (var i = 0; i < Weights.Length; i++)
            {
                Weights[i] = array[i];
            }
        }

        public double FeedForward(double[] signals)
        {
            var sum = 0.0;
            if (this.NeuronType == NeuronType.Input)
            {
                sum += signals.Sum();

                Output = sum;
                return sum;
            }

            sum += Weights.Select((t, i) => signals[i] * this.Weights[i]).Sum();

            Output = MyMath.Sigmoid(sum);
            return Output;
        }
    }
}