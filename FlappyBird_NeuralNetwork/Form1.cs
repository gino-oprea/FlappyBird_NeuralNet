using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FlappyBird_NeuralNetwork
{
    public partial class Form1 : Form
    {
        GameEngine gEngine;

        List<BirdSensors> sensors;

        public Form1()
        {           
            InitializeComponent();
            gEngine = new GameEngine(this, gameTimer);
        }        

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (gEngine.pipes == null || gEngine.pipes.Count == 0 ||
                    this.Width - gEngine.pipes[gEngine.pipes.Count - 1][0].Width - gEngine.pipes[gEngine.pipes.Count - 1][0].Left > gEngine.distanceBetweenPipes)
                gEngine.generatePipes();

            
            sensors = gEngine.getBirdSensors();
            lblDistanceToPipe.Text = "Dist pipe: " + sensors[0].distanceToNextPipePair.ToString();
            lblDistanceToUpperPipe.Text = "Dist upper: " + sensors[0].distanceToNextUpperPipe.ToString();
            lblDistanceToBootomPipe.Text = "Dist bottom: " + sensors[0].distanceToNextBottomPipe.ToString();

            for (int i = 0; i < gEngine.pipes.Count; i++)            
            {
                List<PictureBox> pipePair = gEngine.pipes[i];

                pipePair[0].Left -= gEngine.pipeSpeed;
                pipePair[1].Left -= gEngine.pipeSpeed;
            }

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
                    if (flappyBird.Bounds.IntersectsWith(ground.Bounds))
                    {
                        gEngine.endGame();
                        break_loop = true;
                        break;
                    }
                    else if (flappyBird.Bounds.IntersectsWith(pipePair[0].Bounds))
                    {
                        gEngine.endGame();
                        break_loop = true;
                        break;
                    }
                    else if (flappyBird.Bounds.IntersectsWith(pipePair[1].Bounds))
                    {
                        gEngine.endGame();
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
            if (e.KeyCode == Keys.Space)
            {
                foreach (PictureBox flappyBird in gEngine.flapyBirds)
                {
                    gEngine.jumping = true;
                    if (flappyBird.Top > gEngine.jump)
                        gEngine.gravity = -gEngine.jump;
                    else
                        gEngine.gravity = gEngine.jump;
                }
                
            }
        }

        private void GameKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gEngine.jumping = false;
                gEngine.gravity = gEngine.jump;

            }
        }

        
    }
}
