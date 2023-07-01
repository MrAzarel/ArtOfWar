using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtOfWar
{
    internal sealed class Fight
    {
        //private static Fight instance;
        //private static object syncRoot = new Object();

        //private Fight() { }

        //public static Fight GetInstance
        //{
        //    get
        //    {
        //        lock (syncRoot)
        //            if (instance == null)
        //                instance = new Fight();
        //        return instance;
        //    }
        //}

        static int Turn(int turn)
        {
            if (turn == 1)
                return 2;
            else
                return 1;
        }

        //static int Hit(Unit firstFighter, Unit secondFighter, int points)
        //{
        //    double n = ((points - secondFighter.Deffense) * (double)firstFighter.Attack / 100);
        //    n = Math.Ceiling(n);
        //    return (int)n;
        //}

        public static string Combat(List<Unit> firstArmy, List<Unit> secondArmy, int points)
        {
            int hit = secondArmy[0].TakeDamage(firstArmy[0], points);

            if (hit <= 0)
                return "drawPoints++";

            if (secondArmy[0].Hp <= 0)
                return "turn = 1";
            else
                return "turn = 2";
        }

        public string GoBattle(List<Unit> playerArmy, List<Unit> enemyArmy, int turn, List<Unit> corpesPlayerArmy, List<Unit> corpesEnemyArmy, int points)
        {
            int turnSave = turn;

            string brain = "";
            int drawPoints = 0;

            Invoker invoker = new Invoker();
            Receiver receiver = new Receiver(playerArmy, enemyArmy);
            receiver.points = points;

            string battleOption;
            Console.WriteLine("Играть без остановок?\n1. Да\n2. Нет");
            battleOption = Console.ReadLine();
            if (battleOption == "1")
                battleOption = "4";

            while (playerArmy.Count != 0 && enemyArmy.Count != 0 && drawPoints < 2)
            {
                if (battleOption != "5")
                {
                    receiver.Save(playerArmy, enemyArmy);
                    int playerUnitHp = playerArmy[0].Hp;
                    int enemyUnitHp = enemyArmy[0].Hp;
                    if (turn == 1)
                        brain = Combat(playerArmy, enemyArmy, points);
                    else
                        brain = Combat(enemyArmy, playerArmy, points);

                    SpecialUnitsCast(playerArmy, enemyArmy);

                    turnSave = turn;
                    if (brain == "turn = 2")
                    {
                        if (playerArmy[0].Hp < playerUnitHp || enemyArmy[0].Hp < enemyUnitHp)
                            drawPoints = 0;
                        else
                            drawPoints++;
                        turn = Turn(turn);
                    }
                    else if (brain == "turn = 1")
                        drawPoints = 0;
                    else
                    {
                        turn = Turn(turn);
                        drawPoints++;
                    }

                    ArmysUpdate(playerArmy, enemyArmy);
                    Console.WriteLine("Your Army: ");
                    Log.OutArmy(playerArmy); Console.WriteLine();
                    Console.WriteLine("Enemy Army: ");
                    Log.OutArmy(enemyArmy); Console.WriteLine();
                }

                if (battleOption != "4")
                {
                    Console.WriteLine("1. Отменить раунд \n2. Повторить раунд");
                    battleOption = Console.ReadLine();
                    if (battleOption == "1")
                    {
                        turn = turnSave;
                        receiver.brain = brain;
                        invoker.SetCommand(new RoundCancellation());
                    }
                    else if (battleOption == "2")
                    {
                        receiver.turn = turnSave;
                        receiver.brain = brain;
                        invoker.SetCommand(new RoundRepeat());
                    }

                    if (battleOption == "1" || battleOption == "2")
                    {
                        brain = invoker.DoExecute(receiver, playerArmy, enemyArmy);
                        Console.WriteLine("Your Army: ");
                        Log.OutArmy(playerArmy); Console.WriteLine();
                        Console.WriteLine("Enemy Army: ");
                        Log.OutArmy(enemyArmy); Console.WriteLine();
                        battleOption = "5";
                    }
                }
            }
            return EndBattle(playerArmy, corpesPlayerArmy, enemyArmy, corpesEnemyArmy);
        }

        static void ArmysUpdate(List<Unit> firstArmy, List<Unit> secondArmy)
        {
            for (int i = 0; i < firstArmy.Count; i++)
                if (firstArmy[i].Hp <= 0)
                    firstArmy.RemoveAt(0);

            for (int i = 0; i < secondArmy.Count; i++)
                if (secondArmy[i].Hp <= 0)
                    secondArmy.RemoveAt(0);
        }

        public static void SpecialUnitsCast(List<Unit> firstArmy, List<Unit> secondArmy)
        {
            int count;

            if (firstArmy.Count >= secondArmy.Count)
                count = firstArmy.Count;
            else
                count = secondArmy.Count;

            for (int i = 1; i < count; i++)
            {
                if (firstArmy.Count > i)
                    if (firstArmy[i] is SpecialUnit && (firstArmy[i] as SpecialUnit).SpecialAbilityType == 1)
                        Shoot((firstArmy[i] as SpecialUnit), secondArmy, i);
                    else if (firstArmy[i] is SpecialUnit && (firstArmy[i] as SpecialUnit).SpecialAbilityType == 2)
                        Heal((firstArmy[i] as SpecialUnit), firstArmy, i);
                    else if (firstArmy[i] is SpecialUnit && (firstArmy[i] as SpecialUnit).SpecialAbilityType == 3)
                        Cloning((firstArmy[i] as SpecialUnit), firstArmy, i);

                if (secondArmy.Count > i)
                    if (secondArmy[i] is SpecialUnit && (secondArmy[i] as SpecialUnit).SpecialAbilityType == 1)
                        Shoot((secondArmy[i] as SpecialUnit), firstArmy, i);
                    else if (secondArmy[i] is SpecialUnit && (secondArmy[i] as SpecialUnit).SpecialAbilityType == 2)
                        Heal((secondArmy[i] as SpecialUnit), secondArmy, i);
                    else if (secondArmy[i] is SpecialUnit && (secondArmy[i] as SpecialUnit).SpecialAbilityType == 3)
                        Cloning((secondArmy[i] as SpecialUnit), secondArmy, i);
            }
        }

        static void Shoot(SpecialUnit archer, List<Unit> secondArmy, int archerPosition)
        {
            int target = archer.SpecialAbilityRange - archerPosition;
            if (target < archer.SpecialAbilityRange)
            {
                if (secondArmy.Count < target)
                    target = secondArmy.Count;

                for (int i = 0; i < target; i++)
                    if (secondArmy[i].Hp > 0)
                    {
                        secondArmy[i].Hp -= archer.SpecialAbilityStrength;
                        break;
                    }
            }
        }

        static void Heal(SpecialUnit healer, List<Unit> firstAmy, int healerPosition)
        {
            if (healer.SpecialAbilityRange > healerPosition)
                firstAmy[0].Hp += healer.SpecialAbilityStrength;
            if (firstAmy[0].Hp > firstAmy[0].MaxHp)
                firstAmy[0].Hp = firstAmy[0].MaxHp;
        }

        static void Cloning(SpecialUnit witcher, List<Unit> firstAmy, int witcherPosition)
        {
            for (int i = 0; i < witcherPosition; i++)
            {
                if (firstAmy[i] is ICloneable)
                {
                    Unit clone = (witcher as Witcher).Clone(firstAmy[i + 1] as ICloneable);
                    if (clone != null)
                    {
                        Unit swoper = firstAmy[i];
                        firstAmy.Insert(i, clone);
                        break;
                    }
                }
            }
        }

        static void RefrashArmy(List<Unit> army, List<Unit> armyCorpes)
        {
            int count = army.Count;

            for (int i = 0; i < count; i++)
                army.RemoveAt(0);

            for (int i = 0; i < armyCorpes.Count; i++)
            {
                armyCorpes[i].Hp = armyCorpes[i].MaxHp;
                army.Add(armyCorpes[i]);
            }

            count = armyCorpes.Count;

            for (int i = 0; i < count; i++)
            {
                armyCorpes.RemoveAt(0);
            }
        }

        static string EndBattle(List<Unit> playerArmy, List<Unit> corpesPlayerArmy, List<Unit> enemyArmy, List<Unit> corpesEnemyArmy)
        {
            string result;

            if (enemyArmy.Count == 0)
                result = "WIN";
            else if (playerArmy.Count == 0)
                result = "LOSE";
            else
                result = "DRAW";

            RefrashArmy(playerArmy, corpesPlayerArmy);
            RefrashArmy(enemyArmy, corpesEnemyArmy);

            return result;
        }
    }
}
