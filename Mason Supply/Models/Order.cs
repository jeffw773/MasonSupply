using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Mason_Supply.Models
{
    public class Order
    {
        public List<Shape> shapes = new List<Shape>();
        public IEnumerable<Shape> ShapeList
        {
            get { return shapes;  }
        }

        //Unique Primary Key of Customer
        public int OrderID { get; set; }

        //Name of the customer on the order
        
        public string Customer_Name { get; set; }   

        //contact information of the customer if needed
        public string Customer_Contact { get; set; }

        public DateTime Order_date { get; set; }


        //[Range(1, 2000)] //shapes allowed in the order 
        public int Unique_Shapes { get { return shapes.Count; } }  //The number of unique shapes wanted by the customer for the order
        
        public double Estimated_Cost { get; set; } //The overall estimated cost of the order....Planning to add a method that gathers all shape data and returns a cost

        //Adds given shape to list of shapes in order
        public void AddOrderShape(Shape shape)
        {
            shapes.Add(shape);
        }

        public int Get_Unique_Shapes()
        {
            return shapes.Count;
        }

        public void Set_Estimated_Cost()
        {
            Estimated_Cost = 0; //to ensure the estimated cost is reset every time you set the total estimated cost
            for (int i = 0; i < shapes.Count; i++)
            {
                Estimated_Cost += shapes[i].Total_Cost;
            }
            return;
        }
        public double Get_Full_Cost()
        {
            return Estimated_Cost;
        }

        public Order()
            {
                Order_date = DateTime.Now;  //sets the date and time of the order to the current date and time
            }
    }
}
