using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FlappyBird_NeuralNetwork
{
    public partial class Form1 : Form
    {
        GameEngine gEngine;
        List<BirdSensors> sensors;
        bool AI_enabled;
        bool detailsOpen = false;

        NeuralDetailsForm detailsForm;

        public Form1()
        {
            AI_enabled = false;
            InitializeComponent();

            gEngine = new GameEngine(this, gameTimer, ground);                
        }        

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            gEngine.UpdateGameFrame(detailsForm);
        }

        private void GameKeyDown(object sender, KeyEventArgs e) 
        {
            if (!AI_enabled)
            {
                if (e.KeyCode == Keys.Space)
                {
                    foreach (PictureBox flappyBird in gEngine.flapyBirds)
                    {                        
                        if (flappyBird.Top > gEngine.jump)
                            gEngine.gravity = -gEngine.jump;
                        else
                            gEngine.gravity = gEngine.jump;
                    }

                }
            }
        }

        private void GameKeyUp(object sender, KeyEventArgs e)
        {
            if (!AI_enabled)
            {
                if (e.KeyCode == Keys.Space)
                {                    
                    gEngine.gravity = gEngine.jump;
                }
            }
        }

        private void btnEnableAI_Click(object sender, EventArgs e)
        {
            if(!AI_enabled)
            {
                this.ActiveControl = null;                
                AI_enabled = true;
                btnEnableAI.Text = "Disable AI";
                
                gEngine.endGame();
                gEngine.EnableAI();                
                gEngine.startGame();
            }
            else
            {
                this.ActiveControl = null;
                AI_enabled = false;
                btnEnableAI.Text = "Enable AI";
                
                gEngine.endGame();
                gEngine.DisableAI();                
                gEngine.startGame();
            }           
        }

        private void btnPause_Click(object sender, EventArgs e)
        {            
            this.gameTimer.Stop();
            this.ActiveControl = null;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.gameTimer.Start();
            this.ActiveControl = null;
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;

            if (AI_enabled)
            {
                detailsForm = new NeuralDetailsForm(gEngine.fittestBrain != null ? gEngine.fittestBrain : gEngine.brains[0]);//gEngine.detailsList);
                detailsForm.Show();
            }
        }
    }
}
