using System;
using System.Collections.Generic;

namespace CricketSimulation.Interfaces
{
    public interface IWeightedRandomGenerator
    {
        int generateWeightedList(Random rand, List<int> probability);
    }
}
