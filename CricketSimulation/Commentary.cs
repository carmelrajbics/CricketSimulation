using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CricketSimulation.Interfaces;

namespace CricketSimulation
{
    public class Commentary : ICommentary
    {
        /// <summary>
        /// Writes commentry on every ball including over end commentry on the output stream 
        /// </summary>
        /// <param name="over">The ongoing over</param>
        /// <param name="ball">Bowled ball in the over</param>
        /// <param name="runs">Run scored for the ball</param>
        /// <param name="commentryAfterOver">Flag indicates if the over is completed or not 
        /// by passing true or false value(Default value is false) </param>
        public void printCommentry(int over = 0, int ball = 0, int runs = 0, Player player = null, bool commentryAfterOver = false,
            int totalOvers = 0, int totalRunToWin = 0)
        {
            if (commentryAfterOver)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine();
                Console.WriteLine($"{totalOvers - over} overs left. {totalRunToWin} runs to win");

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                string comment = runs == 1 ? "run" : "runs";
                Console.WriteLine($"\t{over}.{ball} {player.Name} scores {runs} {comment}");
            }
            Console.ResetColor();
        }


        /// <summary>
        ///     Print the score board of the players played
        /// </summary>
        /// <param name="players">List of players</param>
        public void printScores(List<Player> players)
        {
            //Console.WriteLine("\n\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("_______________________________________________________________________________________");
            Console.WriteLine("\t\t\t\tSCORE BOARD");
            Console.WriteLine("_______________________________________________________________________________________");

            var table = new TablePrinter("Name", "Runs", "1s", "2s", "3s", "4s", "5s", "6s", "Stike Rate");
            foreach (var player in players)
            {
                if (player.TotalBallsPlayed > 0)
                {
                    if (player.IsOut == false)
                    {
                        table.AddRow(player.Name, player.Scores + "* (" + player.TotalBallsPlayed + " balls)", player.OneRun,
                            player.TwoRun, player.ThreeRuns, player.FourRuns, player.FiveRuns, player.SixRuns, Math.Round(((double)player.Scores / (double)player.TotalBallsPlayed) * 100, 2) + "%");
                    }
                    else
                    {
                        table.AddRow(player.Name, player.Scores + " (" + player.TotalBallsPlayed + " balls)", player.OneRun,
                            player.TwoRun, player.ThreeRuns, player.FourRuns, player.FiveRuns, player.SixRuns, Math.Round(((double)player.Scores / (double)player.TotalBallsPlayed) * 100, 2) + "%");
                    }
                }
            }
            table.Print();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n----------------------------------------------------------------------------------------");
        }
    }
}
