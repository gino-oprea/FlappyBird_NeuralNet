namespace FlappyBird_NeuralNetwork
{
    partial class NeuralDetailsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlNeuralNet = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlNeuralNet
            // 
            this.pnlNeuralNet.Location = new System.Drawing.Point(1, 2);
            this.pnlNeuralNet.MaximumSize = new System.Drawing.Size(1180, 650);
            this.pnlNeuralNet.MinimumSize = new System.Drawing.Size(1180, 650);
            this.pnlNeuralNet.Name = "pnlNeuralNet";
            this.pnlNeuralNet.Size = new System.Drawing.Size(1180, 650);
            this.pnlNeuralNet.TabIndex = 0;
            this.pnlNeuralNet.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlNeuralNet_Paint);
            // 
            // NeuralDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 653);
            this.Controls.Add(this.pnlNeuralNet);
            this.MaximumSize = new System.Drawing.Size(1200, 700);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "NeuralDetailsForm";
            this.Text = "NeuralDetailsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlNeuralNet;
    }
}