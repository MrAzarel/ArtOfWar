using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtOfWar
{
    internal static class ArmyManagement
    {
        public static int AddUnit(List<Unit> army, int points, int playerChois)
        {
            switch (playerChois)
            {
                case 0:
                    army.Add(new LightInfantryman());
                    points -= 20;
                    break;
                case 1:
                    army.Add(new LightInfantryman());
                    points -= 20;
                    break;
                default:
                    break;
            }
            return 0;
        }
    }
}
