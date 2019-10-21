using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird_NeuralNetwork
{
    public class NeuralNetworkBreeding
    {
        public static List<NeuralNetwork> Breed(List<NeuralNetwork> birds, NeuralNetwork bestBirdUntilNow, Random random)
        {
            List<NeuralNetwork> results = new List<NeuralNetwork>();

            List<NeuralNetwork> sortedBirds = birds.OrderByDescending(b => b.fitness).ToList();            

            int topCount = sortedBirds.Count * 10 / 100;            

            //first 10% duplicate 10 times = 100%
            for (int i = 0; i < topCount; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    NeuralNetwork duplicateBird = sortedBirds[j].Duplicate();
                    results.Add(duplicateBird);
                }
            }
            //last 5% replace with best ever
            for (int i = results.Count/20; i < results.Count; i++)
            {
                results[i] = bestBirdUntilNow.Duplicate();
            }

            //crossover 2 by 2 and the ones in between mutate
            for (int i = 0; i < results.Count; i+=2)
            {
                if (results.Count > i + 1)
                {                    
                    results[i].Crossover(results[i + 1], random);
                    results[i + 1].Mutate(random);
                }
            }

            return results;
        }

        public static List<NeuralNetwork> BreedByMutationOnly(List<NeuralNetwork> birds, NeuralNetwork bestBirdUntilNow, Random random)
        {
            List<NeuralNetwork> results = new List<NeuralNetwork>();

            List<NeuralNetwork> sortedBirds = birds.OrderByDescending(b => b.fitness).ToList();


            int topCount = sortedBirds.Count * 20 / 100;            

            //first 20% duplicate 5 times = 100%
            for (int i = 0; i < topCount; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    NeuralNetwork duplicateBird = sortedBirds[j].Duplicate();
                    results.Add(duplicateBird);
                }                
            }

            //replace the last one with best bird ever
            results[results.Count - 1] = bestBirdUntilNow.Duplicate();

            //mutate all
            for (int i = 0; i < results.Count; i++)
            {                
                results[i].Mutate(random);                
            }            
            return results;
        }
    }
}
