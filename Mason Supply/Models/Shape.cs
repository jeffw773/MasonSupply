using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Mason_Supply.Models
{
    public class Shape
    {
        //List of crude dimensions of the legs given by the customer
        public List<Leg> crude_legs = new List<Leg>();
        public IEnumerable<Leg> Crude_Leg_List
        {
            get { return crude_legs; }
        }

        //List of accurate leg lengths to then use for the formulas
        public List<Leg> true_legs = new List<Leg>();
        public IEnumerable<Leg> Leg_List
        {
            get { return true_legs; }
        }

        public List<Angle> angles = new List<Angle>();
        public IEnumerable<Angle> Angle_List
        {
            get { return angles; }
        }

        public int ShapeID { get; set; }

        [Range(1, 50)] //Max range will most likely be far less than 50 lol
        [Required]
        public int Leg_Num { get; set; } //The set number of legs the specific shape has

        public double Total_Cost { get; set; }

        [Range(3, 6)] //rebar types can only be 3, 4, 5, and 6
        [Required]
        public int Rebar_Type { get; set; } //The size of rebar the shape is made of. Entered as 3,4,5,6 

        //maybe make thickness (i.e. Rebar_Type / 8) an actual defined variable instead of a temp one used in methods below
        
        [Range(1, 2000)] //Have no idea when Jared will decline an order by having too many required of one shapes
        [Required]
        public int Quantity { get; set; }   //The number of this specific shape the customer wants

        //NOT USED ANYMORE BECAUSE I STOPPED BEING STUBBORN AND USED STUPID LISTS
        //public double[] crude_legs = new double[leg_num]; //sets the array to be as big as the number of legs declared for the shape and stores the customer-provided lengths of the legs

        //public double[] true_legs = new double[leg_num];  //sets the array to be as big as the number of legs declared for the shape and stores the true lengths of all legs (straight segments of shape)

        //public double[,] angles = new double[leg_num - 1, 2]; //Creates an array big enough for all angle and their respective Mandrel radii. [Angle, Mandrel]


        //method to convert all customer given leg values to true leg values (dimensions of only the truely straight areas)
        //Must have legs array and angles array completed before executing or else won't work.
        public void Convert_Legs_To_True_Legs() 
        {
            double Thickness = Rebar_Type / 8; //converts the given rebar type to the actual thickness of the rebar in decimal form

            for(int leg = 0; leg < crude_legs.Count; leg++)
            {
                //Hits the if statement if it is on the first leg of the shape
                if (leg == 0)
                {
                    true_legs[leg].Length = crude_legs[leg].Length - (Math.Tan(angles[leg].TheAngle / 2) * angles[leg].Mandrel * Thickness);  //converts the customer leg value to the true leg value and populates the true_legs array
                }

                //Hits else if statement if it is on the last leg of the shape
                else if (leg == crude_legs.Count - 1)
                {
                    true_legs[leg].Length = crude_legs[leg].Length - (Math.Tan(angles[leg - 1].TheAngle / 2) * angles[leg - 1].Mandrel * Thickness);  //converts the customer leg value to the true leg value and populates the true_legs array
                }

                //Hits else statement if it is on anything other than the first or last leg of the shape
                else
                {
                    true_legs[leg].Length = crude_legs[leg].Length - (Math.Tan(angles[leg - 1].TheAngle / 2) * angles[leg - 1].Mandrel * Thickness) - (Math.Tan(angles[leg].TheAngle / 2) * angles[leg].Mandrel * Thickness);  //converts the customer leg value to the true leg value and populates the true_legs array
                }
            }

            return; //When task of converting legs to true legs has completed
        }

        public double Total_Shape_Length() //so far returns the total length of the shape based on the true leg values and angle/mandrel values
        {
            double K_Factor = .446; //Global unit for K_Factor......Could be different, but for most materials it is .446 inches
            double Thickness = Rebar_Type / 8; //converts the given rebar type to the actual thickness of the rebar in decimal form
            double Pi = 3.141592653589793238;


            //Converts all innaccurate leg values to true leg values before calculating the total_shape_length
            Convert_Legs_To_True_Legs();

            double total_shape_length = 0;

            //First for loop adds up all the leg lengths in the legs array and adds them to total_shape_length
            for(int leg = 0; leg < true_legs.Count; leg++)  // I believe it should be leg < true_legs.length not leg <= true_legs.length
            { 
                total_shape_length += true_legs[leg].Length; 
            }

            //Second for loop adds up all the Bend Allowance values in the shape
            for(int angle = 0; angle < angles.Count; angle++) // I believe it should be angle < angles.length not angle <= angles.length
            {
                total_shape_length += (((Pi / 180) * angles[angle].Mandrel) + ((Pi / 180) * K_Factor * Thickness)) * (angles[angle].TheAngle);
            }

            return total_shape_length; //returns the summation of all the parts of the shape
        }

        //Method returns the amount of this shape able to be made out of a 20' bar rounded to the nearest whole shape
        public int Shapes_Per_Bar()
        {
            return (int)Math.Floor(240.00 / Total_Shape_Length()); 
        }

        //returns the number of bars needed to make the current shape rounded to the nearest whole bar.
        public int Bars_Needed()
        {
            return (int)Math.Ceiling((decimal)(Quantity / Shapes_Per_Bar())); 
        }

        public double PriceOf(int type)
        {
            //Prices are NOT accurate. ONLY for testing
            if (type == 3)
            {
                return 5;
            }
            else if (type == 4)
            {
                return 10;
            }
            else if (type == 5)
            {
                return 15;
            }
            else //if type == 6
            {
                return 20;
            }
        }

        //revise this as more info is received for the cost per each rebar type
        public double Set_Total_Cost()
        {
            Total_Cost = 0; //ensures the total cost is reset every time the total cost is being set
            Total_Cost = Bars_Needed() * (PriceOf(Rebar_Type));
            return Total_Cost;
        }
    }
}
