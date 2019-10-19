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
            this.gvNeuralNet = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gvNeuralNet)).BeginInit();
            this.SuspendLayout();
            // 
            // gvNeuralNet
            // 
            this.gvNeuralNet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvNeuralNet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvNeuralNet.Location = new System.Drawing.Point(12, 12);
            this.gvNeuralNet.Name = "gvNeuralNet";
            this.gvNeuralNet.RowTemplate.Height = 24;
            this.gvNeuralNet.Size = new System.Drawing.Size(927, 978);
            this.gvNeuralNet.TabIndex = 0;
            // 
            // NeuralDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 997);
            this.Controls.Add(this.gvNeuralNet);
            this.Name = "NeuralDetailsForm";
            this.Text = "NeuralDetailsForm";
            ((System.ComponentModel.ISupportInitialize)(this.gvNeuralNet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gvNeuralNet;
    }
}