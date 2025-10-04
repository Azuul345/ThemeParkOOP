using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemePark
{
    internal class Visitor
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }

        public Visitor(string name, int age, double height)
        {
            Name = name;
            Age = age;
            Height = height;
            
        }




      


        public bool CheckAge(Ride r)
        {
            if (Age > r.AgeRestriction) return true;
            else return false;
        }

        public bool CheckHeight(Ride r)
        {
            if (Height >= r.HeightRestriction) return true;
            else return false;                                  
        }

    }
}
