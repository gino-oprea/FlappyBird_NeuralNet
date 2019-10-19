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

        NeuralDetailsForm detailsForm;

        public Form1()
        {
            AI_enabled = false;
            InitializeComponent();
            gEngine = new GameEngine(this, gameTimer);

            detailsForm = new NeuralDetailsForm(gEngine.detailsList);           
        }        

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (gEngine.pipes == null || gEngine.pipes.Count == 0 ||
                    this.Width - gEngine.pipes[gEngine.pipes.Count - 1][0].Width - gEngine.pipes[gEngine.pipes.Count - 1][0].Left > gEngine.distanceBetweenPipes)
                gEngine.generatePipes();

            
            //sensors = gEngine.getBirdSensors();
            //lblDistanceToPipe.Text = "Dist pipe: " + sensors[0].distanceToNextPipePair.ToString();
            //lblDistanceToUpperPipe.Text = "Dist upper: " + sensors[0].distanceToNextUpperPipe.ToString();
            //lblDistanceToBootomPipe.Text = "Dist bottom: " + sensors[0].distanceToNextBottomPipe.ToString();
            if(AI_enabled)
            {
                List<double> actions = gEngine.UpdateBirdBrainInput();
                for (int i = 0; i < actions.Count; i++)
                {
                    if(actions[i]>0.5)//jump
                    {                        
                        if (gEngine.flapyBirds[i].Top > gEngine.jump)
                            gEngine.flapyBirds[i].Top += -gEngine.jump;
                        else
                            gEngine.flapyBirds[i].Top += gEngine.jump;
                    }
                    else//don't jump
                    {
                        gEngine.flapyBirds[i].Top += gEngine.jump;
                    }
                }

                detailsForm.UpdateDetails(gEngine.detailsList);
            }


            for (int i = 0; i < gEngine.pipes.Count; i++)            
            {
                List<PictureBox> pipePair = gEngine.pipes[i];

                pipePair[0].Left -= gEngine.pipeSpeed;
                pipePair[1].Left -= gEngine.pipeSpeed;
            }

            if (!AI_enabled)
                foreach (PictureBox flappyBird in gEngine.flapyBirds)
                {
                    flappyBird.Top += gEngine.gravity;
                }

            bool break_loop = false;
            for (int i = 0; i < gEngine.pipes.Count; i++)
            {
                List<PictureBox> pipePair = gEngine.pipes[i];
                if (pipePair[0].Left < -100)
                {
                    this.Controls.Remove(pipePair[0]);//top
                    this.Controls.Remove(pipePair[1]);//bottom

                    gEngine.pipes.Remove(pipePair);
                }
               
                foreach (PictureBox flappyBird in gEngine.flapyBirds)
                {
                    if (flappyBird.Bounds.IntersectsWith(ground.Bounds)
                        || flappyBird.Bounds.IntersectsWith(pipePair[0].Bounds)
                        || flappyBird.Bounds.IntersectsWith(pipePair[1].Bounds))
                    {
                        flappyBird.Left = -600;//bird is dead
                    }

                    if (gEngine.flapyBirds.FindAll(b=>b.Left == -600).Count == gEngine.flapyBirds.Count)//all birds are dead
                    {
                        gEngine.endGame();
                        gEngine.startGame();
                        break_loop = true;
                        break;
                    }
                }
                if (break_loop)
                    break;
            }
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

            
            detailsForm.Show();
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
    }
}
