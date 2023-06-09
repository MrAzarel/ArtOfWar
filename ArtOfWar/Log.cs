using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        void Undo();
        void Redo();
    }

    // Receiver - Получатель
    class Receiver
    {
        public void UndoRound()
        {
            
        }

        public void RedoRound()
        {
            
        }
    }

    class RoundCommand : ICommand
    {
        Receiver r;
        public RoundCommand(Receiver set)
        {
            r = set;
        }
        public void Redo()
        {
            r.RedoRound();
        }
        public void Undo()
        {
            r.UndoRound();           
        }
    }

    // Invoker - инициатор
    class Invoker
    {
        List<Unit> savedPlayer;
        List<Unit> savedEnemy;

        ICommand command;

        public Invoker() { }

        public void SetCommand(ICommand com)
        {
            command = com;
        }

        public void PressUndo()
        {
            command.Undo();
        }

        public void PressRedo()
        {
            command.Redo();
        }
    }
}
