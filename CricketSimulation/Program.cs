//Copyright 2019-2020 Carmel Raj, Bangalore 

using System;
using System.Runtime.InteropServices;
using CricketSimulation.BootStrapper;
using Unity;

namespace CricketSimulation
{
    class Program
    {

        #region Import Section
        //Import external system dlls for maximizing the console window
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;
        #endregion

        static void Main(string[] args)
        {
            //Maximizing the console window 
            SetConsoleWindowToMax();

            UnityContainer container = DependencyInjection.SetUp();
            try
            {
                do
                {
                    CricketMatch match = container.Resolve<CricketMatch>();
                    match.BeginTheMatch();
                    Console.WriteLine("\n\t\tPress 'Enter' to simulate the match again or 'Esc' to exit...");
                    Console.WriteLine("----------------------------------------------------------------------------------------");
                } while (Console.ReadKey().Key != ConsoleKey.Escape);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void SetConsoleWindowToMax()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
            Console.Title = "Cricket Simulation - By Carmel Raj M (PUID-wk29)";
        }
    }
}
