using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemePark
{
    internal class Zone
    {

        public string Name { get; }
        public List<Attractions> Attractions { get; } = new();
        public List<StaffMember> Staff { get; } = new();

        public bool HasFoodKiosk { get; }
        public bool HasRestroom { get; }

        public Zone(string name, Attractions[] attractions, bool hasFoodKiosk, bool hasRestroom)
        {
            Name = name;
            for (int i = 0; i < attractions.Length; i++)
            {
                Attractions.Add(attractions[i]);
            }

            HasFoodKiosk = hasFoodKiosk;
            HasRestroom = hasRestroom;
        }


        public static List<Zone> CreateAllZones()
        {
            var North = new Attractions[]
            {
                new Ride("Mountain Ride", 3, 160, false, 13),
                new Ride("Water Slide", 2, 120, true, 9),
                new GamesHub("Tivoli Games",60,11, GamesHub.GameType.Tivoli)
            };

            var East = new Attractions[]
            {
                new Ride("Swings", 3, 90, false,9),
                new Ride("Train Ride", 4, 0, false, 3)
            };

            var West = new Attractions[]
            {
                new Ride("Bababoi", 3, 150, false, 13),
                new Ride("SuperMan", 2, 100, false, 9),
                new GamesHub("Arcade Games",60,6, GamesHub.GameType.Arcade)
            };

            var South = new Attractions[]
            {
                new Ride("Pirate Ocean", 7, 80, true, 7),
                new Ride("Mario Road", 5, 80, true, 6)
            };

            var zones = new List<Zone>();
            zones.Add(new Zone("North", North, hasFoodKiosk: true, hasRestroom: true));
            zones.Add(new Zone("East", East, hasFoodKiosk: true, hasRestroom: false));
            zones.Add(new Zone("West", West, hasFoodKiosk: false, hasRestroom: false));
            zones.Add(new Zone("South", South, hasFoodKiosk: true, hasRestroom: true));

            for (int i = 0; i < zones.Count; i++)
            {
                var zone = zones[i];

                bool hasHub = false;
                for (int j = 0; j < zone.Attractions.Count; j++)
                    if (zone.Attractions[j] is GamesHub) { hasHub = true; break; }

                if (!hasHub) continue;

                bool hasGM = false;
                for (int k = 0; k < zone.Staff.Count; k++)
                    if (zone.Staff[k].Role == StaffMember.StaffRole.GameMaster) { hasGM = true; break; }

                if (!hasGM)
                    zone.AddStaff(new StaffMember($"GameMaster {zone.Name}", StaffMember.StaffRole.GameMaster));
            }

            return zones;
        }

        public void AddStaff(StaffMember s)
        {
            Staff.Add(s);
        }

    }
}
