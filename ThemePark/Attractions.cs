using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemePark
{
    internal class Attractions
    {
        private string _name;
        private double _durationtime;
        private int _ageRestriction;

        public string Name { get => _name; set => _name = value; }
        public double DurationTime { get => _durationtime; set => _durationtime = value; }
        public int AgeRestriction { get => _ageRestriction; set => _ageRestriction = value; }

        public Attractions(string name, double time, int ageRestriction)
        {
            Name = name;
            DurationTime = time;
            AgeRestriction = ageRestriction;
            
        }
    }
}
