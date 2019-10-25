using System;
using System.Collections.Generic;
using CricketSimulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestCricketSimulation
{
    [TestClass]
    public class CricketMathUnitTest
    {
        private CricketMatch _cricket;
        private int _totalOver = 4;
        private int _totalRunsToWin = 40;

        [TestInitialize]
        public void Initialize()
        {
            _cricket = new CricketMatch(_totalOver, _totalRunsToWin);
        }

        [TestMethod]
        public void GeneratedRandomNumberIsAlwaysWithIn100()
        {
            List<Player> players = new List<Player>
            {
               new Player("Pravin",0,false,0),

            };
            //List of players with their respective probabilities to to score the runs
            Dictionary<string, List<int>> playersWithprobabilities = new Dictionary<string, List<int>>();
            playersWithprobabilities[players[0].Name] = (new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 });

            Random r = new Random();

            int answer = _cricket.generateWeightedList(r, playersWithprobabilities[players[0].Name]);

            Assert.IsInstanceOfType(answer, typeof(int));
            Assert.IsNotNull(answer);
            Assert.IsTrue(answer <= 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PrintTableRowMisMatches()
        {
            string[] tableRowHeader = { "Name", "Score" };
            string[] tableRowValue = { "Pravin" };

            TablePrinter printTable = new TablePrinter(tableRowHeader);
            printTable.AddRow(tableRowValue);
        }

        [TestMethod]
        public void ChangePlayerStrike()
        {
            List<Player> playersOnTheGround = new List<Player>
            {
               new Player("Pravin",0,false,0),
               new Player("Irfan",0,false,0)
            };
            _cricket.changeStrike(playersOnTheGround);

            Assert.AreEqual(playersOnTheGround[0].Name, "Irfan");
            Assert.AreEqual(playersOnTheGround[1].Name, "Pravin");
        }

        [TestMethod]
        public void PlayMatch()
        {
            _cricket.BeginTheMatch();
            Assert.IsNotNull(_cricket.totalRunToWin);
            Assert.IsNotNull(_cricket.totalOvers);
            Assert.AreEqual(_cricket.totalOvers, _totalOver);
            Assert.IsTrue(_cricket.totalRunToWin <= _totalRunsToWin);
        }
    }
}
