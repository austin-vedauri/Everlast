using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Managers
{
    public static class ManagerAssistant
    {
        public static DateTime GetFirstDayOfCurrentWeek()
        {
            DateTime now = DateTime.Now;

            DateTime result = now.AddDays(-(int)now.DayOfWeek);

            return result;
        }

        public static DateTime GetLastDayOfCurrentWeek()
        {
            DateTime now = DateTime.Now;

            DateTime start = now.AddDays(-(int)now.DayOfWeek);
            DateTime result = start.AddDays(7).AddSeconds(-1);

            return result;
        }
    }
}