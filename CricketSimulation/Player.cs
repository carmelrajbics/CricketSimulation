//Copyright 2019-2020 Carmel Raj, Bangalore 

namespace CricketSimulation
{
    public class Player
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

        public Player(string name, int score, bool isOut, int totalBallsPlayed)
        {
            this.Name = name;
            Scores = score;
            IsOut = IsOut;
            TotalBallsPlayed = totalBallsPlayed;
        }
    }
}
