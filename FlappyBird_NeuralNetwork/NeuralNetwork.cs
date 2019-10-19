using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird_NeuralNetwork
{
    class Neuron
    {
        public int layerIndex;
        public int positionIndex;
        public double value = 0;
        public List<Synapse> synapses = null;
        
        public void sigmoid(double x)//sigmoid function
        {
            this.value = 1.0 / (1.0 + Math.Exp(-x));
        }

        private void ReLU(double x) //(leaky) rectified linear unit
        {
            if (x >= 0)
                this.value = x;
            else
                this.value = x / 20;
        }

        public void activationFunction(bool useRelu = false)
        {
            double sum = 0;
            if (synapses != null)
            {
                foreach (Synapse synapse in synapses)
                {
                    sum += synapse.originNeuron.value * synapse.weight;
                }

                //set proper value to this neuron
                if (useRelu)
                    ReLU(sum);
                else
                    sigmoid(sum);
            }
        }
    }

    class Synapse
    {        
        public Neuron originNeuron;
        public double weight = 0;        
    }

    public class NeuralNetwork
    {
        double CROSSOVER_RATE = 0.8;
        double MUTATION_RATE = 0.1;

        List<List<Neuron>> neuralLayers;

        public double fitness=0;        

        public NeuralNetwork(int inputLayerSize, int outputLayerSize, int hiddenLayersNumber, Random random)
        {
            neuralLayers = new List<List<Neuron>>();

            //create input layer
            List<Neuron> inputLayer = new List<Neuron>();
            for (int i = 0; i < inputLayerSize; i++)
            {
                inputLayer.Add(new Neuron { layerIndex = 0, positionIndex = i });
            }
            neuralLayers.Add(inputLayer);

            //create hidden layers
            for (int i = 0; i < hiddenLayersNumber; i++)
            {
                List<Neuron> hiddenLayer = new List<Neuron>();
                int size_of_hidden_layers = (inputLayerSize + outputLayerSize) / 2;
                for (int j = 0; j < size_of_hidden_layers; j++)
                {
                    hiddenLayer.Add(new Neuron { layerIndex = i + 1, positionIndex = j });
                }
                neuralLayers.Add(hiddenLayer);
            }            

            //create output layers
            List<Neuron> outputLayer = new List<Neuron>();
            for (int i = 0; i < outputLayerSize; i++)
            {
                outputLayer.Add(new Neuron { layerIndex = (1 + hiddenLayersNumber + 1), positionIndex = i });
            }
            neuralLayers.Add(outputLayer);

            //create all the synapses between neurons with random weights(genes)            
            for (int i = 1; i < neuralLayers.Count; i++)//for each layer
            {
                for (int j = 0; j < neuralLayers[i].Count; j++)//for each neuron in layer
                {
                    Neuron currentLayerNeuron = neuralLayers[i][j];
                    currentLayerNeuron.synapses = new List<Synapse>();

                    for (int k = 0; k < neuralLayers[i-1].Count; k++)//for each neuron in previous layer
                    {                        
                        Neuron previousLayerNeuron = neuralLayers[i - 1][k];
                        //create synapse with each neuron in previous layer
                        currentLayerNeuron.synapses.Add(new Synapse { originNeuron = previousLayerNeuron, weight = random.NextDouble() * 2 - 1 });                      
                    }
                }
            }
        }

        public void CalculateFitness(int numberOfPipesPassed, int distanceToCenterPipeGap, int distanceToPipePair, int windowHeight, int windowWidth)
        {
            double bonusCloseToCenterGap = 1 - (Math.Abs(distanceToCenterPipeGap) * 100 / windowHeight) * 0.01;
            double bonusCloseToPipePair = 1 - (Math.Abs(distanceToPipePair) * 100 / windowWidth) * 0.01;
            if (numberOfPipesPassed == 0)
            {
                //smallest percentage of distance to center pipe from total window height is better
                fitness = bonusCloseToCenterGap + bonusCloseToPipePair;
            }
            else
            {
                fitness = numberOfPipesPassed + bonusCloseToCenterGap + bonusCloseToPipePair;
            }
        }

        public void Crossover(NeuralNetwork crossover_neuralNet, Random random)
        {
            //check if crossover rate does not exceed limit
            if (random.NextDouble() > CROSSOVER_RATE)
                return;

            //check if neural networks are identical in structure (layers and number of neurons per layer)
            if (this.neuralLayers.Count != crossover_neuralNet.neuralLayers.Count
                || this.neuralLayers[0].Count != crossover_neuralNet.neuralLayers[0].Count
                || this.neuralLayers[1].Count != crossover_neuralNet.neuralLayers[1].Count
                || this.neuralLayers[this.neuralLayers.Count - 1].Count != crossover_neuralNet.neuralLayers[this.neuralLayers.Count - 1].Count)
                return;

            for (int i = 1; i < this.neuralLayers.Count; i++)
            {
                for (int j = 0; j < this.neuralLayers[i].Count; j++)
                {
                    for (int k = 0; k < this.neuralLayers[i][j].synapses.Count; k++)
                    {
                        this.neuralLayers[i][j].synapses[k].weight = (this.neuralLayers[i][j].synapses[k].weight + crossover_neuralNet.neuralLayers[i][j].synapses[k].weight) / 2.0;
                    }                    
                }
            }
        }

        public void Mutate(Random random)
        {
            for (int i = 1; i < this.neuralLayers.Count; i++)
            {
                for (int j = 0; j < this.neuralLayers[i].Count; j++)
                {
                    for (int k = 0; k < this.neuralLayers[i][j].synapses.Count; k++)
                    {
                        if (random.NextDouble() < MUTATION_RATE)
                            this.neuralLayers[i][j].synapses[k].weight = random.NextDouble() * 2 - 1;
                    }
                }
            }
        }

        public NeuralNetwork Duplicate()
        {
            NeuralNetwork duplicateNeuralNetwork = new NeuralNetwork(this.neuralLayers[0].Count,
                                                                     this.neuralLayers[this.neuralLayers.Count - 1].Count,
                                                                     this.neuralLayers.Count-2,
                                                                      new Random());

            for (int i = 1; i < duplicateNeuralNetwork.neuralLayers.Count; i++)
            {
                for (int j = 0; j < duplicateNeuralNetwork.neuralLayers[i].Count; j++)
                {
                    for (int k = 0; k < duplicateNeuralNetwork.neuralLayers[i][j].synapses.Count; k++)
                    {
                        duplicateNeuralNetwork.neuralLayers[i][j].synapses[k].weight = this.neuralLayers[i][j].synapses[k].weight;
                    }
                }
            }

            return duplicateNeuralNetwork;
        }

        //return output neuron values
        public List<double> ComputeOutput(List<double> inputNeuronValues)
        {
            //set up input layer values
            if(neuralLayers!=null)
            {
                if(inputNeuronValues.Count >= neuralLayers[0].Count)
                {
                    for (int i = 0; i < neuralLayers[0].Count; i++)
                    {
                        neuralLayers[0][i].value = inputNeuronValues[i];
                    }
                }
            }

            //fire up the network
            foreach (List<Neuron> neuralLayer in neuralLayers)
            {
                foreach (Neuron neuron in neuralLayer)
                {
                    if (neuron.layerIndex < neuralLayers.Count - 1)//for the hidden layers we use ReLu activation function
                        neuron.activationFunction(true);
                    else
                        neuron.activationFunction(false);
                }
            }

            return neuralLayers[neuralLayers.Count - 1].Select(n => n.value).ToList();
        }
    }
}
