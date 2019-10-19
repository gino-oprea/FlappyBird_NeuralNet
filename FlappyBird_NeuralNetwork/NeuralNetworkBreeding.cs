using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird_NeuralNetwork
{
    public class NeuralNetworkBreeding
    {
        public static List<NeuralNetwork> Breed(List<NeuralNetwork> birds, Random random)
        {
            List<NeuralNetwork> results = new List<NeuralNetwork>();

            List<NeuralNetwork> sortedBirds = birds.OrderByDescending(b => b.fitness).ToList();


            int topCount = (int)Math.Ceiling(Convert.ToDecimal(sortedBirds.Count * 100 / 20));
            int restCount = sortedBirds.Count - topCount;

            //first 20% crossover 2 by 2
            for (int i = 0; i < topCount; i+=2)
            {
                if (sortedBirds.Count > i + 1)
                {
                    NeuralNetwork crossBird = sortedBirds[i].Duplicate();
                    crossBird.Crossover(sortedBirds[i + 1], random);
                    results.Add(crossBird);
                }
            }

            //the rest mutate
            for (int i = topCount-1; i < sortedBirds.Count; i++)
            {
                NeuralNetwork mutatedBird = sortedBirds[i].Duplicate();
                mutatedBird.Mutate(random);
                results.Add(mutatedBird);
            }


            return results;
        }

        public static List<NeuralNetwork> BreedByMutationOnly(List<NeuralNetwork> birds, Random random)
        {
            List<NeuralNetwork> results = new List<NeuralNetwork>();

            List<NeuralNetwork> sortedBirds = birds.OrderByDescending(b => b.fitness).ToList();


            int topCount = sortedBirds.Count * 20 / 100;
            int restCount = sortedBirds.Count - topCount;

            //first 20% duplicate 5 times = 100%
            for (int i = 0; i < topCount; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    NeuralNetwork duplicateBird = sortedBirds[j].Duplicate();
                    results.Add(duplicateBird);
                }                
            }

            //mutate all
            for (int i = 0; i < results.Count; i++)
            {                
                results[i].Mutate(random);                
            }            
            return results;
        }
    }
}
