using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtOfWar
{
    internal class BestArmyTest : IBestArmy
    {

        List<Unit> firstArmy = new List<Unit>();
        List<Unit> secondArmy = new List<Unit>();

        List<Unit> corpesFirstArmy = new List<Unit>();
        List<Unit> corpesSecondArmy = new List<Unit>();

        public static void CreateArmy(List<Unit> army, int fUnitPoints)
        {
            int count = army.Count;
            for (int i = 0; i < count; i++)
                army.RemoveAt(0);

            var rand = new Random();

            int unitType = rand.Next(1, 4);

            int fighterCount = rand.Next(1, 11);

            string Name;

            int Hp, Deffense, Attack;

            for (int i = 0; i < fighterCount; i++)
            {
                Thread.Sleep(10);
                if (fUnitPoints <= 0)
                    break;

                Name = i.ToString();

                Hp = rand.Next(1, fUnitPoints + 1);
                fUnitPoints -= Hp;

                Deffense = rand.Next(0, fUnitPoints + 1);
                fUnitPoints -= Deffense;

                if (i == fighterCount - 1)
                    Attack = fUnitPoints;
                else
                {
                    Attack = rand.Next(0, fUnitPoints + 1);
                    fUnitPoints -= Attack;
                }

                if (unitType != 1 && fUnitPoints > 0)
                {

                    int sas = rand.Next(0, fUnitPoints / 2);
                    fUnitPoints -= sas * 2;

                    int sar = rand.Next(0, fUnitPoints / 2);
                    fUnitPoints -= sar * 2;

                    if (i == 2)
                        army.Add(new SpecialUnit(Name, Hp, Deffense, Attack, 1, sas, sar));
                    else
                        army.Add(new SpecialUnit(Name, Hp, Deffense, Attack, 2, sas, sar));
                }
                else
                {
                    Unit unit = new Proxy(new Unit(Name, Hp, Deffense, Attack));
                    army.Add(unit);
                }
            }
        }

        public string Testing()
        {
            Fight fight = new Fight();

            var rand = new Random();

            int points = 100;

            int iterationCount = 4;

            List<Unit> bestScore = new List<Unit>();
            List<Unit>[] challenger = new List<Unit>[iterationCount];

            int bestWinStreak;
            int fightsCount;

            int fightRounds = 0;

            int fFightNumer = 0;
            int sFightNumer = 0;

            int winPoints;

            int turn;

            int whoWillDelited;

            for (int i = 0; i < iterationCount; i++)
            {
                CreateArmy(firstArmy, 100);
                CreateArmy(secondArmy, 100);

                for (int k = 0; k < firstArmy.Count; k++)
                    corpesFirstArmy.Add(firstArmy[k]);
                for (int k = 0; k < secondArmy.Count; k++)
                    corpesSecondArmy.Add(secondArmy[k]);

                Score.AddNew(bestScore, secondArmy);

                bestWinStreak = 0;
                fightsCount = 0;
                fightRounds = 0;

                while (fightsCount < 5)
                {
                    winPoints = 0;
                    while (fightRounds < 4)
                    {
                        string result;
                        if (fightRounds == 0 || fightRounds == 2)
                            turn = 1;
                        else
                            turn = 2;

                        result = fight.GoBattle(firstArmy, secondArmy, turn, corpesFirstArmy, corpesSecondArmy, points);

                        if (result == "WIN")
                            winPoints++;
                        else if (result == "LOSE")
                            winPoints--;

                        fightRounds++;
                    }

                    if (bestWinStreak < fFightNumer)
                    {
                        bestWinStreak = fFightNumer;
                        Score.AddNew(bestScore, firstArmy);
                    }
                    if (bestWinStreak < sFightNumer)
                    {
                        bestWinStreak = sFightNumer;
                        Score.AddNew(bestScore, secondArmy);
                    }


                    if (winPoints > 0)
                    {
                        fFightNumer++;

                        sFightNumer = 0;
                        CreateArmy(secondArmy, 100);
                    }
                    else if (winPoints < 0)
                    {
                        sFightNumer++;

                        fFightNumer = 0;
                        CreateArmy(firstArmy, 100);
                    }
                    else
                    {
                        whoWillDelited = rand.Next(1, 3);

                        if (whoWillDelited == 1)
                        {
                            sFightNumer = 0;
                            CreateArmy(secondArmy, 100);
                        }
                        else
                        {
                            fFightNumer = 0;
                            CreateArmy(firstArmy, 100);
                        }
                    }

                    fightRounds = 0;
                    fightsCount++;
                }
                Console.WriteLine("\n =====Best Figther===== ");
                Log.OutArmy(bestScore);

                Console.WriteLine("Best Win Streak = {0}", bestWinStreak);

                challenger[i] = bestScore;
                bestScore = new List<Unit>();
            }

            int[] point = new int[10];

            for (int i = 0; i < challenger.Length; i++)
            {
                for (int j = i; j < challenger.Length; j++)
                {
                    winPoints = 0;
                    while (fightRounds < 4)
                    {
                        string result;
                        if (fightRounds == 0 || fightRounds == 2)
                            turn = 1;
                        else
                            turn = 2;

                        result = fight.GoBattle(challenger[i], challenger[j], turn, corpesFirstArmy, corpesSecondArmy, points);

                        if (result == "WIN")
                            winPoints++;
                        else if (result == "LOSE")
                            winPoints--;

                        fightRounds++;
                    }

                    if (winPoints > 0)
                    {
                        point[i] += 3;
                    }
                    else if (winPoints < 0)
                    {
                        point[j] += 3;
                    }
                    else
                    {
                        point[i] += 1;
                        point[j] += 1;
                    }
                }
            }

            Console.WriteLine("=======================");

            for (int i = 0; i < challenger.Length; i++)
            {
                Console.WriteLine("Army {0}", i);
                Console.WriteLine("Win points: {0}", point[i]);
                Log.OutArmy(challenger[i]);
                Console.WriteLine();
            }

            return "Testing complited successfully";
        }
    }
}
