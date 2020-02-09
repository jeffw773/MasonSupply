using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mason_Supply.Models
{
    public class Order
    {
        public List<Shape> shapes = new List<Shape>();
        public IEnumerable<Shape> ShapeList
        {
            get { return shapes;  }
        }

        public int OrderID { get; set; } //Unique Primary Key of Customer

        public string Customer_Name { get; set; }   //Name of the customer on the order

        public DateTime Order_date { get; set; }

        public int Unique_Shapes { get { return shapes.Count; }  }  //returns the number of unique shapes wanted by the customer via the list of shapes
        
        public double Estimated_Cost { get; set; } //The overall estimated cost of the order....Planning to add a method that gathers all shape data and returns a cost

        public Order()
            {
                Order_date = DateTime.Now;  //sets the date and time of the order to the current date and time
            }
    }
}
