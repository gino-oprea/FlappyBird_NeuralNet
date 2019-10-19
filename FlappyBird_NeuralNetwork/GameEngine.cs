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

    public class NeuralDetails
    {
        public double distanceToNextPipePair;
        public double distanceToNextUpperPipe;
        public double distanceToNextBottomPipe;
        public double output;
    }

    public class GameEngine
    {        
        public int pipeSpeed = 5;
        public int distanceBetweenPipes = 300;
        public int gravity = 8;
        public int jump = 5;

        public int epochNo = 1;

        public int[] gameScore;//how many pipes were passed

        public int birdPopulation = 1;

        public int minimumPipeLength = 120;
        public int pipeGap = 100;

        Form form;        
        System.Windows.Forms.Timer gameTimer;
        PictureBox ground;

        public List<List<PictureBox>> pipes;

        public List<PictureBox> flapyBirds;

        private List<NeuralNetwork> brains;

        public NeuralNetwork fittestBrain;

        public List<NeuralDetails> detailsList = new List<NeuralDetails>();

        Random random = new Random();

        bool AI_enabled = false;

        public GameEngine(Form form, System.Windows.Forms.Timer gameTimer, PictureBox ground)
        {
            this.form = form;
            this.gameTimer = gameTimer;
            this.ground = ground;

            
            flapyBirds = GenerateBirds();            
        }

        public void EnableAI()
        {
            birdPopulation = 30;

            brains = new List<NeuralNetwork>();
            for (int i = 0; i < birdPopulation; i++)
            {
                NeuralNetwork brain = new NeuralNetwork(3, 1, 2, random);
                brains.Add(brain);
            }

            AI_enabled = true;
        }
        public void DisableAI()
        {
            brains = null;
            birdPopulation = 1;
            AI_enabled = false;
        }        

        public List<double> UpdateBirdBrainInput()
        {
            List<double> outputActions = new List<double>();
            List<BirdSensors> birdSensors = getBirdSensors();
            for (int i = 0; i < brains.Count; i++)
            {
                NeuralNetwork brain = brains[i];

                //squash the input values to a number between 0 and 1
                double squashedDist1 = (birdSensors[i].distanceToNextPipePair * 100 / form.Width) * 0.01;
                double squashedDist2 = (birdSensors[i].distanceToNextUpperPipe * 100 / form.Height) * 0.01;
                double squashedDist3 = (birdSensors[i].distanceToNextBottomPipe * 100 / form.Height) * 0.01;

                List<double> neuralInputs = new List<double> { squashedDist1, squashedDist2, squashedDist3 };
                List<double> output = brain.ComputeOutput(neuralInputs);

                outputActions.Add(output[0]);//there is only one output neuron


                ////for showing details in next Form only
                if (detailsList.Count - 1 < i)
                    detailsList.Add(new NeuralDetails
                    {
                        distanceToNextPipePair = squashedDist1,
                        distanceToNextUpperPipe = squashedDist2,
                        distanceToNextBottomPipe = squashedDist3,
                        output = output[0]
                    });
                else
                {
                    detailsList[i].distanceToNextPipePair = squashedDist1;
                    detailsList[i].distanceToNextUpperPipe = squashedDist2;
                    detailsList[i].distanceToNextBottomPipe = squashedDist3;
                    detailsList[i].output = output[0];
                }
                ///////
            }

            return outputActions;
        }

        public int getMaxScore(int[] arr)
        {
            int max = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
            }

            return max;
        }


        public void UpdateGameFrame(NeuralDetailsForm detailsForm)
        {            
            form.Controls["lblEpoch"].Text = "Generation " + epochNo;
            form.Controls["lblScore"].Text = "Score: " + getMaxScore(gameScore);

            if (pipes == null || pipes.Count == 0 ||
                    form.Width - pipes[pipes.Count - 1][0].Width - pipes[pipes.Count - 1][0].Left > distanceBetweenPipes)
                generatePipes();

            if (AI_enabled)
            {
                List<double> actions = UpdateBirdBrainInput();
                for (int i = 0; i < actions.Count; i++)
                {
                    if (actions[i] > 0.5)//jump
                    {
                        if (flapyBirds[i].Top > jump)
                            flapyBirds[i].Top += -jump;
                        else
                            flapyBirds[i].Top += jump;
                    }
                    else//don't jump
                    {
                        flapyBirds[i].Top += jump;
                    }
                }                
            }


            for (int i = 0; i < pipes.Count; i++)
            {
                List<PictureBox> pipePair = pipes[i];

                pipePair[0].Left -= pipeSpeed;
                pipePair[1].Left -= pipeSpeed;
            }

            if (!AI_enabled)
                foreach (PictureBox flappyBird in flapyBirds)
                {
                    flappyBird.Top += gravity;
                }

            bool break_loop = false;
            for (int i = 0; i < pipes.Count; i++)
            {
                List<PictureBox> pipePair = pipes[i];
                if (pipePair[0].Left < -100)
                {
                    form.Controls.Remove(pipePair[0]);//top
                    form.Controls.Remove(pipePair[1]);//bottom

                    pipes.Remove(pipePair);

                    UpdateGameScoreForBirdsLeftAlive();
                }

                for (int j = 0; j < flapyBirds.Count; j++)
                {
                    PictureBox flappyBird = flapyBirds[j];

                    if (flappyBird.Bounds.IntersectsWith(ground.Bounds)
                        || flappyBird.Bounds.IntersectsWith(pipePair[0].Bounds)
                        || flappyBird.Bounds.IntersectsWith(pipePair[1].Bounds))
                    {
                        //set bird's brain fitness
                        if (AI_enabled)
                        {
                            List<BirdSensors> sensors = getBirdSensors();
                            int distanceToCenter = Math.Abs(sensors[j].distanceToNextUpperPipe - sensors[j].distanceToNextBottomPipe) / 2;
                            brains[j].CalculateFitness(gameScore[j], distanceToCenter, sensors[j].distanceToNextPipePair, form.Height, form.Width);
                        }
                        ////
                        flappyBird.Left = -600;//bird is dead
                    }

                    if (flapyBirds.FindAll(b => b.Left == -600).Count == flapyBirds.Count)//all birds are dead
                    {                       
                        endGame();

                        if (AI_enabled)
                        {
                            //epoch end
                            //breeeding time
                            epochNo++;
                            fittestBrain = brains.OrderByDescending(b => b.fitness).ToList()[0];
                            if (detailsForm != null)
                                detailsForm.UpdateDetails(fittestBrain);
                            //create new brains                                 
                            brains = NeuralNetworkBreeding.BreedByMutationOnly(brains, random);
                            birdPopulation = brains.Count;
                            ////                            
                        }

                        startGame();
                        break_loop = true;
                        break;
                    }
                }
               
                if (break_loop)
                    break;
            }
        }

        public void UpdateGameScoreForBirdsLeftAlive()
        {
            for (int i = 0; i < flapyBirds.Count; i++)
            {
                if (flapyBirds[i].Left != -600)
                    gameScore[i] += 1;
            }
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
            gameScore = new int[birdPopulation];

            List<PictureBox> birds = new List<PictureBox>();
            for (int i = 0; i < birdPopulation; i++)
            {
                PictureBox flappyBird = new PictureBox();
                flappyBird.Width = 25;
                flappyBird.Height = 21;
                flappyBird.Location = new System.Drawing.Point(112, initialBirdPosition(0));
                flappyBird.Image = Properties.Resources.bird;
                flappyBird.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


                form.Controls.Add(flappyBird);
                birds.Add(flappyBird);

                gameScore[i] = 0;//init game score for each bird
            }

            return birds;
        }

        public int initialBirdPosition(int variable)
        {
            return (form.Height / 3) + variable;
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
            pipeTop.Width = 100;
            pipeTop.Location = new System.Drawing.Point(form.Width - pipeTop.Width, 0);
            pipeTop.Image = Properties.Resources.pipedown;
            pipeTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            PictureBox pipeBottom = new PictureBox();
            pipeBottom.Width = 100;
            pipeBottom.Location = new System.Drawing.Point(form.Width - pipeTop.Width, form.Height);
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
             

        public void randomizePipeSize(PictureBox pipeTop, PictureBox pipeBottom)
        {
                           
            pipeTop.Height = random.Next(minimumPipeLength, form.Height - minimumPipeLength - pipeGap);

            pipeBottom.Top = pipeTop.Height + pipeGap;
            pipeBottom.Height = form.Height - pipeTop.Height + pipeGap;
        }
        public void endGame()
        {
            this.gameTimer.Stop();

            //remove pipes
            if (pipes != null)
                for (int i = 0; i < pipes.Count; i++)
                {
                    form.Controls.Remove(pipes[i][0]);//top
                    form.Controls.Remove(pipes[i][1]);//bottom                
                }
            pipes = null;

            //remove birds
            for (int i = 0; i < birdPopulation; i++)
            {
                if (flapyBirds.Count > 0)
                    form.Controls.Remove(flapyBirds[i]);
            }
            flapyBirds = null;

            Thread.Sleep(1000);
        }
        public void startGame()
        {
            //regenerate birds
            flapyBirds = GenerateBirds();
            for (int i = 0; i < birdPopulation; i++)
            {
                flapyBirds[i].Top = initialBirdPosition(0);
            }

            this.gameTimer.Start();
        }
    }
}
