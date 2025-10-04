using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemePark
{
    internal class Ride : Attractions
    {
        
        private bool _waterRide;
        private int _heightRestriction;
        

        public bool WaterRide { get => _waterRide; set => _waterRide = value; }

        public int HeightRestriction 
        { 
            get => _heightRestriction;
            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"Height restriction can't be negative" );
                }
                _heightRestriction = value;
            }
        }



        public Ride(string name, int durationMinutes, int heightRestriction, bool waterRide, int ageRestriction)
            : base(name, durationMinutes, ageRestriction)
        {
            
            HeightRestriction = heightRestriction;
            WaterRide = waterRide;
        }



    }
}
