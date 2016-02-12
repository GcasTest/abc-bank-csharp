using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Common
{
    /// <summary>
    /// Deprecated. No use for this method at this time
    /// </summary>
    public class DateProvider
    {
        private static DateProvider instance = null;

        public static DateProvider getInstance()
        {
            if (instance == null)
                instance = new DateProvider();
            return instance;
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
