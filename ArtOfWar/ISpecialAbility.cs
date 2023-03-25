using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtOfWar
{
    internal interface ISpecialAbility
    {
        public int SpecialAbilityType { get; set; }

        public int SpecialAbilityStrength { get; set; }

        public int SpecialAbilityRange { get; set; }
    }
}
