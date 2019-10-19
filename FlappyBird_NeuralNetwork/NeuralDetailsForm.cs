using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird_NeuralNetwork
{
    public partial class NeuralDetailsForm : Form
    {
        DataTable dt;
        int ellipseDimension = 50;

        double synapseThresholdMax = 0.7;
        double synapseThresholdMiddle = 0.5;
        double synapseThresholdLowMiddle = 0.0;
        double synapseThresholdMin = -0.3;

        NeuralNetwork neuralNet;

        public NeuralDetailsForm(NeuralNetwork neuralNet)//List<NeuralDetails> detailsList)
        {
            this.neuralNet = neuralNet;
            InitializeComponent();            
        }

        public void UpdateDetails(NeuralNetwork neuralNet)
        {
            if (neuralNet != null)
            {
                this.neuralNet = neuralNet;
                pnlNeuralNet.Invalidate();
            }
        }

        private void pnlNeuralNet_Paint(object sender, PaintEventArgs e)
        {
            //NeuralNetwork neuralNet = new NeuralNetwork(3, 1, 2, new Random());
            if (neuralNet != null)
            {
                List<int> neuronsPerLayer = new List<int>();
                foreach (List<Neuron> layer in neuralNet.neuralLayers)
                {
                    neuronsPerLayer.Add(layer.Count);
                }

                int incremental = this.Width / (neuralNet.neuralLayers.Count + 1);

                List<int> xPositionsNeurons = new List<int>();
                for (int i = 0; i < neuralNet.neuralLayers.Count; i++)
                {
                    xPositionsNeurons.Add((i + 1) * incremental);
                }

                List<List<int>> yPositionNeurons = new List<List<int>>();
                for (int i = 0; i < neuronsPerLayer.Count; i++)
                {
                    List<int> yPosition = new List<int>();
                    int heightIncremental = this.Height / (neuronsPerLayer[i] + 1);
                    for (int j = 0; j < neuronsPerLayer[i]; j++)
                    {
                        yPosition.Add((j + 1) * heightIncremental);
                    }
                    yPositionNeurons.Add(yPosition);
                }



                Graphics g = pnlNeuralNet.CreateGraphics();
                Pen p = new Pen(Color.Black);

                SolidBrush sb = new SolidBrush(Color.Red);

                for (int i = 0; i < neuralNet.neuralLayers.Count; i++)
                {
                    for (int j = 0; j < neuralNet.neuralLayers[i].Count; j++)
                    {
                        g.DrawEllipse(p, xPositionsNeurons[i], yPositionNeurons[i][j], ellipseDimension, ellipseDimension);
                        g.FillEllipse(sb, xPositionsNeurons[i], yPositionNeurons[i][j], ellipseDimension, ellipseDimension);

                        if (i > 0)//from second layer foreward draw synapses
                        {
                            for (int k = 0; k < neuralNet.neuralLayers[i - 1].Count; k++)
                            {
                                Point originalNeuron = new Point(xPositionsNeurons[i - 1] + ellipseDimension / 2, yPositionNeurons[i - 1][k] + ellipseDimension / 2);
                                Point currentNeuron = new Point(xPositionsNeurons[i] + ellipseDimension / 2, yPositionNeurons[i][j] + ellipseDimension / 2);

                                if (neuralNet.neuralLayers[i][j].synapses[k].weight < synapseThresholdMin)
                                {
                                    p.Color = Color.Gray;
                                    p.Width = 1;
                                }
                                if (neuralNet.neuralLayers[i][j].synapses[k].weight >= synapseThresholdMin && neuralNet.neuralLayers[i][j].synapses[k].weight < synapseThresholdLowMiddle)
                                {
                                    p.Color = Color.Orange;
                                    p.Width = 2;
                                }
                                if (neuralNet.neuralLayers[i][j].synapses[k].weight >= synapseThresholdLowMiddle && neuralNet.neuralLayers[i][j].synapses[k].weight < synapseThresholdMiddle)
                                {
                                    p.Color = Color.Salmon;
                                    p.Width = 3;
                                }
                                if (neuralNet.neuralLayers[i][j].synapses[k].weight >= synapseThresholdMiddle && neuralNet.neuralLayers[i][j].synapses[k].weight < synapseThresholdMax)
                                {
                                    p.Color = Color.Red;
                                    p.Width = 4;
                                }
                                if (neuralNet.neuralLayers[i][j].synapses[k].weight >= synapseThresholdMax)
                                {
                                    p.Color = Color.DarkRed;
                                    p.Width = 5;
                                }

                                g.DrawLine(p, originalNeuron, currentNeuron);
                            }
                        }
                    }
                }
            }
        }
    }
}
