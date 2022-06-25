using System;

namespace BilgeAdamBlog.Common.Clients.Extensions
{
    public static class GeneralExtensions
    {
        public static long ToUnixTime(this DateTime date)
        {
            long unixTimeStamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimeStamp /= TimeSpan.TicksPerSecond;
            return unixTimeStamp;
        }
    }
}
