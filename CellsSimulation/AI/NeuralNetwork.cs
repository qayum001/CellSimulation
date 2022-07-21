
using System;
using SharpDX.Direct2D1;

namespace MonoGameWindowsDesktopApplication1.AI
{
    public class NeuralNetwork
    {
        private readonly Topology _topology;
        private readonly NeuronLayer[] _layers;
        private readonly Random _random;

        public NeuralNetwork(Topology topology)
        {
            _topology = topology;
            _layers = new NeuronLayer[_topology.HiddenNeuronsArray.Length + 2];
            _random = new Random();
            
            CreateInputNeurons();
            CreateHiddenNeurons();
            CreateOutputNeurons();
            SetNeuronWeights();
        }

        public double[] FeedForward(double[] signals)
        {
            var results = new double[_topology.OutputNeuronsCount];
            
            SendInputSignals(signals);
            FeedForwardAllLayersAfterInput();
            
            for (var i = 0; i < _topology.OutputNeuronsCount; i++)
            {
                results[i] = _layers[^1].Neurons[i].Output;
            }

            return results;
        }
        private void FeedForwardAllLayersAfterInput()
        {
            for (var i = 1; i < _layers.Length; i++)
            {
                var previousLayerOutputs = _layers[i - 1].LayerOutputsArray();
                var currentLayer = _layers[i];
                for (var j = 0; j < _layers[i].Neurons.Length; j++)
                {
                    currentLayer.Neurons[j].FeedForward(previousLayerOutputs);
                }
            }
        }
        private void SendInputSignals(double[] inputSignals)
        {
            if (inputSignals == null) throw new ArgumentNullException(nameof(inputSignals));
            if (_layers[0].LayerNeuronsCount < inputSignals.Length) throw new IndexOutOfRangeException();

            for (var i = 0; i < _layers[0].LayerNeuronsCount; i++)
            {
                _layers[0].Neurons[i].FeedForward(new double[]{inputSignals[i]});
            }
        }
        private double[] GenerateRandomWeights(int weightsCount)
        {
            var weights = new double[weightsCount];
            for (var i = 0; i < weightsCount; i++)
            {
                weights[i] = _random.NextDouble();
            }
            return weights;
        }
        private void SetNeuronWeights()
        {
            for (var i = 0; i < _layers[0].LayerNeuronsCount; i++)
            {
                _layers[0].Neurons[i].Weights[0] = 1;
            }

            for (var i = 1; i < _layers.Length; i++)
            {
                for (var j = 0; j < _layers[i].LayerNeuronsCount; j++)
                {
                    _layers[i].Neurons[j].SetRandomWeightsFromArray(GenerateRandomWeights(_layers[i].Neurons[j].Weights.Length));
                }
            }
        }
        private void CreateOutputNeurons()
        {
            var outputNeurons = new Neuron[_topology.OutputNeuronsCount];
            for (var i = 0; i < _topology.OutputNeuronsCount; i++)
            {
                outputNeurons[i] = new Neuron(_layers[^2].LayerNeuronsCount, NeuronType.Output);
            }

            _layers[^1] = new NeuronLayer(outputNeurons, _topology.OutputNeuronsCount, NeuronType.Output);
        }
        private void CreateHiddenNeurons()
        {
            for (var i = 0; i < _topology.HiddenNeuronsArray.Length; i++)
            {
                var hiddenLayer = new Neuron[_topology.HiddenNeuronsArray[i]];
                for (var j = 0; j < _topology.HiddenNeuronsArray[i]; j++)
                {
                    hiddenLayer[j] = new Neuron(_topology.InputNeuronsCount, NeuronType.Hidden);
                }

                _layers[i + 1] = new NeuronLayer(hiddenLayer, _topology.HiddenNeuronsArray[i], NeuronType.Hidden);
            }
        }
        private void CreateInputNeurons()
        {
            var inputNeurons = new Neuron[_topology.InputNeuronsCount];
            for (var i = 0; i < _topology.InputNeuronsCount; i++)
            {
                inputNeurons[i] = new Neuron(1, NeuronType.Input);
            }

            _layers[0] = new NeuronLayer(inputNeurons, _topology.InputNeuronsCount, NeuronType.Input);
        }
    }
}