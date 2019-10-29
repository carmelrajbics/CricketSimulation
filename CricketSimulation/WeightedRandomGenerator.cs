using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CricketSimulation.Interfaces;

namespace CricketSimulation
{
    public class WeightedRandomGenerator : IWeightedRandomGenerator
    {

        /// <summary>
        ///     Generates random number based on the given probability
        /// </summary>
        /// <param name="rand">The object of the Random class</param>
        /// <param name="probability">List of probability on which the random number will be generated</param>
        /// <returns>
        ///     The run/wicket scored by the ball
        /// </returns>
        public int generateWeightedList(Random rand, List<int> probability)
        {
            var weighed_list = new List<int>();
            // Loop over weights
            for (var i = 0; i < probability.Count; i++)
            {
                var multiples = probability[i];
                // Loop over the list of items
                for (var j = 0; j < multiples; j++)
                {
                    weighed_list.Add(i);
                }
            }
            return weighed_list[rand.Next(0, 100)];
        }
    }
}
