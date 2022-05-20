using System;

namespace Tools
{
    public class Calculator1
    {
        public static double Div(double a, double b)
        {
            if (b == 0)
            {
                return double.PositiveInfinity;
            }
            else
            {
                return a / b;
            }
        }
        
    }
}
