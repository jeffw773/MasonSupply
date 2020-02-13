using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Mason_Supply.Models
{
    public class Angle
    {
        //The angle of the bend
        public int AngleID { get; set; }
        public double TheAngle { get; set; }

        //the madrel radius of the bend
        public double Mandrel { get; set; }
    }
}
