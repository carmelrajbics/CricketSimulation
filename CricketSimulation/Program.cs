//Copyright 2019-2020 Carmel Raj, Bangalore 

using System;
using System.Runtime.InteropServices;

namespace CricketSimulation
{
    class Program
    {


        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;
        static void Main(string[] args)
        {
            //Maximizing the console window 
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
            Console.Title = "Cricket Simulation - By Carmel Raj M (PUID-wk29)";

            do
            {
                //Number of runs and number of over are passed to the constructor to make the match feasible
                // any  number of overs with any target run to win the match
                CricketMatch match = new CricketMatch(4, 40);
                match.BeginTheMatch();
                Console.WriteLine("\n\t\tPress 'Enter' to simulate the match again or 'Esc' to exit...");
                Console.WriteLine("----------------------------------------------------------------------------------------");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
            Console.ResetColor();
        }
    }
}
