using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemePark
{
    internal class GamesHub : Attractions
    {

        internal enum GameType { Arcade, Tivoli }

        public GameType Type { get; }
        

        //what is const? 
        public const int CouponCostPerPlay = 2;
        public const int PriceSekPerBundle = 50;
        public const int CouponsPerBundle = 20;


        public GamesHub(string hubname, int duration, int ageRestriction, GameType type)
            : base(hubname, duration, ageRestriction)
        {
            Type = type;
        }

        public string[] GetGameMenu()
        {
            if (Type == GameType.Arcade) return ArcadeGamesList;
            else return TivoliGameList;
        }


        //what is ref?
        public string PlayByIndex(Visitor v, ref int coupons, int index)
        {
            var menu = GetGameMenu();
            if (index < 0 || index >= menu.Length)
            {
                return "Invalid game selection";
            }
            if (AgeRestriction > 0 && v.Age < AgeRestriction)
            {
                return $"Denied: need age to be at least {AgeRestriction}";
            }
            if (coupons < CouponCostPerPlay)
            {
                return $"Denied: needs {CouponCostPerPlay} coupons.";
            }
            coupons -= CouponCostPerPlay;
            return $"Played {menu[index]}. Coupons left: {coupons}";
        }

        public static readonly string[] ArcadeGamesList =
       {
            "Space Invaders", "Pac-Man", "Air Hockey", "Pinball",
            "Dance Machine", "Claw Machine", "Street Fighter"
        };

        public static readonly string[] TivoliGameList =
        {
            "Duck Shoot", "Ring Toss", "Can Knockdown",
            "Fishing Game", "Balloon Darts", "Shooting Alley", "Bottle Ring"
        };

        
        public static (int coupons, int changeSek) BuyCoupons(int moneySek)
        {
            if (moneySek < PriceSekPerBundle) return (0, moneySek);
            int bundles = moneySek / PriceSekPerBundle;
            int coupons = bundles * CouponsPerBundle;
            int change = moneySek % PriceSekPerBundle;
            return (coupons, change);
        }
    }

}
