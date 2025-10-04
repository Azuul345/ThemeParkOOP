using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemePark
{
    internal class StaffMember
    {
        internal enum StaffRole { RideOperator, FoodVendor, Cleaner, Security, TicketSeller, Entertainer, GameMaster, RestRoomPersonal }

       
        private static readonly Random rnd = new Random();
        
        private static readonly string[]  names = ["Robin", "Lukas", "Maria", "Lisa", "Zorro", "Thomas", "Johan", "Erik", "Sam", "Julia", "Stephanie"];


        public string Name { get;  private set; }
        public StaffRole Role { get; private set; }


        public StaffMember()
        {
            int nameIndex = rnd.Next(names.Length);
            Name = names[nameIndex];
            Role = StaffRole.RideOperator;
        }

        public StaffMember(string name, StaffRole role)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name is requiered ", nameof(name));
            }
            Name = name.Trim();
            Role = role;
        }

        public override string ToString()
        {
            return $"{Name} ({Role})";
        }


    }
}
