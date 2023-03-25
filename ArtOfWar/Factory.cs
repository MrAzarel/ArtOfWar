using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtOfWar
{
    internal abstract class ICreator
    {
        public abstract Unit Create(string chois);
    }

    class UnitCreator : ICreator
    {
        public override Unit Create(string chois) 
        {
            switch (chois)
            {
                case "1":
                    return new LightInfantryman();
                case "2":
                    return new HeavyInfantryman();
                case "3":
                    return new Knight();
                default:
                    return new Unit();
            }           
        }
    }

    class SpecialUnitCreator : ICreator
    {
        public override Unit Create(string chois)
        {
            switch (chois)
            {
                case "1":
                    return new Archer();
                case "2":
                    return new Healer();
                default:
                    return new Unit();
            }
        }
    }

    sealed class LightInfantryman : Unit
    {
        public LightInfantryman()
        {
            Name = "Light Infantryman";
            MaxHp = 10;
            Hp = MaxHp;
            Deffense = 0;
            Attack = 10;
        }
    }

    sealed class HeavyInfantryman : Unit
    {
        public HeavyInfantryman()
        {
            Name = "Heavy Infantryman";
            MaxHp = 15;
            Hp = MaxHp;
            Deffense = 5;
            Attack = 10;
        }
    }

    sealed class Knight : Unit
    {
        public Knight()
        {
            Name = "Knight";
            MaxHp = 30;
            Hp = MaxHp;
            Deffense = 9;
            Attack = 15;
        }
    }

    sealed class Archer : SpecialUnit
    {
        public Archer()
        {
            Name = "Archer";
            MaxHp = 5;
            Hp = MaxHp;
            Deffense = 0;
            Attack = 5;
            SpecialAbilityType = 1;
            SpecialAbilityStrength = 10;
            SpecialAbilityRange = 3;
        }
    }

    sealed class Healer : SpecialUnit
    {
        public Healer()
        {
            Name = "Healer";
            MaxHp = 5;
            Hp = MaxHp;
            Deffense = 0;
            Attack = 5;
            SpecialAbilityType = 2;
            SpecialAbilityStrength = 5;
            SpecialAbilityRange = 5;
        }
    }

}
