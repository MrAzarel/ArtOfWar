using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtOfWar
{
    internal class Score
    {
        public static void AddNew(List<Unit> score, List<Unit> army)
        {
            int count = score.Count;
            for (int i = 0; i < count; i++)
                score.RemoveAt(0);

            foreach (var item in army)
                score.Add(item);
        }
    }
}
