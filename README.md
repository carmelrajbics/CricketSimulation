# CricketSimulation
A program to simulate cricket match of the last four over  with given target  with batting probability of given value

# How to run the application
    Open the solution in Visual studio and follow the stepsto run the application.
        1) Restore the Nuget by choosing Restore NuGet packages
        2) On successful restoration of NuGet, Run F5 to the the application 
 3) The following NuGet packages are used in this program
# NuGet packages used #
        1.Unity version 5.11.1 
        2.Moq version 4.13.1
    
## Application Type
    This is console application developed in C#
 
# Over view of the solution
    1. There are 3 classes [Player.cs,CricketMatch.cs,TablePrinter.cs]  excluding Program.cs
    2. Player class is just a model having properties of the player
    3. TablePriter class is used to print the result in the tabular format
    4. CricketMatch class is the main class which does most of the logics
    5. Player's list and their probabilities are added in the App.Config file for better flexibilities
    
 # sample screenshot 1   
![sample output](https://raw.githubusercontent.com/carmelrajbics/CricketSimulation/master/resources/1.JPG)

 # sample screenshot2  
 ![sample output](https://raw.githubusercontent.com/carmelrajbics/CricketSimulation/master/resources/2.JPG)
