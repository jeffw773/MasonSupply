using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mason_Supply.Models
{
    public class ShapeInputView
    {
        public Shape Shape { get; set; } = new Shape();

        public Order Order { get; set; } = new Order();

        public int OrderID { get; set; }
    }
}
