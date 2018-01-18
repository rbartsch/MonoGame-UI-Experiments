using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITests
{
    public static class LinearConversion
    {
        /* Linear conversion, convert from one scale range to another maintaining ratio
         * new_value = ( (old_value - old_min) / (old_max - old_min) ) * (new_max - new_min) + new_min
         * new value = (((old_value)-(old_min)) / ((old_max)-(old_min))) * ((new_max)-(new_min)) + (new_min)
         * e.g Match -8.0f..-4.0f to 0.0f..5.0f
         * 0	= (((-8)-(-8)) / ((-4)-(-8))) * ((5)-(0)) + (0)
         * 1.25 = (((-7)-(-8)) / ((-4)-(-8))) * ((5)-(0)) + (0)
         * 2.5	= (((-6)-(-8)) / ((-4)-(-8))) * ((5)-(0)) + (0)
         * 3.75 = (((-5)-(-8)) / ((-4)-(-8))) * ((5)-(0)) + (0)
         * 5.0	= (((-4)-(-8)) / ((-4)-(-8))) * ((5)-(0)) + (0)
         * ...
         * or you can just calculate any current value like -7.32 to its respective other range.
         */
        public static float Float(float oldValue, float oldMin, float oldMax, float newMin, float newMax) {
            return (((oldValue) - (oldMin)) / ((oldMax) - (oldMin))) * ((newMax) - (newMin)) + (newMin);
        }

        public static int Integer(int oldValue, int oldMin, int oldMax, int newMin, int newMax) {
            return (((oldValue) - (oldMin)) / ((oldMax) - (oldMin))) * ((newMax) - (newMin)) + (newMin);
        }
    }
}
