using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtOfWar
{
    internal class BestArmy : IBestArmy
    {
        private BestArmyTest bestArmyTest = null;
        private readonly object _lock = new object();

        public void Lazy() { lock (_lock) { bestArmyTest = bestArmyTest ??= new BestArmyTest(); } } 

        public string Testing()
        {
            Lazy();
            return bestArmyTest.Testing();
        }
    }
}
