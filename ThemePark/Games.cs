using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemePark
{
    internal class Games : Attractions
    {



        public Games(string name, double duration, int ageRestriction)
            :base ( name,  duration,  ageRestriction)
        {
            
        }

        public class TivoliGames : Games
        {
            
            public int Cost { get; set; }

            public TivoliGames(int cost, string name, double duration, int ageRestriction)
                : base(name, duration, ageRestriction)
            {
                Cost = cost;
            }

        }
        public class ArcadeGames : Games
        {
            public int Cost { get; set; }

            public ArcadeGames(int cost, string name, double duration, int ageRestriction)
                 : base(name, duration, ageRestriction)
            {
                Cost = cost;
            }

        }
    }

}
