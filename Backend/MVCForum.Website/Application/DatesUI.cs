using System;
using MVCForum.Domain.Interfaces.Services;
using MVCForum.Utilities;

namespace MVCForum.Website.Application
{
    public static class DatesUI
    {

        private static string GetLocalisedText(string key)
        {
            var locService = ServiceFactory.Get<ILocalizationService>();
            return locService.GetResourceString(key);
        }

        /// <summary>
        /// Returns a pretty date like Facebook
        /// </summary>
        /// <param name="date"></param>
        /// <returns>28 Days Ago</returns>
        public static string GetPrettyDate(string date)
        {
            DateTime time;
            if (DateTime.TryParse(date, out time))
            {
               
             
                return DateUtils.FormatDateTime(date, "dd-MM-yyyy");
                
            }
            return date;
            //DateTime time;
            //if (DateTime.TryParse(date, out time))
            //{
            //    var span = DateTime.Now.Subtract(time);
            //    var totalDays = (int)span.TotalDays;
            //    var totalSeconds = (int)span.TotalSeconds;
            //    if ((totalDays < 0) || (totalDays >= 0x1f))
            //    {
            //        return DateUtils.FormatDateTime(date, "dd MMMM yyyy");
            //    }
            //    if (totalDays == 0)
            //    {
            //        if (totalSeconds < 60)
            //        {
            //            return GetLocalisedText("Date.JustNow");
            //        }
            //        if (totalSeconds < 120)
            //        {
            //            return GetLocalisedText("Date.OneMinuteAgo");
            //        }
            //        if (totalSeconds < 0xe10)
            //        {
            //            return string.Format(GetLocalisedText("Date.MinutesAgo"), Math.Floor((double)(((double)totalSeconds) / 60.0)));
            //        }
            //        if (totalSeconds < 0x1c20)
            //        {
            //            return GetLocalisedText("Date.OneHourAgo");
            //        }
            //        if (totalSeconds < 0x15180)
            //        {
            //            return string.Format(GetLocalisedText("Date.HoursAgo"), Math.Floor((double)(((double)totalSeconds) / 3600.0)));
            //        }
            //    }
            //    if (totalDays == 1)
            //    {
            //        return GetLocalisedText("Date.Yesterday");
            //    }
            //    if (totalDays < 7)
            //    {
            //        return string.Format(GetLocalisedText("Date.DaysAgo"), totalDays);
            //    }
            //    if (totalDays < 0x1f)
            //    {
            //        return string.Format(GetLocalisedText("Date.WeeksAgo"), Math.Ceiling((double)(((double)totalDays) / 7.0)));
            //    }
            //}
            //return date;
        }
        public static DateTime MondayOfWeek(this DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;

            if (dayOfWeek == DayOfWeek.Sunday)
            {
                //xét chủ nhật là đầu tuần thì thứ 2 là ngày kế tiếp nên sẽ tăng 1 ngày  
                //return date.AddDays(1);  

                // nếu xét chủ nhật là ngày cuối tuần  
                return date.AddDays(-6);
            }

            // nếu không phải thứ 2 thì lùi ngày lại cho đến thứ 2  
            int offset = dayOfWeek - DayOfWeek.Monday;
            return date.AddDays(-offset);
        }

        public static DateTime SaturdayOfWeek(this DateTime date)
        {
            return date.MondayOfWeek().AddDays(5);
        }
    }
}