using System;
using System.Dynamic;

namespace MonoGameWindowsDesktopApplication1.AI
{
    public class NeuronLayer
    {
        public Neuron[] Neurons { get; }
        public int LayerNeuronsCount { get; }
        private NeuronType NeuronType { get; }

        public NeuronLayer(Neuron[] neurons, int layerNeuronsCount, NeuronType neuronType = NeuronType.Hidden)
        {
            Neurons = neurons;
            LayerNeuronsCount = layerNeuronsCount;
            NeuronType = neuronType;
        }

        public double[] LayerOutputsArray()
        {
            var outputs = new double[LayerNeuronsCount];
            for (var i = 0; i < LayerNeuronsCount; i++)
            {
                outputs[i] = Neurons[i].Output;
            }

            return outputs;
        }
    }
}