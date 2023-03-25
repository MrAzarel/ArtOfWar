using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtOfWar
{
    internal class BestArmy
    {
        private BestArmyTest bestArmyTest = null;
        private readonly object _lock = new object();

        public BestArmyTest Testing { get { lock (_lock) { return bestArmyTest ??= new BestArmyTest(); } } }
    }
}
