using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketSimulation.Interfaces
{
    public interface ICommentary
    {
        void printCommentry(int over = 0, int ball = 0, int runs = 0, Player player = null, bool commentryAfterOver = false,
            int totalOvers = 0, int totalRunToWin = 0);
        void printScores(List<Player> players);
    }
}
