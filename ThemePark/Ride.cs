using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemePark
{
    internal class Ride : Attractions
    {
        //private string _name;
        //private double _durationtime;
        //private int _ageRestriction;
        private bool _waterRide;
        private double _heighRestriction;
        //Properties
        //public string Name { get => _name; set => _name = value; }
        //public double DurationTime { get => _durationtime; set => _durationtime = value; }
        //public int AgeRestriction { get => _ageRestriction; set => _ageRestriction = value; }
        public double HeightRestriction { get => _heighRestriction; set => _heighRestriction = value; }
        public bool WaterRide { get => _waterRide; set => _waterRide = value; }



        public Ride(string name, double duration, double heighrestriction, bool waterRide, int ageRestriction)
            : base(name, duration, ageRestriction)
        {
            Name = name;
            DurationTime = duration;
            AgeRestriction = ageRestriction;
            HeightRestriction = heighrestriction;
            WaterRide = waterRide;
        }



    }
}
