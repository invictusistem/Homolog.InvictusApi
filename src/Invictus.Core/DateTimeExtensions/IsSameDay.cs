using System;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static bool IsSameDay(this DateTime datetimenow)
        {
            var datetime = DateTime.Now;

            return datetimenow.Year == datetime.Year
                && datetimenow.Month == datetime.Month
                && datetimenow.Day == datetime.Day;
        }

        //public static bool IsSameDay(this DateTime datetime1, DateTime datetime2)
        //{
        //    return datetime1.Year == datetime2.Year
        //        && datetime1.Month == datetime2.Month
        //        && datetime1.Day == datetime2.Day;
        //}
    }
}