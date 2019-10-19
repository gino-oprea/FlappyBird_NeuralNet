using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird_NeuralNetwork
{
    public class BirdSensors
    {
        public int distanceToNextPipePair;
        public int distanceToNextUpperPipe;
        public int distanceToNextBottomPipe;
    }

    public class GameEngine
    {
        public bool jumping = false;
        public int pipeSpeed = 5;
        public int distanceBetweenPipes = 500;
        public int gravity = 8;
        public int jump = 5;
        public int birdPopulation = 1;

        public int minimumPipeLength = 150;
        public int pipeGap = 140;

        Form form;
        System.Windows.Forms.Timer gameTimer;

        public List<List<PictureBox>> pipes;

        public List<PictureBox> flapyBirds;

        public GameEngine(Form form, System.Windows.Forms.Timer gameTimer)
        {
            this.form = form;
            this.gameTimer = gameTimer;

            flapyBirds = GenerateBirds();

            //NeuralNetwork skyNet = new NeuralNetwork(3, 1, 1);
            //Random rand = new Random();
            //List<double> output;
            //while (true)
            //{
            //    output = skyNet.ComputeOutput(new List<double> { (double)rand.Next(-500, 500), (double)rand.Next(-500, 500), (double)rand.Next(-500, 500) });
            //}
            
        }

        //calculate for each bird
        public List<BirdSensors> getBirdSensors()
        {
            List<BirdSensors> birdSensors = new List<BirdSensors>();
            foreach (PictureBox bird in flapyBirds)
            {
                foreach (List<PictureBox> pipePair in pipes)
                {
                    if (bird.Left - (pipePair[0].Left + pipePair[0].Width/2)<0)//this is the next pipe pair
                    {
                        BirdSensors birdSensor = new BirdSensors();
                        birdSensor.distanceToNextPipePair = (pipePair[0].Left + pipePair[0].Width / 2) - (bird.Left + bird.Width / 2);
                        birdSensor.distanceToNextUpperPipe = bird.Top - pipePair[0].Height;
                        birdSensor.distanceToNextBottomPipe = pipePair[1].Top - bird.Top;

                        birdSensors.Add(birdSensor);
                        break;
                    }
                }
            }

            return birdSensors;
        }      


        public List<PictureBox> GenerateBirds()
        {
            List<PictureBox> birds = new List<PictureBox>();
            for (int i = 0; i < birdPopulation; i++)
            {
                PictureBox flappyBird = new PictureBox();
                flappyBird.Width = 39;
                flappyBird.Height = 34;
                flappyBird.Location = new System.Drawing.Point(112, initialBirdPosition(-i));
                flappyBird.Image = Properties.Resources.bird;
                flappyBird.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


                form.Controls.Add(flappyBird);
                birds.Add(flappyBird);

            }

            return birds;
        }

        public int initialBirdPosition(int variable)
        {
            return (form.Height / 2) + variable;
        }

        public void generatePipes()
        {
            if (pipes == null)
                pipes = new List<List<PictureBox>>();

            List<PictureBox> pipePair = generatePipePair();

            pipes.Add(pipePair);
        }

        public List<PictureBox> generatePipePair()
        {
            PictureBox pipeTop = new PictureBox();
            pipeTop.Width = 138;
            pipeTop.Location = new System.Drawing.Point(form.Width - pipeTop.Width, 3);
            pipeTop.Image = Properties.Resources.pipedown;
            pipeTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            PictureBox pipeBottom = new PictureBox();
            pipeBottom.Width = 138;
            pipeBottom.Location = new System.Drawing.Point(form.Width - pipeTop.Width, 707);
            pipeBottom.Image = Properties.Resources.pipe;
            pipeBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            randomizePipeSize(pipeTop, pipeBottom);

            List<PictureBox> pipePair = new List<PictureBox>();
            pipePair.Add(pipeTop);
            pipePair.Add(pipeBottom);


            foreach (PictureBox pipe in pipePair)
            {
                pipe.Left = form.Width - pipe.Width;
                form.Controls.Add(pipe);
            }

            return pipePair;
        }

        public void resetGame()
        {
            for (int i = 0; i < pipes.Count; i++)
            {
                form.Controls.Remove(pipes[i][0]);//top
                form.Controls.Remove(pipes[i][1]);//bottom                
            }
            pipes = null;

            bool jumping = false;

            for (int i = 0; i < birdPopulation; i++)
            {
                flapyBirds[i].Top = initialBirdPosition(-i);
            }
        }

        public void randomizePipeSize(PictureBox pipeTop, PictureBox pipeBottom)
        {
            Random random = new Random();               
            pipeTop.Height = random.Next(minimumPipeLength, form.Height - minimumPipeLength - pipeGap);

            pipeBottom.Top = pipeTop.Height + pipeGap;
            pipeBottom.Height = form.Height - pipeTop.Height + pipeGap;
        }
        public void endGame()
        {
            this.gameTimer.Stop();
            Thread.Sleep(1000);
            this.resetGame();            
            this.gameTimer.Start();
        }
    }
}
