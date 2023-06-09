using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtOfWar
{
    internal class Proxy : Unit
    {
        public Unit unit;

        public Proxy(Unit NewUnit)
        {
            if (unit == null)
                unit = NewUnit;
            Name = unit.Name;
            MaxHp = unit.MaxHp;
            Hp = unit.Hp;
            Deffense = unit.Deffense;
            Attack = unit.Attack;
        }
        public Proxy(string name, int maxHp, int deffense, int attack)
        {
            if (unit == null)
                unit = new Unit(name, maxHp, deffense, attack);
            Name = unit.Name;
            MaxHp = unit.MaxHp;
            Hp = unit.Hp;
            Deffense = unit.Deffense;
            Attack = unit.Attack;
        }

        public override int TakeDamage(Unit gUnit, int points)
        {
            double n = ((points - Deffense) * (double)gUnit.Attack / 100);
            n = Math.Ceiling(n);
            Hp -= (int)n;

            Console.WriteLine("{0} attack {1}", gUnit.Name, Name);
            if (n <= 0)
                Console.WriteLine("The attack failed!");
            else
                Console.WriteLine("Damage: {0} \nHP left: {1}\n", n, Hp);

            return (int)n;
        }
    }
}
