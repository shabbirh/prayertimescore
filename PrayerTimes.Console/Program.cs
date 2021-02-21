using System.ComponentModel;
using System;
using NodaTime;
using PrayerTimes.Library.Helpers;
using PrayerTimes.Library.Enumerations;
using PrayerTimes.Library.Models;
using PrayerTimes.Library.Calculators;
using System.Globalization;

namespace PrayerTimes.Console
{
    class Program
    {
        private const int Year = 2021;
        private const int Month = 2;
        private const int Day = 21;
        private const double TimeZone = 0.0;
        static void Main(string[] args)
        {
            var when = Instant.FromUtc(Year, Month, Day, 0, 0);

            var settings = new PrayerCalculationSettings();
            settings.CalculationMethod.SetCalculationMethodPreset(when,
            CalculationMethodPreset.IthnaAshari);
            var geo = new Geocoordinate(52.62972887, -1.1315918, 70.0);

            var prayer = Prayers.On(when, settings, geo, TimeZone);

            System.Console.WriteLine($"Prayer Times at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for April 12th, 2018:");
            System.Console.WriteLine($"Imsak: {GetPrayerTimeString(prayer.Imsak)}");
            System.Console.WriteLine($"Fajr: {GetPrayerTimeString(prayer.Fajr)}");
            System.Console.WriteLine($"Sunrise: {GetPrayerTimeString(prayer.Sunrise)}");
            System.Console.WriteLine($"Dhuha: {GetPrayerTimeString(prayer.Dhuha)}");
            System.Console.WriteLine($"Dhuhr: {GetPrayerTimeString(prayer.Dhuhr)}");
            System.Console.WriteLine($"Asr: {GetPrayerTimeString(prayer.Asr)}");
            System.Console.WriteLine($"Sunset: {GetPrayerTimeString(prayer.Sunset)}");
            System.Console.WriteLine($"Maghrib: {GetPrayerTimeString(prayer.Maghrib)}");
            System.Console.WriteLine($"Isha: {GetPrayerTimeString(prayer.Isha)}");
            System.Console.WriteLine($"Midnight: {GetPrayerTimeString(prayer.Midnight)}");

            // Generate current prayer time
            var current = Prayer.Now(settings, geo, TimeZone, SystemClock.Instance);
            System.Console.WriteLine($"Current prayer at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for April 12th, 2018:");
            System.Console.WriteLine($"{current.Type} - {GetPrayerTimeString(current.Time)}");

            // Generate next prayer time
            var next = Prayer.Next(settings, geo, TimeZone, SystemClock.Instance);
            System.Console.WriteLine($"Next prayer at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for April 12th, 2018:");
            System.Console.WriteLine($"{next.Type} - {GetPrayerTimeString(next.Time)}");

            // Generate later prayer time
            var later = Prayer.Later(settings, geo, TimeZone, SystemClock.Instance);
            System.Console.WriteLine($"Later prayer at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for April 12th, 2018:");
            System.Console.WriteLine($"{later.Type} - {GetPrayerTimeString(later.Time)}");

            // Generate after later prayer time
            var afterLater = Prayer.AfterLater(settings, geo, TimeZone, SystemClock.Instance);
            System.Console.WriteLine($"After later prayer at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for April 12th, 2018:");
            System.Console.WriteLine($"{afterLater.Type} - {GetPrayerTimeString(afterLater.Time)}");

            System.Console.WriteLine();
            System.Console.WriteLine("Press any key to exit");
            System.Console.ReadLine();

        }

        static string GetPrayerTimeString(Instant instant)
        {
            var zoned = instant.InZone(DateTimeZone.ForOffset(Offset.FromTimeSpan(TimeSpan.FromHours(TimeZone))));
            return zoned.ToString("HH:mm", CultureInfo.InvariantCulture);
        }
    }
}
