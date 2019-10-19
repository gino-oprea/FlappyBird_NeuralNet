namespace FlappyBird_NeuralNetwork
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.ground = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.lblDistanceToPipe = new System.Windows.Forms.Label();
            this.lblDistanceToUpperPipe = new System.Windows.Forms.Label();
            this.lblDistanceToBootomPipe = new System.Windows.Forms.Label();
            this.btnEnableAI = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ground)).BeginInit();
            this.SuspendLayout();
            // 
            // ground
            // 
            this.ground.Image = global::FlappyBird_NeuralNetwork.Properties.Resources.ground;
            this.ground.Location = new System.Drawing.Point(0, 910);
            this.ground.Name = "ground";
            this.ground.Size = new System.Drawing.Size(1831, 54);
            this.ground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ground.TabIndex = 3;
            this.ground.TabStop = false;
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 15;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // lblDistanceToPipe
            // 
            this.lblDistanceToPipe.AutoSize = true;
            this.lblDistanceToPipe.Location = new System.Drawing.Point(12, 927);
            this.lblDistanceToPipe.Name = "lblDistanceToPipe";
            this.lblDistanceToPipe.Size = new System.Drawing.Size(0, 17);
            this.lblDistanceToPipe.TabIndex = 4;
            // 
            // lblDistanceToUpperPipe
            // 
            this.lblDistanceToUpperPipe.AutoSize = true;
            this.lblDistanceToUpperPipe.Location = new System.Drawing.Point(289, 927);
            this.lblDistanceToUpperPipe.Name = "lblDistanceToUpperPipe";
            this.lblDistanceToUpperPipe.Size = new System.Drawing.Size(0, 17);
            this.lblDistanceToUpperPipe.TabIndex = 5;
            // 
            // lblDistanceToBootomPipe
            // 
            this.lblDistanceToBootomPipe.AutoSize = true;
            this.lblDistanceToBootomPipe.Location = new System.Drawing.Point(580, 927);
            this.lblDistanceToBootomPipe.Name = "lblDistanceToBootomPipe";
            this.lblDistanceToBootomPipe.Size = new System.Drawing.Size(0, 17);
            this.lblDistanceToBootomPipe.TabIndex = 6;
            // 
            // btnEnableAI
            // 
            this.btnEnableAI.Location = new System.Drawing.Point(1596, 927);
            this.btnEnableAI.Name = "btnEnableAI";
            this.btnEnableAI.Size = new System.Drawing.Size(219, 23);
            this.btnEnableAI.TabIndex = 7;
            this.btnEnableAI.TabStop = false;
            this.btnEnableAI.Text = "Enable AI";
            this.btnEnableAI.UseVisualStyleBackColor = true;
            this.btnEnableAI.Click += new System.EventHandler(this.btnEnableAI_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(1286, 924);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(84, 24);
            this.btnPause.TabIndex = 8;
            this.btnPause.TabStop = false;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(1387, 924);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 26);
            this.btnPlay.TabIndex = 9;
            this.btnPlay.TabStop = false;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(1827, 953);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnEnableAI);
            this.Controls.Add(this.lblDistanceToBootomPipe);
            this.Controls.Add(this.lblDistanceToUpperPipe);
            this.Controls.Add(this.lblDistanceToPipe);
            this.Controls.Add(this.ground);
            this.MaximumSize = new System.Drawing.Size(1845, 1000);
            this.MinimumSize = new System.Drawing.Size(1845, 1000);
            this.Name = "Form1";
            this.Text = "FlappyBird Neural Network";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameKeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.ground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox ground;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label lblDistanceToPipe;
        private System.Windows.Forms.Label lblDistanceToUpperPipe;
        private System.Windows.Forms.Label lblDistanceToBootomPipe;
        private System.Windows.Forms.Button btnEnableAI;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
    }
}

