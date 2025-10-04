using System.Xml.Serialization;

namespace ThemePark
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<Zone> zones = Zone.CreateAllZones();

            Park park = new Park("Jego copyright infringement Park", zones);

            park.AutoAssignStaff();



            string name = PromptText("Enter your name");
            int age = PromptInt("Enter your age", 0,70);
            int heightCm = PromptInt("Enter your height (cm)", 20, 230);

            var visitor = new Visitor(name, age, heightCm);

            int wallet = 300;
            int coupons = 0;

            Console.WriteLine(BuyTicket(park,visitor,ref wallet));

            while (true)
            {
                Console.WriteLine($"\n Wallet: {wallet}kr | Coupons: {coupons} ");
                ShowParkLayout(park);

                var zoneNames = new List<string>();
                for (int i = 0; i < zones.Count; i++)
                {
                    zoneNames.Add(zones[i].Name);
                }
                zoneNames.Add("Leave park");

                int choice = PromptMenu("Select a zone to visit or leave park", zoneNames);
                if(choice == park.Zones.Count)
                {
                    Console.WriteLine($"Thank you for visiting {park.Name}");
                    return;
                }

                var zone = park.Zones[choice];
                ZoneLoop(park, zone,visitor, ref wallet, ref coupons);

            }

            //Visitor luffy = new Visitor("luffy", 18, 160);

            //Attractions a = park.Zones[0].Attractions[0];

            //park.TryAttraction(luffy, a);

            //park.PrintZones();

            //Console.WriteLine(luffy.CheckHeight((Ride)Zone.North[0]));
            //Console.WriteLine(Zone.North[0].Name);

            //StaffMember n = new StaffMember();
            //Console.WriteLine(n.Name);






            Console.ReadKey();
         }

        static string PromptText(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                var s = Console.ReadLine();
                if (!string.IsNullOrEmpty(s))
                {
                    return s.Trim();
                }
                Console.WriteLine("Please enter a value");
            }
        }

        static int PromptInt(string message, int min, int max)
        {
            while (true)
            {
                Console.WriteLine(message);
                if(int.TryParse(Console.ReadLine(), out int n) && n >= min && n <= max)
                {
                    return n;
                }
                    Console.WriteLine($"Enter a number between {min} and {max}.");
            }
        }

        static int PromptMenu(string title, IList<string> options, bool includeBack = false)
        {
            Console.WriteLine(title);
            for(int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"[{i}] {options[i]}");
            }
            if(includeBack)
            {
                Console.WriteLine($"[b] Back");
            }
            while (true)
            {
                Console.Write("select index: ");
                var s = Console.ReadLine();
                if(includeBack && string.Equals(s,"b", StringComparison.OrdinalIgnoreCase))
                {
                    return -1;
                }
                if(int.TryParse(s,out int idx) && idx >= 0 && idx < options.Count)
                {
                    return idx;
                }
                Console.WriteLine("invalid selection");
            }
        }

        static void ShowParkLayout(Park park)
        {
            Console.WriteLine($"==={park.Name}===");
            for (int i = 0; i < park.Zones.Count; i++)
            {
                var z = park.Zones[i];
                Console.WriteLine($"[{i}] {z.Name} (Food: {(z.HasFoodKiosk ? "Yes": "No")}, " +
                    $"Restroom: {(z.HasRestroom ? "Yes":"No")})");
                for(int j = 0; j < z.Attractions.Count; j++)
                {
                    Console.WriteLine($"  - {z.Attractions[j].Name}");
                }
            }
        }

        static string BuyTicket(Park park, Visitor v, ref int wallet)
        {
            var seller = park.EntrnaceSeller;

            int price = Park.TicketPriceSek;
            if (wallet < price)
            {
                return $"{v.Name}: not enough money for tickets({price}kr. Ticket seller: {seller.Name}.)";
            }
            wallet -= price;

            return $"{v.Name} bought an entry ticket from Ticket seller {seller.Name} for {price}kr. Left {wallet}kr. ";
        }
        
        static GamesHub? FindHub(Zone zone)
        {
            for (int i = 0; i < zone.Attractions.Count; i++)
                if (zone.Attractions[i] is GamesHub gh) return gh;
            return null;
        }

        static void ZoneLoop(Park park, Zone zone, Visitor v, ref int wallet, ref int coupons)
        {
            while (true)
            {
                Console.WriteLine($"\n--{zone.Name}--");
                var hasHub = FindHub(zone) != null;

                var opts = new List<string>();
                opts.Add("Ride an attraction");
                if (hasHub)
                {
                    opts.Add("Visit game hub");
                }
                if (zone.HasFoodKiosk)
                {
                    opts.Add("Buy food");
                }
                if (zone.HasRestroom)
                {
                    opts.Add("Use restroom");
                }
                opts.Add("Back to Zones");

                int choice = PromptMenu("Choose:", opts);
                int idx = 0;

                if (choice == idx++) { RideFlow(park, zone, v, ref coupons); continue; }
                if (hasHub && choice == idx++) { GamesFlow(park, zone, v, ref wallet, ref coupons); continue; }
                if (zone.HasFoodKiosk && choice == idx++) { Console.WriteLine(park.BuyFoodInZone(v, zone, ref wallet)); continue; }
                if (zone.HasRestroom && choice == idx++) { Console.WriteLine(park.UseRestroom(zone)); MaybeGreetSecurity(zone); continue; }
                return;
            }
        }

        static void RideFlow(Park park, Zone zone, Visitor v, ref int coupons)
        {
            var rides = new List<Ride>();
            for(int i = 0; i < zone.Attractions.Count; i++)
            {
                if (zone.Attractions[i] is Ride r)
                {
                    rides.Add(r);
                }
            }

            var names = new List<string>();
            for (int i = 0; i < rides.Count; i++)
            {
                names.Add(rides[i].Name);
            }
            int idx = PromptMenu("Choose a ride:", names, includeBack: true);
            if (idx == -1) return;

            park.TryAttraction(v, rides[idx], ref coupons);
        }
        //why ?
        static StaffMember? PickStaffInZone(Zone z, StaffMember.StaffRole role)
        {
            for (int i = 0; i < z.Staff.Count; i++)
                if (z.Staff[i].Role == role) return z.Staff[i];
            return null;
        }

        static void GamesFlow(Park park, Zone zone, Visitor v, ref int wallet, ref int coupons)
        {
            var hub = FindHub(zone);
            if (hub == null) { Console.WriteLine("No game hub in this zone."); return; }
            var seller = PickStaffInZone(zone, StaffMember.StaffRole.GameMaster);
            Console.WriteLine($"{seller.Name} Welcomes you to {hub.Name}");

            while (true)
            {

                var options = new List<string> { "Play games", "Buy coupons", "Go back" };
                int choice = PromptMenu("Choose:", options);

                if (choice == 0)
                {
                    if (coupons < GamesHub.CouponCostPerPlay)
                    {
                        Console.WriteLine($"Not enough coupons to play. Coupons needed: {GamesHub.CouponCostPerPlay}");
                        continue;
                    }

                    var menu = hub.GetGameMenu();
                    int gameIdx = PromptMenu("Choose a game", new List<string>(menu), includeBack: true);
                    if (gameIdx == -1) 
                    {
                        continue;
                    }
                    park.TryAttraction(v, hub, ref coupons, selectionIndex: gameIdx);
                    Console.WriteLine($"Coupons left: {coupons}");
                    continue;
                }

                if (choice == 1)
                {
                    if(wallet < GamesHub.PriceSekPerBundle)
                    {
                        Console.WriteLine($"{seller.Name} says: you can unfortuantly not buy any coupons");
                        continue;
                    }
                    else
                    {
                        wallet -= GamesHub.PriceSekPerBundle;
                        coupons += GamesHub.CouponsPerBundle;
                        Console.WriteLine($"{v.Name} bought {GamesHub.CouponsPerBundle} from {seller.Name} " +
                            $"\nMoney left in wallet: {wallet}");
                    }
                    continue;

                }
                if (choice == 2) return;


            }

        }

        static void MaybeGreetSecurity(Zone z)
        {
            for (int i = 0; i < z.Staff.Count; i++)
                if (z.Staff[i].Role == StaffMember.StaffRole.Security)
                {
                    Console.WriteLine($"You see security: {z.Staff[i].Name}.");
                    return;
                }
        }
        
    }
}
