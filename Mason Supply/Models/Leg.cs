using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Mason_Supply.Models
{
    public class Leg
    {
        public int LegID { get; set; }
        public double Length { get; set; }
    }
}
