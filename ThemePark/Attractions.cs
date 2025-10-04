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
        private int _durationMinutes;
        private int _ageRestriction;

        public string Name 
        { 
            get => _name; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name is required", nameof(value));
                }
                _name = value.Trim();
            }
        }

        public int DurationMinutes 
        { 
            get => _durationMinutes; 
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The Duration must be more than 0 minutes");
                }
                _durationMinutes = value;
            }
        }
        public int AgeRestriction 
        { 
            get => _ageRestriction; 
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Age restriction can not be negative");
                }
                _ageRestriction = value;
            }
        }

        public Attractions(string name, int durationMinutes, int ageRestriction)
        {
            Name = name;
            DurationMinutes = durationMinutes;
            AgeRestriction = ageRestriction;
            
        }

        public override string ToString() 
        {
            return $"{Name} ({DurationMinutes} min, min age {AgeRestriction}+ )";
        }


    }
}
