using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Common
{
    public static class Utilities
    {
        public static String ToDollars(decimal d)
        {
            //return String.Format("$%,.2f", Math.Abs(d));
            return Math.Abs(d).ToString("C");
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        public static String PluralFormatter(int number, String word)
        {
            return string.Format("{0} {1}", number, (number == 1 ? word : word + "s"));
        }

    }
}
