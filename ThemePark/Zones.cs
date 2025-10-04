using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemePark
{
    internal class Zones
    {
        public static string[] ArcadeGamesList =
        {
            "Space Invaders", "Pac-Man", "Air Hockey", "Pinball",
            "Dance Machine", "Claw Machine", "Street Fighter"
        };

        public static string[] TivoliGameList =
        {
            "Duck Shoot", "Ring Toss", "Can Knockdown",
            "Fishing Game", "Balloon Darts", "Shooting Alley", "Bottle Ring"
        };


        public static Attractions[] North = new Attractions[]
        {
            new Ride("Mountain Ride", 3.5, 160, false, 13),
            new Ride("Water Slide", 2, 120, true, 9),
            new Games("Tivoli Games",120,7)
        };

        public static Attractions[] East = new Attractions[]
        {
            new Ride("Swings", 3, 90, false,9),
            new Ride("Train Ride", 4, 0, false, 3)
        };

        public static Attractions[] West = new Attractions[]
        {
            new Ride("Bababoi", 3, 150, false, 13),
            new Ride("SuperMan", 2, 100, false, 9)
        };

        public static Attractions[] South = new Attractions[]
        {
            new Ride("Pirate Ocean", 7, 80, true, 7),
            new Ride("Mario Road", 5, 80, true, 6)
        };

    }
}
