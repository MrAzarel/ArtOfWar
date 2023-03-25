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
}
