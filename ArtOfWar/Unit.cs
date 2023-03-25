using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtOfWar
{
    internal class Unit : IUnit
    {
        public Unit()
        {
            Name = Name;
            MaxHp = MaxHp;
            Hp = MaxHp;
            Deffense = Deffense;
            Attack = Attack;
        }

        public Unit(string name, int maxHp, int deffense, int attack)
        {
            Name = name;
            MaxHp = maxHp;
            Hp = MaxHp;
            Deffense = deffense;
            Attack = attack;
        }

        public string Name { get; set; }
        public int Hp { get; set; }
        public int Deffense { get; set; }
        public int Attack { get; set; }
        public int MaxHp { get; set; }
    }
}
