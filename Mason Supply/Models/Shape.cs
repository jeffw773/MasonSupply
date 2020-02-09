using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mason_Supply.Models
{
    public class Shape
    {
        public int ShapeID { get; set; }
        static public int Leg_Num { get; set; } //The set number of legs the specific shape has
        
        public int Rebar_Type { get; set; } //The size of rebar the shape is made of. Entered as 3,4,5,6 

        //maybe make thickness (i.e. Rebar_Type / 8) an actual defined variable instead of a temp one used in methods below

        public int Quantity { get; set; }   //The number of this specific shape the customer wants

        public double[] crude_legs = new double[Leg_Num]; //sets the array to be as big as the number of legs declared for the shape and stores the customer-provided lengths of the legs

        public double[] true_legs = new double[Leg_Num];  //sets the array to be as big as the number of legs declared for the shape and stores the true lengths of all legs (straight segments of shape)

        public double[,] angles = new double[Leg_Num - 1, 2]; //Creates an array big enough for all angle and their respective Mandrel radii. [Angle, Mandrel]


        //method to convert all customer given leg values to true leg values (dimensions of only the truely straight areas)
        //Must have legs array and angles array completed before executing or else won't work.
        public void Convert_Legs_To_True_Legs() 
        {
            double Thickness = Rebar_Type / 8; //converts the given rebar type to the actual thickness of the rebar in decimal form

            for(int leg = 0; leg < crude_legs.Length; leg++)
            {
                //Hits the if statement if it is on the first leg of the shape
                if (leg == 0)
                {
                    true_legs[leg] = crude_legs[leg] - (Math.Tan(angles[leg, 0] / 2) * angles[leg, 1] * Thickness);  //converts the customer leg value to the true leg value and populates the true_legs array
                }

                //Hits else if statement if it is on the last leg of the shape
                else if (leg == crude_legs.Length - 1)
                {
                    true_legs[leg] = crude_legs[leg] - (Math.Tan(angles[leg - 1, 0] / 2) * angles[leg - 1, 1] * Thickness);  //converts the customer leg value to the true leg value and populates the true_legs array
                }

                //Hits else statement if it is on anything other than the first or last leg of the shape
                else
                {
                    true_legs[leg] = crude_legs[leg] - (Math.Tan(angles[leg - 1, 0] / 2) * angles[leg - 1, 1] * Thickness) - (Math.Tan(angles[leg, 0] / 2) * angles[leg, 1] * Thickness);  //converts the customer leg value to the true leg value and populates the true_legs array
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
            for(int leg = 0; leg < true_legs.Length; leg++)  // I believe it should be leg < true_legs.length not leg <= true_legs.length
            { 
                total_shape_length += true_legs[leg]; 
            }

            //Second for loop adds up all the Bend Allowance values in the shape
            for(int angle = 0; angle < angles.Length; angle++) // I believe it should be angle < angles.length not angle <= angles.length
            {
                total_shape_length += (((Pi / 180) * angles[angle, 1]) + ((Pi / 180) * K_Factor * Thickness)) * (angles[angle, 0]);
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

        
        //Didnt know if it would be better to have a list or an array. Decided to do an array
        //public List<Leg> legs = new List<Leg>(); //Gives the specific shape a list of legs

        //public IEnumerable<Leg> Leg_List
        //{
        //    get { return legs; }
        //}


    }
}
