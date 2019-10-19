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
        public NeuralDetailsForm(List<NeuralDetails> detailsList)
        {
            InitializeComponent();

            dt = new DataTable();
            dt.Columns.Add("BrainIndex");
            dt.Columns.Add("DistanceToPipePair");
            dt.Columns.Add("DistanceToPipeTop");
            dt.Columns.Add("DistanceToPipeBottom");
            dt.Columns.Add("Output");


            for (int i = 0; i < detailsList.Count; i++)
            {
                NeuralDetails detail = detailsList[i];
                dt.Rows.Add(new object[] { i, detail.distanceToNextPipePair, detail.distanceToNextUpperPipe, detail.distanceToNextBottomPipe, detail.output });
            }
            

            gvNeuralNet.DataSource = dt;                
        }

        public void UpdateDetails(List<NeuralDetails> detailsList)
        {
            dt.Rows.Clear();
            for (int i = 0; i < detailsList.Count; i++)
            {
                NeuralDetails detail = detailsList[i];
                dt.Rows.Add(new object[] { i, detail.distanceToNextPipePair, detail.distanceToNextUpperPipe, detail.distanceToNextBottomPipe, detail.output });                     
                gvNeuralNet.DataSource = dt;                
            }
        }
    }
}
