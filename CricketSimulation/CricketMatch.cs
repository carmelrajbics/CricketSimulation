//Copyright 2019-2020 Carmel Raj, Bangalore 

using System;
using System.Collections.Generic;

namespace CricketSimulation
{
    /// <summary>
    ///     The class to simulate the cricket match
    /// </summary>
    public class CricketMatch
    {
        public int totalRunToWin, totalOvers;

        #region Constructor
        public CricketMatch(int over, int run)
        {
            this.totalOvers = over;
            this.totalRunToWin = run;
        }

        #endregion

        /// <summary>
        /// The main method to start the match
        /// </summary>
        public void BeginTheMatch()
        {
            //Number of wickets gone. Used as flag to check if all players are out
            int wicketLost = 0;

            //Number of balls in an over
            int balls = 6;

            //List of players in a match
            List<Player> players = new List<Player>
            {
               new Player("Pravin",0,false,0),
               new Player("Irfan",0,false,0),
               new Player("Jalindar",0,false,0),
               new Player("Vaishali",0,false,0),
            };

            //List of players with their respective probabilities to to score the runs
            Dictionary<string, List<int>> playersWithprobabilities = new Dictionary<string, List<int>>();
            playersWithprobabilities[players[0].Name] = (new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 });
            playersWithprobabilities[players[1].Name] = (new List<int> { 10, 40, 20, 05, 10, 01, 04, 10 });
            playersWithprobabilities[players[2].Name] = (new List<int> { 20, 30, 15, 05, 05, 01, 04, 20 });
            playersWithprobabilities[players[3].Name] = (new List<int> { 30, 25, 05, 00, 05, 01, 04, 30 });

            //Assuming first two Players on the ground (1st and 2nd on batting the field)
            List<Player> playing = new List<Player>();
            playing.Add(players[wicketLost]);
            playing.Add(players[wicketLost + 1]);

            Random random = new Random();

            //Loop total number of overs given
            for (int over = 0; over < totalOvers; over++)
            {
                printCommentry(over: over, commentryAfterOver: true);

                //Loop 6 balls by over
                for (int ball = 1; ball <= balls; ball++)
                {
                    playing[0].TotalBallsPlayed += 1;
                    int runs = generateWeightedList(random, playersWithprobabilities[playing[0].Name]);//random.Next(0, 7);

                    //runs being 7 is out
                    if (runs != 7)
                    {
                        //Reduce the run scored from the total run to win
                        totalRunToWin -= runs;

                        //Increasing the score of the player
                        playing[0].Scores += runs;

                        //Updating the score details for individual for detailed summary
                        updateScoreBoardForPlayer(runs, playing[0]);

                        //printing commentry for the ball
                        printCommentry(over, ball, runs, playing[0]);

                        //If the no. of runs is odd then change strike
                        if (runs % 2 != 0)
                        {
                            changeStrike(playing);
                        }
                        //More the given target made Remus team to win
                        if (totalRunToWin <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n\t\tRemus won by {3 - wicketLost} wickets and {(totalOvers - 1 - over) * 6 + (6 - ball)} balls remaing");
                            Console.ResetColor();
                            printScores(players);
                            return;
                        }
                    }
                    else
                    {
                        //If the run scored is 7 then the player is out
                        //Increasing the fall of wicket
                        wicketLost += 1;

                        //Making the player status is out
                        playing[0].IsOut = true;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\t{over}.{ball} {playing[0].Name} Out!");
                        Console.ResetColor();

                        //If all players of out, Remus team lost the match!
                        if (wicketLost == 3)
                        {
                            Console.WriteLine(Environment.NewLine);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"\t\tRemus team lost by {totalRunToWin} runs");
                            Console.ResetColor();
                            printScores(players);
                            return;
                        }
                        else
                            //New player come at the same position 
                            playing[0] = players[wicketLost + 1];
                    }
                }
                //Change strike at the end of every over
                changeStrike(playing);
            }

            //If total run to win is less then the target and the over is completed, Remus team lost the match!
            if (totalRunToWin > 0)
            {
                Console.WriteLine(Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\t\tRemus lost by {totalRunToWin} runs");
                Console.ResetColor();
                printScores(players);
            }
        }

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

        /// <summary>
        ///     Print the score board of the players played
        /// </summary>
        /// <param name="players">List of players</param>
        private void printScores(List<Player> players)
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

        /// <summary>
        ///     Changing the strike of the players
        /// </summary>
        /// <param name="playing">List of players(always two) on the ground</param>
        public void changeStrike(List<Player> playing)
        {
            var switchPlayer = playing[0];
            playing[0] = playing[1];
            playing[1] = switchPlayer;
        }

        /// <summary>
        /// Writes commentry on every ball including over end commentry on the output stream 
        /// </summary>
        /// <param name="over">The ongoing over</param>
        /// <param name="ball">Bowled ball in the over</param>
        /// <param name="runs">Run scored for the ball</param>
        /// <param name="commentryAfterOver">Flag indicates if the over is completed or not 
        /// by passing true or false value(Default value is false) </param>
        private void printCommentry(int over = 0, int ball = 0, int runs = 0, Player player = null, bool commentryAfterOver = false)
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
        /// Updating the individual score details for the playes
        /// </summary>
        /// <param name="runs">run scored for the ball </param>
        /// <param name="player">Player details</param>
        private void updateScoreBoardForPlayer(int runs, Player player)
        {
            switch (runs)
            {
                case 1:
                    player.OneRun += 1;
                    break;
                case 2:
                    player.TwoRun += 1;
                    break;
                case 3:
                    player.ThreeRuns += 1;
                    break;
                case 4:
                    player.FourRuns += 1;
                    break;
                case 5:
                    player.FiveRuns += 1;
                    break;
                case 6:
                    player.SixRuns += 1;
                    break;

            }
        }

    }
}
