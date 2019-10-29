//Copyright 2019-2020 Carmel Raj, Bangalore 

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CricketSimulation.Interfaces;

namespace CricketSimulation
{
    public class Player : IPlayers
    {
        public string Name { get; set; }
        public int Scores { get; set; }
        public bool IsOut { get; set; }
        public int TotalBallsPlayed { get; set; }
        public int OneRun { get; set; }
        public int TwoRun { get; set; }
        public int ThreeRuns { get; set; }
        public int FourRuns { get; set; }
        public int FiveRuns { get; set; }
        public int SixRuns { get; set; }

        public List<int> Probabilities { get; set; }

        public Player()
        {

        }
        public Player(string name, int score, bool isOut, int totalBallsPlayed, List<int> probabilities)
        {
            this.Name = name;
            Scores = score;
            IsOut = IsOut;
            TotalBallsPlayed = totalBallsPlayed;
            Probabilities = probabilities;
        }

        public List<Player> getPlayersList()
        {
            return ReadPlayersListFromConfig();
        }

        private List<Player> ReadPlayersListFromConfig()
        {
            List<Player> player = new List<Player>();
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key.StartsWith("Player"))
                {
                    var p = ConfigurationManager.AppSettings[key].Split(':');
                    var playerName = p[0];
                    var playersProbability = p[1].Split(',').Select(int.Parse).ToList();
                    player.Add(new Player(playerName, 0, false, 0, playersProbability));
                }
            }
            return player;
        }

        /// <summary>
        /// Assuming first two Players on the ground (1st and 2nd on batting the field)
        /// </summary>
        /// <param name="players">Players List</param>
        /// <param name="wicketLost">Number of wickets lost</param>
        /// <returns></returns>
        public List<Player> getPlayersOnStrike(List<Player> players, int wicketLost)
        {
            List<Player> playing = new List<Player>();
            playing.Add(players[wicketLost]);
            playing.Add(players[wicketLost + 1]);
            return playing;
        }

        /// <summary>
        ///     Changing the strike of the players
        /// </summary>
        /// <param name="playing">List of players(always two) on the ground</param>
        public void playerschangeStrike(List<Player> playing)
        {
            var switchPlayer = playing[0];
            playing[0] = playing[1];
            playing[1] = switchPlayer;
        }


        /// <summary>
        /// Updating the individual score details for the playes
        /// </summary>
        /// <param name="runs">run scored for the ball </param>
        /// <param name="player">Player details</param>
        public void updateScoreBoardForPlayer(int runs, Player player)
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
