using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static ThemePark.StaffMember;

namespace ThemePark
{
    internal class Park
    {
        public const int TicketPriceSek = 100;
        public const int FoodPriceSek = 35;
        public string Name { get; }
        public List<Zone> Zones { get; } = new List<Zone>();

        public StaffMember EntrnaceSeller { get; }

        private static readonly Random rnd = new Random();


        private static readonly StaffMember.StaffRole[] RandomRolesPool = 
        {
            StaffMember.StaffRole.FoodVendor,
            StaffMember.StaffRole.Cleaner,
            StaffMember.StaffRole.Security,
            StaffMember.StaffRole.TicketSeller,
            StaffMember.StaffRole.Entertainer,
            StaffMember.StaffRole.RestRoomPersonal 
        };

        public Park(string name, List<Zone> z)
        {
            Name = name;
            for (int i = 0; i < z.Count; i++)
            {
                Zones.Add(z[i]);
            }

            EntrnaceSeller = new StaffMember("Entrance-Ticket", StaffMember.StaffRole.TicketSeller);

            if(Zones.Count > 0)
            {
                Zones[0].AddStaff(EntrnaceSeller);
            }
        }

        public void PrintZones()
        {
            Console.WriteLine($"=== {Name} ===");
            for (int i = 0; i < Zones.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {Zones[i].Name} Zone");

                for (int j = 0; j < Zones[i].Attractions.Count; j++)
                {
                    Console.WriteLine($"-{Zones[i].Attractions[j].Name}");
                }
            }
        }


        public void TryAttraction(Visitor v, Attractions a, ref int coupons, int selectionIndex = 0)
        {
            if (a is Ride r)
            {
                if (v.CheckAge(r) && v.CheckHeight(r))
                {
                    Console.WriteLine($"{v.Name} can ride {r.Name}");
                }
                else
                {

                    Console.WriteLine($"{v.Name} can't ride {r.Name}: {v.ResonForFailedRide(r)}");
                }
            }
            else if(a is GamesHub g)
            {
                var menu = g.GetGameMenu();
                Console.WriteLine($"{g.Name} ({g.Type}) Menu:");
                for(int i = 0; i < menu.Length; i++)
                {
                    Console.WriteLine($"[{i}] {menu[i]}");
                }
                string result = g.PlayByIndex(v, ref coupons, selectionIndex);
                Console.WriteLine(result);
            }

            else
            {
                Console.WriteLine($"{a.Name} is not a ride");
            }

        }

        public void AutoAssignStaff(int extraPerZone = 0, int extraRandomAcrossPark = 4)
        {
            for (int i = 0; i < Zones.Count; i++)
            {
                var z = Zones[i];
                int rideCount = 0;
                

                for (int j = 0; j < z.Attractions.Count; j++)
                {
                    if (z.Attractions[j] is Ride r)
                    {
                        rideCount++;
                    }
                    
                }

                for (int j = 0; j < rideCount; j++)
                {
                    z.AddStaff(new StaffMember($"Operator-{z.Name}-{j + 1}", StaffMember.StaffRole.RideOperator));
                }

                if (z.HasRestroom)
                {
                    z.AddStaff(new StaffMember($"Restroom attendant {z.Name}", StaffMember.StaffRole.RestRoomPersonal));
                }
                if (z.HasFoodKiosk)
                {
                    z.AddStaff(new StaffMember($"Food vendor {z.Name}", StaffMember.StaffRole.FoodVendor));
                }
                for (int j = 0; j < extraPerZone; j++)
                {
                    var role = RandomRolesPool[rnd.Next(RandomRolesPool.Length)];
                    z.AddStaff(new StaffMember($"Staff-{z.Name}-{j + 1}", role));
                }
            }
            if (extraRandomAcrossPark > 0)
                DistributeExtraStaff(extraRandomAcrossPark);
        }

        private void DistributeExtraStaff(int totalExtras)
        {
            for (int i = 0; i < totalExtras; i++)
            {
                int j = rnd.Next(Zones.Count);
                var role = RandomRolesPool[rnd.Next(RandomRolesPool.Length)];
                Zones[j].AddStaff(new StaffMember($"Staff-{role}-{i+1}", role));
            }
        }

        public string BuyFoodInZone(Visitor v, Zone z, ref int walletSek)
        {
            if (!z.HasFoodKiosk)
            {
                return $"{z.Name}: no food kiosk here";
            }
            if(walletSek < FoodPriceSek)
            {
                return $"insufficient funds to buy food. Needs {FoodPriceSek}kr to buy food";
            }
            walletSek -= FoodPriceSek;
            return $"{v.Name} bought food in {z.Name}. Money left: {walletSek}kr";
        }

        public string UseRestroom(Zone z)
        {
            return z.HasRestroom ? $"{z.Name}: restroom used." : $"{z.Name}: no restroom in this zone";
        }

    }
}


                