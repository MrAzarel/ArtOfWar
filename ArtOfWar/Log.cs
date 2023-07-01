using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArtOfWar
{
    internal static class Log
    {
        public static void OutArmy(List<Unit> army)
        {
            foreach (var item in army)
            {
                if (item is SpecialUnit)
                {
                    Console.WriteLine("Name: {0} Hp: {1} Def: {2}  Att: {3} \n Special: \n Strength: {4} Range: {5}", item.Name, item.Hp, item.Deffense, item.Attack, (item as SpecialUnit).SpecialAbilityStrength, (item as SpecialUnit).SpecialAbilityRange);
                }
                else
                    Console.WriteLine("Name: {0} Hp: {1} Def: {2}  Att: {3}", item.Name, item.Hp, item.Deffense, item.Attack);
            }
        }

    }

    interface ICommand
    {
        string Execute(Receiver receiver, List<Unit> playerArmy, List<Unit> enemyArmy);
    }



    // Receiver - Получатель
    class Receiver
    {
        public List<Unit> army1 = new List<Unit>();
        public List<Unit> army2 = new List<Unit>();
        public int turn;
        public string brain;
        public int points;

        public Receiver(List<Unit> playerArmy, List<Unit> enemyArmy)
        {
            Save(playerArmy, enemyArmy);
        }

        public void Save(List<Unit> playerArmy, List<Unit> enemyArmy)
        {
            if (army1 != null)      
                army1.Clear();

            for (int i = 0; i < playerArmy.Count; i++)
            {
                AddUnit(army1, playerArmy[i]);
            }

            if (army2 != null)
                army2.Clear();

            for (int i = 0; i < enemyArmy.Count; i++)
            {
                AddUnit(army2, enemyArmy[i]);
            }
        }

        public void AddUnit(List<Unit> army, Unit unit)
        {
            if (!(unit is SpecialUnit))
            {
                army.Add(new Proxy(new Unit(unit.Name, unit.Hp, unit.Deffense, unit.Attack)));
            }
            else if (unit is SpecialUnit)
            {
                army.Add(new SpecialUnit(unit.Name, unit.Hp, unit.Deffense, unit.Attack, (unit as SpecialUnit).SpecialAbilityType, 
                    (unit as SpecialUnit).SpecialAbilityStrength, (unit as SpecialUnit).SpecialAbilityRange));
            }
        }
    }

    class RoundCancellation : ICommand
    {
        public string Execute(Receiver receiver, List<Unit> playerArmy, List<Unit> enemyArmy)
        {
            playerArmy.Clear();

            for (int i = 0; i < receiver.army1.Count; i++)
            {
                playerArmy.Add(receiver.army1[i]);
            }

            enemyArmy.Clear();

            for (int i = 0; i < receiver.army2.Count; i++)
            {
                enemyArmy.Add(receiver.army2[i]);
            }

            return receiver.brain;
        }
    }

    class RoundRepeat : ICommand
    {
        public string Execute(Receiver receiver, List<Unit> playerArmy, List<Unit> enemyArmy)
        {
            playerArmy.Clear();

            int count = receiver.army1.Count;
            for (int i = 0; i < count; i++)
            {
                playerArmy.Add(receiver.army1[i]);
            }

            enemyArmy.Clear();

            count = receiver.army2.Count;
            for (int i = 0; i < count; i++)
            {
                enemyArmy.Add(receiver.army2[i]);
            }

            int playerUnitHp = playerArmy[0].Hp;
            int enemyUnitHp = enemyArmy[0].Hp;
            if (receiver.turn == 1)
                receiver.brain = Fight.Combat(playerArmy, enemyArmy, receiver.points);
            else
                receiver.brain = Fight.Combat(enemyArmy, playerArmy, receiver.points);

            Fight.SpecialUnitsCast(playerArmy, enemyArmy);

            return receiver.brain;
        }
    }

    // Invoker - инициатор
    class Invoker
    {
        ICommand command;

        public Invoker() { }

        public void SetCommand(ICommand com)
        {
            command = com;
        }

        public string DoExecute(Receiver receiver, List<Unit> playerArmy, List<Unit> enemyArmy)
        {
            return command.Execute(receiver, playerArmy, enemyArmy);
        }
    }
}
