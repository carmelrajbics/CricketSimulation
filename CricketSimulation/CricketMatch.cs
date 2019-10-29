//Copyright 2019-2020 Carmel Raj, Bangalore 

using System;
using System.Collections.Generic;
using Unity;
using CricketSimulation.Interfaces;

namespace CricketSimulation
{
    /// <summary>
    ///     The class to simulate the cricket match
    /// </summary>
    public class CricketMatch : ICricket
    {
        static int WICKET = 7;
        private readonly ICommentary _commentary;
        private readonly IPlayers _player;
        private readonly IWeightedRandomGenerator _weightedRandomGenerator;
        private readonly int _noOfOvers;
        private int _totalRunToWin;

        #region Constructor
        public CricketMatch([Dependency("totalRunToWin")] int totalRunToWin, [Dependency("noOfOvers")] int noOfOvers,
            IPlayers player, ICommentary commentary, IWeightedRandomGenerator weightedRandomGenerator)
        {
            _noOfOvers = noOfOvers;
            _totalRunToWin = totalRunToWin;
            _commentary = commentary;
            _player = player;
            _weightedRandomGenerator = weightedRandomGenerator;
        }

        #endregion

        /// <summary>
        /// The main method to start the match
        /// </summary>
        public void BeginTheMatch()
        {
            int targetRun = _totalRunToWin;
            //Number of wickets gone. Used as flag to check if all players are out
            int wicketLost = 0;

            //Number of balls in an over
            int balls = 6;

            //List of players in a match
            List<Player> players = _player.getPlayersList();

            if (players.Count <= 0)
                throw new ArgumentException("Players List is missing in the config file");
            if (_noOfOvers <= 0)
                throw new ArgumentException("Number of overs can not be less or equal to 0");
            if (_totalRunToWin <= 0)
                throw new ArgumentException("Target runs can not be less or equal to 0");

            List<Player> playing = _player.getPlayersOnStrike(players, wicketLost);
            //Generating random objects
            Random random = new Random();

            //Loop total number of overs given
            for (int over = 0; over < _noOfOvers; over++)
            {
                _commentary.printCommentry(over: over, commentryAfterOver: true, totalOvers: _noOfOvers, totalRunToWin: _totalRunToWin);

                //Loop 6 balls by over
                for (int ball = 1; ball <= balls; ball++)
                {
                    playing[0].TotalBallsPlayed += 1;
                    int runs = _weightedRandomGenerator.generateWeightedList(random, playing[0].Probabilities);

                    //runs being 7 is out
                    if (runs != WICKET)
                    {
                        //Reduce the run scored from the total run to win
                        _totalRunToWin -= runs;

                        //Increasing the score of the player
                        playing[0].Scores += runs;

                        //Updating the score details for individual for detailed summary
                        _player.updateScoreBoardForPlayer(runs, playing[0]);

                        //printing commentry for the ball
                        _commentary.printCommentry(over, ball, runs, playing[0]);

                        //If the no. of runs is odd then change strike
                        if (runs % 2 != 0)
                        {
                            _player.playerschangeStrike(playing);
                        }
                        //More the given target made Remus team to win
                        if (_totalRunToWin <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n\t\tRemus won by {3 - wicketLost} wickets and {(_noOfOvers - 1 - over) * 6 + (6 - ball)} balls remaing");
                            Console.ResetColor();
                            _commentary.printScores(players);
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
                            Console.WriteLine($"\t\tRemus team lost by {_totalRunToWin} runs");
                            Console.ResetColor();
                            _commentary.printScores(players);
                            return;
                        }
                        else
                            //New player come at the same position 
                            playing[0] = players[wicketLost + 1];
                    }
                }
                //Change strike at the end of every over
                _player.playerschangeStrike(playing);
            }

            //If total run to win is less then the target and the over is completed, Remus team lost the match!
            if (_totalRunToWin > 0)
            {
                Console.WriteLine(Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\t\tRemus lost by {_totalRunToWin} runs");
                Console.ResetColor();
                _commentary.printScores(players);
            }
        }
    }
}
