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
    public class CricketMatchTests
    {
        private CricketMatch _cricket;
        private int _totalOver = 4;
        private int _totalRunsToWin = 40;
        Mock<ICommentary> _commentaryMock;
        Mock<IPlayers> _playersMock;
        Mock<IWeightedRandomGenerator> _weightedRandomGeneratorMock;

        [TestInitialize]
        public void Initialize()
        {
            _commentaryMock = new Mock<ICommentary>();
            _playersMock = new Mock<IPlayers>();
            _weightedRandomGeneratorMock = new Mock<IWeightedRandomGenerator>();

            _cricket = new CricketMatch(_totalRunsToWin, _totalOver, _playersMock.Object, _commentaryMock.Object, _weightedRandomGeneratorMock.Object);
        }

        [ExpectedException(typeof(ArgumentException), "Players List is missing in the config file")]
        [TestMethod]
        public void ValidatePlayers_WithZeroCount_ThrowsException()
        {
            //Arragne
            _playersMock.Setup(x => x.getPlayersList()).Returns(new List<Player>());
            _cricket.BeginTheMatch();

        }

        [ExpectedException(typeof(ArgumentException), "Number of overs can not be less or equal to 0")]
        [TestMethod]
        public void ValidateNoOfOver_WithZeroCount_ThrowsException()
        {
            //Arrange
            var playerList = new List<Player> { new Player("Carmel", 0, false, 0, new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 }) };
            var cricketWithoutOver = new CricketMatch(40, 0, _playersMock.Object, _commentaryMock.Object, _weightedRandomGeneratorMock.Object);
            //Act
            _playersMock.Setup(x => x.getPlayersList()).Returns(playerList);
            cricketWithoutOver.BeginTheMatch();
        }

        [ExpectedException(typeof(ArgumentException), "Target runs can not be less or equal to 0")]
        [TestMethod]
        public void Validate_WithZeroTargetRun_ThrowsException()
        {
            //Arrange
            var playerList = new List<Player> { new Player("Carmel", 0, false, 0, new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 }) };
            var cricketWithoutOver = new CricketMatch(0, 40, _playersMock.Object, _commentaryMock.Object, _weightedRandomGeneratorMock.Object);
            //Act
            _playersMock.Setup(x => x.getPlayersList()).Returns(playerList);
            cricketWithoutOver.BeginTheMatch();
        }

        [TestMethod]
        public void RemusTeam_LoosesTheMatchByAllOutWithZeroRuns()
        {
            //Arrage
            var playerList = new List<Player> { new Player("Carmel", 0, false, 0, new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 }),
            new Player("Irfan", 0, false, 0, new List<int> { 10,40,20,05,10,01,04,105 })};
            Random rand = new Random();
            int wicket = 7;

            //Act
            _playersMock.Setup(x => x.getPlayersList()).Returns(playerList);
            _playersMock.Setup(x => x.getPlayersOnStrike(playerList, 0)).Returns(playerList.Take(2).ToList());
            _weightedRandomGeneratorMock.Setup(x => x.generateWeightedList(rand, playerList[0].Probabilities)).Returns(wicket);
            _commentaryMock.Setup(x => x.printCommentry(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), null, false, 0, 0)).Verifiable();
            _commentaryMock.Setup(x => x.printScores(playerList)).Verifiable();
            _cricket.BeginTheMatch();

            //Assert
            _playersMock.Verify(v => v.getPlayersList(), Times.AtLeastOnce);
            _playersMock.Verify(v => v.getPlayersOnStrike(It.IsAny<List<Player>>(), It.IsAny<int>()), Times.AtLeastOnce);
            _playersMock.Verify(v => v.getPlayersOnStrike(It.IsAny<List<Player>>(), It.IsAny<int>()), Times.AtLeastOnce);
            _weightedRandomGeneratorMock.Verify(v => v.generateWeightedList(It.IsAny<Random>(), It.IsAny<List<int>>()), Times.AtLeastOnce);
            _commentaryMock.Verify(v => v.printCommentry(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), null, false, 0, 0), Times.Never);
            _commentaryMock.Verify(v => v.printScores(It.IsAny<List<Player>>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void RemusTeam_WinsTheMatchsByScoringAllBallsSix()
        {
            //Arrage
            var playerList = new List<Player> { new Player("Carmel", 0, false, 0, new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 }),
            new Player("Irfan", 0, false, 0, new List<int> { 10,40,20,05,10,01,04,105 })};
            Random rand = new Random();
            int six = 6;

            //Act
            _playersMock.Setup(x => x.getPlayersList()).Returns(playerList);
            _playersMock.Setup(x => x.getPlayersOnStrike(playerList, 0)).Returns(playerList.Take(2).ToList());
            _weightedRandomGeneratorMock.Setup(x => x.generateWeightedList(rand, playerList[0].Probabilities)).Returns(six);
            _commentaryMock.Setup(x => x.printCommentry(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), null, false, 0, 0)).Verifiable();
            _commentaryMock.Setup(x => x.printScores(playerList)).Verifiable();
            _cricket.BeginTheMatch();

            //Assert
            _playersMock.Verify(v => v.getPlayersList(), Times.AtLeastOnce);
            _playersMock.Verify(v => v.getPlayersOnStrike(It.IsAny<List<Player>>(), It.IsAny<int>()), Times.AtLeastOnce);
            _playersMock.Verify(v => v.getPlayersOnStrike(It.IsAny<List<Player>>(), It.IsAny<int>()), Times.AtLeastOnce);
            _weightedRandomGeneratorMock.Verify(v => v.generateWeightedList(It.IsAny<Random>(), It.IsAny<List<int>>()), Times.AtLeastOnce);
            _commentaryMock.Verify(v => v.printScores(It.IsAny<List<Player>>()), Times.AtLeastOnce);
        }
    }
}
