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
            this.lblScore = new System.Windows.Forms.Label();
            this.lblEpoch = new System.Windows.Forms.Label();
            this.btnEnableAI = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ground)).BeginInit();
            this.SuspendLayout();
            // 
            // ground
            // 
            this.ground.Image = global::FlappyBird_NeuralNetwork.Properties.Resources.ground;
            this.ground.Location = new System.Drawing.Point(4, 844);
            this.ground.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ground.Name = "ground";
            this.ground.Size = new System.Drawing.Size(2034, 81);
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
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(612, 861);
            this.lblScore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(64, 25);
            this.lblScore.TabIndex = 4;
            this.lblScore.Text = "Score";
            // 
            // lblEpoch
            // 
            this.lblEpoch.AutoSize = true;
            this.lblEpoch.Location = new System.Drawing.Point(371, 864);
            this.lblEpoch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEpoch.Name = "lblEpoch";
            this.lblEpoch.Size = new System.Drawing.Size(108, 25);
            this.lblEpoch.TabIndex = 5;
            this.lblEpoch.Text = "Generation";
            // 
            // btnEnableAI
            // 
            this.btnEnableAI.Location = new System.Drawing.Point(1533, 861);
            this.btnEnableAI.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnEnableAI.Name = "btnEnableAI";
            this.btnEnableAI.Size = new System.Drawing.Size(301, 40);
            this.btnEnableAI.TabIndex = 7;
            this.btnEnableAI.TabStop = false;
            this.btnEnableAI.Text = "Enable AI";
            this.btnEnableAI.UseVisualStyleBackColor = true;
            this.btnEnableAI.Click += new System.EventHandler(this.btnEnableAI_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(1103, 861);
            this.btnPause.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(116, 38);
            this.btnPause.TabIndex = 8;
            this.btnPause.TabStop = false;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(1261, 861);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(103, 39);
            this.btnPlay.TabIndex = 9;
            this.btnPlay.TabStop = false;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Location = new System.Drawing.Point(4, 855);
            this.btnDetails.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(138, 42);
            this.btnDetails.TabIndex = 10;
            this.btnDetails.TabStop = false;
            this.btnDetails.Text = "Details";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(2030, 885);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnEnableAI);
            this.Controls.Add(this.lblEpoch);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.ground);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximumSize = new System.Drawing.Size(2054, 949);
            this.MinimumSize = new System.Drawing.Size(2054, 949);
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
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblEpoch;
        private System.Windows.Forms.Button btnEnableAI;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnDetails;
    }
}

