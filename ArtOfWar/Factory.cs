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

    class Brain
    {
        ICreator creator;
        
        public Brain(ICreator newCreator)
        {
            creator = newCreator;
        }

        public Unit Create(string n) 
        {
            return creator.Create(n);
        }
    }

    class UnitCreator : ICreator
    {
        public override Unit Create(string chois)
        {
            switch (chois)
            {
                case "1":
                    Unit lightInfantryman = new Proxy(new LightInfantryman(10, 0, 10));
                    return lightInfantryman;
                case "2":
                    Unit heavyInfantryman = new Proxy(new HeavyInfantryman());
                    return heavyInfantryman;
                case "3":
                    Unit knight = new Proxy(new Knight());
                    return knight;
                case "4":
                    Unit walkSity = new Adapter();
                    return walkSity;
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
                case "3":
                    return new Witcher();
                default:
                    return new Unit();
            }
        }
    }

    abstract class Buff : HeavyInfantryman
    {
        protected HeavyInfantryman buffedHeavyInfantryman;

        public Buff(Unit heavyInfantryman) 
        {
            buffedHeavyInfantryman = heavyInfantryman as HeavyInfantryman;
        }
    }

    class HalfBuff : Buff
    {
        public HalfBuff(Unit lightInfantryman, Unit heavyInfantryman) : base(heavyInfantryman)
        {
            Name += " with two swords";
            Attack += lightInfantryman.Attack;
        }
    }

    class FullBuff : Buff
    {
        public FullBuff(Unit lightInfantryman, Unit heavyInfantryman) : base(heavyInfantryman)
        {
            Name += " buffed";
            Attack += lightInfantryman.Attack;
            Hp += lightInfantryman.Hp;

            lightInfantryman.Hp = 0;
        }
    }

    sealed class LightInfantryman : Unit, ICloneable
    {
        public LightInfantryman(int maxHp, int deffense, int attack)
        {
            Name = "Light Infantryman";
            MaxHp = maxHp;
            Hp = MaxHp;
            Deffense = deffense;
            Attack = attack;
        }

        public ICloneable Clone()
        {
            return new LightInfantryman(this.Hp, this.Deffense, this.Attack);
        }
    }

    class HeavyInfantryman : Unit
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
            Deffense = 10;
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
            Attack = 4;
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

    sealed class Witcher : SpecialUnit
    {
        public Witcher()
        {
            Name = "Witcher";
            MaxHp = 15;
            Hp = MaxHp;
            Deffense = 0;
            Attack = 5;
            SpecialAbilityType = 3;
            SpecialAbilityStrength = 2;
            SpecialAbilityRange = 3;
        }

        public Unit Clone(ICloneable cloneable)
        {
            var rand = new Random();
            int willItColoned = rand.Next(1, 21);

            if (willItColoned == 1)
                return cloneable.Clone() as Unit;

            return null;
        }
    }

    class Adapter : Unit
    {
        WalkCity walkCity = new WalkCity();

        public Adapter()
        {
            Name = walkCity.Name;
            MaxHp = walkCity.MaxHp;
            Hp = MaxHp;
            Deffense = walkCity.Deffense;
            Attack = 0; 
        }
    }

    sealed class WalkCity
    {
        public WalkCity()
        {
            Name = name;
            MaxHp = maxHp;
            Deffense = deffensse;
        }
        string name = "WalkCity";
        int maxHp = 10;
        int deffensse = 40;
        public string Name { get; }       
        public int MaxHp { get; } 
        public int Deffense { get; }
    }
}
