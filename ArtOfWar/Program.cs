using System.IO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text.Json;
using static System.Formats.Asn1.AsnWriter;
using System.Security.Cryptography;
using ArtOfWar; 

class Program
{
    static void Main(string[] args)
    {
        Fight fight = new Fight();
        BestArmy bestArmy = new BestArmy();
        ICreator creator;

        List<Unit> enemyArmy = new List<Unit>();
        List<Unit> playerArmy = new List<Unit>();

        List<Unit> corpesEnemyArmy = new List<Unit>();
        List<Unit> corpesPlayerArmy = new List<Unit>();

        Console.WriteLine("Points: ");
        int armyPoints = Convert.ToInt32(Console.ReadLine());
        int points = armyPoints;
        Console.WriteLine();

        BestArmyTest.CreateArmy(enemyArmy, armyPoints);

        //Log.OutArmy(enemyArmy);

        int turn;
        string result;
        string whatTheUnit;

        string userChois = "";

        while (userChois != "7")
        {
            Console.WriteLine("Press 1 to add Simple Unit \nPress 2 to add Special Unit" +
            " \nPress 3 to see your army \nPress 4 to Start fighting \nPress 5 to play Tournament " +
            "\nPress 6 to add your Army in JSON \nPress 7 to exit program");
            userChois = Console.ReadLine();
            Console.WriteLine();

            if (userChois == "666")
            {
                var bestArmyTest = bestArmy.Testing;
            }

            switch (userChois)
            {
                case "1":
                    Console.WriteLine("\n1.Легкий пехотинец \n2.Тяжёлый пехотинец \n3.Рыцарь \n");
                    whatTheUnit = Console.ReadLine();
                    if (whatTheUnit == "1" && armyPoints >= 20)
                        armyPoints -= 20;
                    else if (whatTheUnit == "2" && armyPoints >= 30)
                        armyPoints -= 30;
                    else if (whatTheUnit == "3" && armyPoints >= 54)
                        armyPoints -= 54;
                    else
                    {
                        Console.WriteLine("You have not enough points! \n Points: {0} \n", armyPoints);
                        break;
                    }
                    creator = new UnitCreator();
                    playerArmy.Add(creator.Create(whatTheUnit));
                    break;

                case "2":
                    Console.WriteLine("\n1.Лучник \n2.Медик");
                    whatTheUnit = Console.ReadLine();
                    if (whatTheUnit == "1" && armyPoints >= 36)
                        armyPoints -= 36;
                    else if (whatTheUnit == "2" && armyPoints >= 30)
                        armyPoints -= 30;
                    else
                    {
                        Console.WriteLine("You have not enough points! \n Points: {0} \n", armyPoints);
                        break;
                    }
                    creator = new SpecialUnitCreator();
                    playerArmy.Add(creator.Create(whatTheUnit));
                    break;

                case "3":
                    Log.OutArmy(playerArmy);
                    Console.WriteLine();
                    break;

                case "4":
                    for (int i = 0; i < playerArmy.Count; i++)
                        corpesPlayerArmy.Add(playerArmy[i]);

                    for (int i = 0; i < enemyArmy.Count; i++)
                        corpesEnemyArmy.Add(enemyArmy[i]);

                    turn = 1;

                    result = fight.GoBattle(playerArmy, enemyArmy, turn, corpesPlayerArmy, corpesEnemyArmy, points);

                    if (result == "WIN")
                        Console.WriteLine("WIN");
                    else if (result == "LOSE")
                        Console.WriteLine("LOSE");
                    else
                        Console.WriteLine("DRAW");

                    Console.WriteLine("Your Army: \n");
                    Log.OutArmy(playerArmy);
                    Console.WriteLine("\n");

                    Console.WriteLine("Enemy Army: \n");
                    Log.OutArmy(enemyArmy);
                    Console.WriteLine();

                    break;

                case "5":
                    int playerPoints = 0;
                    int enemyPoints = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 0 || i == 2)
                            turn = 1;
                        else
                            turn = 2;

                        result = fight.GoBattle(playerArmy, enemyArmy, turn, corpesPlayerArmy, corpesEnemyArmy, points);

                        if (result == "WIN")
                            playerPoints += 3;
                        else if (result == "LOSE")
                            enemyPoints += 3;
                        else
                        {
                            playerPoints += 1;
                            enemyPoints += 1;
                        }
                    }

                    if (playerPoints > enemyPoints)
                        Console.WriteLine("WIN");
                    else if (enemyPoints > playerPoints)
                        Console.WriteLine("LOSE");
                    else
                        Console.WriteLine("DRAW");

                    Console.WriteLine("With {0} points, aganest {1}\n", playerPoints, enemyPoints);

                    break;

                case "6":
                    string json = JsonSerializer.Serialize(playerArmy);
                    StreamWriter sw = new StreamWriter("C:\\JSON\\json.json");
                    sw.WriteLine(json);
                    sw.Close();
                    Console.WriteLine("Your army was saved \n");

                    break;

                default:
                    break;
            }
        }
    }
}