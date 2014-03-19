using System;

namespace PaymillWrapper.Utils
{
    internal static class DateTimeExtensions
    {

        private static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
  
        public static int ToUnixTimestamp(this DateTime dateTime)
        {
            return (int)(dateTime - _unixEpoch).TotalSeconds;
        }

        public static DateTime ParseAsUnixTimestamp(this int timestamp)
        {
            return _unixEpoch.AddSeconds(timestamp);
        }

        public static DateTime ParseAsUnixTimestamp(this long timestamp)
        {
            return _unixEpoch.AddSeconds(timestamp);
        }
    }
}
