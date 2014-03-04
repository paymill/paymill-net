using System;

namespace PaymillWrapper.Net
{
    internal static class DateTimeExtensions
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();

        public static int ToUnixTimestamp(this DateTime dateTime)
        {
            return (int) (dateTime - UnixEpoch).TotalSeconds;
        }

        public static DateTime ParseAsUnixTimestamp(this int timestamp)
        {
            return UnixEpoch.AddSeconds(timestamp);
        }

        public static DateTime ParseAsUnixTimestamp(this long timestamp)
        {
            return UnixEpoch.AddSeconds(timestamp);
        }
    }
}
