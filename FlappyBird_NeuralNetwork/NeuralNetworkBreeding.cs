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

        public static List<NeuralNetwork> BreedByMutationOnly(List<NeuralNetwork> birds, Random random)
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

            //mutate all
            for (int i = 0; i < results.Count; i++)
            {                
                results[i].Mutate(random);                
            }            
            return results;
        }
    }
}
