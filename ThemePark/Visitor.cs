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
        public int HeightCm { get; set; }

        public Visitor(string name, int age, int height)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name is requierd", nameof(name));
            if (age < 0) throw new ArgumentOutOfRangeException(nameof(age), "Age can't be negative");
            if (height < 0) throw new ArgumentOutOfRangeException(nameof(height), "Height must be over 0");

            Name = name; //.Trim() ?? 
            Age = age;
            HeightCm = height;

        }


        public bool CanRide(Ride r)
        {
            return CheckHeight(r) && CheckAge(r);
        }

        public bool CheckAge(Ride r)
        {
            
            return r.AgeRestriction == 0 || Age >= r.AgeRestriction;
        }

        public bool CheckHeight(Ride r)
        {
            
            return r.HeightRestriction == 0 || HeightCm >= r.HeightRestriction;
        }

        public string ResonForFailedRide(Ride r)
        {
            if(!CheckAge(r)) return $"Age has to be at least {r.AgeRestriction}";
            if (!CheckHeight(r)) return $"Height has to be at least {r.HeightRestriction} cm";
            return "OK";
        }

    }
}
