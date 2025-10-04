using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemePark
{
    internal class StaffMember
    {
        Random rnd = new Random();
        

        private string _name;
        private string _role;

        public string Name { get => _name; set => _name = value; }
        public string Role { get; set; }

        string[] names = ["Robin", "Lukas", "Maria", "Lisa", "Luffy", "Thomas"];

        public StaffMember()
        {
            int name = rnd.Next(names.Length);
            Name = names[name];
        }



    }
}
