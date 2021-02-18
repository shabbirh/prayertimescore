using System;

namespace prayertimescore.PrayerTimes.Lib.Models
{
    public class Times
    {
        public DateTimeOffset Date { get; set; }
        public TimeSpan Fajr { get; set; }
        public TimeSpan Sunrise { get; set; }
        public TimeSpan Dhuhr { get; set; }
        public TimeSpan Asr { get; set; }
        public TimeSpan Sunset { get; set; }
        public TimeSpan Maghrib { get; set; }
        public TimeSpan Isha { get; set; }
    }
}