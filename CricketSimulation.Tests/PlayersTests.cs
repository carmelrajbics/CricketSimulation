//Copyright 2019-2020 Carmel Raj, Bangalore 

using System;
using System.Collections.Generic;
using System.Linq;
using CricketSimulation.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CricketSimulation.Tests
{
    [TestClass]
    public class PlayersTests
    {
        private CricketMatch _cricket;
        Mock<IPlayers> _playersMock;
        private int _totalOver = 4;
        private int _totalRunsToWin = 40;
        Mock<ICommentary> _commentaryMock;
        Mock<IWeightedRandomGenerator> _weightedRandomGeneratorMock;

        [TestInitialize]
        public void Initialize()
        {
            _commentaryMock = new Mock<ICommentary>();
            _playersMock = new Mock<IPlayers>();
            _weightedRandomGeneratorMock = new Mock<IWeightedRandomGenerator>();

            _cricket = new CricketMatch(_totalRunsToWin, _totalOver, _playersMock.Object, _commentaryMock.Object, _weightedRandomGeneratorMock.Object);
        }

        [TestMethod]
        public void GetAllPlayersOut()
        {

            //Arrange
            Mock<Player> player = new Mock<Player>();
            player.SetupAllProperties();
            player.Object.Name = "Pravin";

            List<Player> players = new List<Player>
            {
               new Player("Pravin",0,false,0,new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 })

            };
            Dictionary<string, List<int>> playersWithprobabilities = new Dictionary<string, List<int>>();
            playersWithprobabilities[players[0].Name] = (new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 });

            //Act
            Mock<Random> r = new Mock<Random>();
            r.SetupSequence(x => x.Next(0, 100))
                .Returns(5)
                .Returns(15)
                .Returns(45)
                .Returns(80)
                .Returns(97);

            Action act = () =>
            {
                int answer = _weightedRandomGeneratorMock.Object.generateWeightedList(r.Object, playersWithprobabilities[players[0].Name]);
                //Assert
                Assert.AreEqual(answer, 1);
                Assert.IsInstanceOfType(answer, typeof(int));
                Assert.IsNotNull(answer);
                Assert.IsTrue(answer <= 100);
            };
        }

        [TestMethod]
        public void ChangePlayerStrike()
        {
            List<Player> playersOnTheGround = new List<Player>
            {
               new Player("Pravin",0,false,0,new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 }),
               new Player("Irfan",0,false,0,new List<int> { 10,40,20,05,10,01,04,10 })
            };
            Player player = new Player();
            player.playerschangeStrike(playersOnTheGround);

            Assert.AreEqual(playersOnTheGround[0].Name, "Irfan");
            Assert.AreEqual(playersOnTheGround[1].Name, "Pravin");
        }

        [TestMethod]
        public void GetAllPlayers_List()
        {
            //Arrange
            var expectedPlayerList = new List<Player> {
                new Player("Pravin", 0, false, 0, new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 }),
                new Player("Irfan", 0, false, 0, new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 }),
                new Player("Jalindar", 0, false, 0, new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 }),
                new Player("Vaishali", 0, false, 0, new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 }),
            };
            //Act
            _playersMock.Setup(x => x.getPlayersList()).Returns(expectedPlayerList);
            _playersMock.Setup(x => x.getPlayersOnStrike(expectedPlayerList, 0)).Returns(expectedPlayerList.Take(2).ToList());

            _cricket.BeginTheMatch();

            //Assert
            _playersMock.Verify(v => v.getPlayersList(), Times.AtLeastOnce);
            _playersMock.Verify(v => v.getPlayersOnStrike(expectedPlayerList, 0), Times.AtLeastOnce);

        }
    }
}
