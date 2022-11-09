using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ForSaftey
    {
        public static DateTime getDateTime(string stringDate)
        {
            int yearDate = Int32.Parse(stringDate.Substring(0, 4));
            int monthDate = Int32.Parse(stringDate.Substring(5, 2));
            int dayDate = Int32.Parse(stringDate.Substring(8, 2));

            return new DateTime(yearDate, monthDate, dayDate);
            //hehehe
        }
    }
    //test
}