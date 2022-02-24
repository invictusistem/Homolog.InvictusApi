using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToCompleteTime(this DateTime date, int hour, int minute)
        {
            DateTime newDate = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            
            return newDate;
        }

        public static int CalculateTotalMinutes(this DateTime date, DateTime dateIni, DateTime dateFinal) 
        {
            TimeSpan minutes = dateFinal - dateIni;
            
            return Convert.ToInt32(minutes.TotalMinutes);
        }
    }

    
}
