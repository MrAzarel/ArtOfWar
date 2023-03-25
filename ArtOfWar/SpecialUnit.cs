using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArtOfWar
{
    internal class SpecialUnit : Unit, ISpecialAbility
    {
        public SpecialUnit()
        {
            Name = Name;
            MaxHp = MaxHp;
            Hp = MaxHp;
            Deffense = Deffense;
            Attack = Attack;
            SpecialAbilityType = SpecialAbilityType;
            SpecialAbilityStrength = SpecialAbilityStrength;
            SpecialAbilityRange = SpecialAbilityRange;
        }

        public SpecialUnit(string name, int maxHp, int deffense, int attack, int specialAbilityType, int specialAbilityStrength, int specialAbilityRange)
        {
            Name = name;
            MaxHp = maxHp;
            Hp = MaxHp;
            Deffense = deffense;
            Attack = attack;
            SpecialAbilityType = specialAbilityType;
            SpecialAbilityStrength = specialAbilityStrength;
            SpecialAbilityRange = specialAbilityRange;
        }

        public int SpecialAbilityType { get; set; }

        public int SpecialAbilityStrength { get; set; }

        public int SpecialAbilityRange { get; set; }
    }
}
