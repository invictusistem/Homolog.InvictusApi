using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core
{
    public static class DoubleExtensions
    {

        public static bool IsPositive(this double number)
        {
            return number > 0;
        }

        public static bool IsNegative(this double number)
        {
            return number < 0;
        }

        public static bool IsZero(this double number)
        {
            return number == 0;
        }

        public static bool NegativeOrZero(this double number)
        {
            return IsNegative(number) || IsZero(number);
        }
    }
}
