using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtOfWar
{
    internal interface IUnit
    {
        public string Name { get; set; }

        public int Hp { get; set; }

        public int MaxHp { get; set; }

        public int Deffense { get; set; }

        public int Attack { get; set; }
    }
}
