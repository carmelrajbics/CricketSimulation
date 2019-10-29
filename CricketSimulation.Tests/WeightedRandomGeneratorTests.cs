//Copyright 2019-2020 Carmel Raj, Bangalore 

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CricketSimulation.Tests
{
    [TestClass]
    public class WeightedRandomGeneratorTests
    {

        WeightedRandomGenerator sut;

        [TestInitialize]
        public void Initialize()
        {
            sut = new WeightedRandomGenerator();
        }

        [TestMethod]
        public void GeneratedRandomNumberIsAlwaysWithIn100()
        {
            //Arrange
            List<Player> players = new List<Player>
            {
               new Player("Pravin",0,false,0,new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 }),

            };
            //Setup
            Mock<Random> r = new Mock<Random>();
            r.Setup(x => x.Next(0, 100)).Returns(5);

            Action act = () =>
            {
                int answer = sut.generateWeightedList(r.Object, players[0].Probabilities);
                Assert.IsInstanceOfType(answer, typeof(int));
                Assert.IsNotNull(answer);
                Assert.IsTrue(answer <= 100);
            };
        }

        [TestMethod]
        public void GenerateRandomNumber_AlwaysSix()
        {
            //Arrange
            List<Player> players = new List<Player>
            {
               new Player("Pravin",0,false,0,new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 }),

            };
            int expectedRandomNumberBasedOnProbability = 6;
            //Setup
            Mock<Random> r = new Mock<Random>();
            r.Setup(x => x.Next(0, 100)).Returns(93);



            int answer = sut.generateWeightedList(r.Object, players[0].Probabilities);

            //Assert
            Assert.AreEqual(answer, expectedRandomNumberBasedOnProbability);
        }

        [TestMethod]
        public void GenerateRandomNumber_AlwaysThree()
        {
            //Arrange
            List<Player> players = new List<Player>
            {
               new Player("Pravin",0,false,0,new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 }),

            };
            int expectedRandomNumberBasedOnProbability = 4;
            //Setup
            Mock<Random> r = new Mock<Random>();
            r.Setup(x => x.Next(0, 100)).Returns(74);



            int answer = sut.generateWeightedList(r.Object, players[0].Probabilities);

            //Assert
            Assert.AreEqual(answer, expectedRandomNumberBasedOnProbability);
        }

        [TestMethod]
        public void GenerateRandomNumber_AlwaysOut()
        {
            //Arrange
            List<Player> players = new List<Player>
            {
               new Player("Pravin",0,false,0,new List<int> { 5, 30, 25, 10, 15, 01, 09, 05 }),

            };
            int expectedRandomNumberBasedOnProbability = 0;
            //Setup
            Mock<Random> r = new Mock<Random>();
            r.Setup(x => x.Next(0, 100)).Returns(3);



            int answer = sut.generateWeightedList(r.Object, players[0].Probabilities);

            //Assert
            Assert.AreEqual(answer, expectedRandomNumberBasedOnProbability);
        }
    }
}
