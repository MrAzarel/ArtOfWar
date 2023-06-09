using System.IO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text.Json;
using static System.Formats.Asn1.AsnWriter;
using System.Security.Cryptography;
using ArtOfWar;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        Fight fight = new Fight();
        IBestArmy bestArmy = new BestArmy();

        Brain simpleUnitCreator = new Brain(new UnitCreator());
        Brain specialUnitCreator = new Brain(new SpecialUnitCreator());

        List<Unit> enemyArmy = new List<Unit>();
        List<Unit> playerArmy = new List<Unit>();

        List<Unit> corpesEnemyArmy = new List<Unit>();
        List<Unit> corpesPlayerArmy = new List<Unit>();

        Console.WriteLine("Points: ");
        string temp = Console.ReadLine();
        int armyPoints = 0;

        if (temp == "" || temp.Any(c => char.IsLetter(c)))
            while (temp == "" || temp.Any(c => char.IsLetter(c)))
            {
                temp = Console.ReadLine();
                if (temp != "" && !temp.Any(c => char.IsLetter(c)))
                    armyPoints = Convert.ToInt32(temp);
                else
                    Console.WriteLine("Колличество очков не указано или указано некоректно!");
            }
        else
            armyPoints = Convert.ToInt32(temp);


        int points = armyPoints;
        Console.WriteLine();

        BestArmyTest.CreateArmy(enemyArmy, armyPoints);
        //Log.OutArmy(enemyArmy); 

        int turn;
        string result;
        string whatTheUnit;

        string userChois = "";

        bool someoneCanHaveBuff = false;
        bool bufferIsHere = false;

        List<int> buff = new List<int>();
        int bufferPosition = -1;

        while (userChois != "7")
        {
            Console.WriteLine();
            Console.WriteLine("Press 1 to add Simple Unit \nPress 2 to add Special Unit" +
            " \nPress 3 to see your army \nPress 4 to Start fighting \nPress 5 to play Tournament " +
            "\nPress 6 to add your Army in JSON \nPress 7 to exit program");
            userChois = Console.ReadLine();
            Console.WriteLine();

            if (userChois == "666")
            {
                string test = bestArmy.Testing();
                Console.WriteLine(test + "\n");
            }

            switch (userChois)
            {
                case "1":
                    Console.WriteLine("\n1.Легкий пехотинец \n2.Тяжёлый пехотинец \n3.Рыцарь \n4.Гуляй город");
                    whatTheUnit = Console.ReadLine();
                    if (whatTheUnit == "1" && armyPoints >= 20)
                    {
                        armyPoints -= 20;
                        bufferIsHere = true;
                        bufferPosition = playerArmy.Count;
                    }
                    else if (whatTheUnit == "2" && armyPoints >= 30)
                    {
                        armyPoints -= 30;
                        someoneCanHaveBuff = true;
                        buff.Add(playerArmy.Count);
                    }
                    else if (whatTheUnit == "3" && armyPoints >= 55)
                        armyPoints -= 55;
                    else if (whatTheUnit == "4" && armyPoints >= 50)
                        armyPoints -= 50;
                    else
                    {
                        Console.WriteLine("You have not enough points or no such unit does not exist! \n Points: {0} \n", armyPoints);
                        break;
                    }
                    playerArmy.Add(simpleUnitCreator.Create(whatTheUnit));
                    if (bufferIsHere && someoneCanHaveBuff)
                    {
                        string chois;
                        Console.WriteLine("Вы можете бафнуть тежелого пехотинца!");
                        Console.WriteLine("1. Бафнуть всех\n2. Бафнуть последнего");
                        chois = Console.ReadLine();
                        switch (chois) 
                        {
                            case "1": 
                                for (int i = 0; i < buff.Count; i++)
                                    playerArmy[buff[i]] = new HalfBuff(playerArmy[bufferPosition], playerArmy[buff[i]]);
                                break;
                            case "2":
                                    playerArmy[buff[buff.Count - 1]] = new HalfBuff(playerArmy[bufferPosition], playerArmy[buff[buff.Count - 1]]);
                                break;
                            default:
                                break;
                        }
                        someoneCanHaveBuff = false;
                    }                 
                    break;

                case "2":
                    Console.WriteLine("\n1.Лучник \n2.Медик \n3.Колдун");
                    whatTheUnit = Console.ReadLine();
                    if (whatTheUnit == "1" && armyPoints >= 35)
                        armyPoints -= 35;
                    else if (whatTheUnit == "2" && armyPoints >= 30)
                        armyPoints -= 30;
                    else if (whatTheUnit == "3" && armyPoints >= 30)
                        armyPoints -= 30;
                    else
                    {
                        Console.WriteLine("You have not enough points or no such unit does not exist! \n Points: {0} \n", armyPoints);
                        break;
                    }

                    playerArmy.Add(specialUnitCreator.Create(whatTheUnit));
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