//Copyright 2019-2020 Carmel Raj, Bangalore 

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CricketSimulation.Tests
{
    [TestClass]
    public class TablePrinterTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PrintTableRowMisMatches()
        {
            string[] tableRowHeader = { "Name", "Score" };
            string[] tableRowValue = { "Pravin" };

            TablePrinter printTable = new TablePrinter(tableRowHeader);
            printTable.AddRow(tableRowValue);
        }
    }
}
