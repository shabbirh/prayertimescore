using System.ComponentModel;
using System;
using NodaTime;
using PrayerTimes.Library.Helpers;
using PrayerTimes.Library.Enumerations;
using PrayerTimes.Library.Models;
using PrayerTimes.Library.Calculators;
using System.Globalization;
using prayertimescore.PrayerTimes.Library.Calender;

namespace PrayerTimes.Console
{
    internal static class Program
    {
        private static int Year = 2021;
        private static int Month = 2;
        private static int Day = 26;
        private static double TimeZone = 0.0;
        private static int LunarHijriOffset = -1;
        private static void Main(string[] args)
        {
            if (args is null)
            {
                ShowUsage();
            }
            else
            {
                if (args.Length > 5)
                {
                    Year = int.Parse(args[1]);
                    Month = int.Parse(args[2]);
                    Day = int.Parse(args[3]);
                    TimeZone = double.Parse(args[4]);
                    LunarHijriOffset = int.Parse(args[5]);
                }
                else
                {
                    ShowUsage();
                }
            }

            var when = Instant.FromUtc(Year, Month, Day, 0, 0);

            var settings = new PrayerCalculationSettings();
            settings.CalculationMethod.SetCalculationMethodPreset(when,
            CalculationMethodPreset.IthnaAshari);
            var geo = new Geocoordinate(52.62972887, -1.1315918, 70.0);

            var prayer = Prayers.On(when, settings, geo, TimeZone);
            SolarDate solarDate = prayertimescore.PrayerTimes.Library.Calender.Calendar.ConvertToPersian(when.ToDateTimeUtc());
            LunarDate lunarDate = prayertimescore.PrayerTimes.Library.Calender.Calendar.ConvertToIslamic(when.ToDateTimeUtc().AddDays(LunarHijriOffset));
            DateTime gregorianDate = when.ToDateTimeUtc();

            System.Console.WriteLine($"Prayer Times at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for {gregorianDate:D} (Solar: {solarDate.ToString("N")} / Lunar: {lunarDate.ToString("N")}):");
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
            System.Console.WriteLine($"Current prayer at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for {gregorianDate:D}:");
            System.Console.WriteLine($"{current.Type} - {GetPrayerTimeString(current.Time)}");

            // Generate next prayer time
            var next = Prayer.Next(settings, geo, TimeZone, SystemClock.Instance);
            System.Console.WriteLine($"Next prayer at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for {gregorianDate:D}:");
            System.Console.WriteLine($"{next.Type} - {GetPrayerTimeString(next.Time)}");

            // Generate later prayer time
            var later = Prayer.Later(settings, geo, TimeZone, SystemClock.Instance);
            System.Console.WriteLine($"Later prayer at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for {gregorianDate:D}:");
            System.Console.WriteLine($"{later.Type} - {GetPrayerTimeString(later.Time)}");

            // Generate after later prayer time
            var afterLater = Prayer.AfterLater(settings, geo, TimeZone, SystemClock.Instance);
            System.Console.WriteLine($"After later prayer at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for {gregorianDate:D}:");
            System.Console.WriteLine($"{afterLater.Type} - {GetPrayerTimeString(afterLater.Time)}");

            System.Console.WriteLine();
            System.Console.WriteLine("Press any key to exit");
            System.Console.ReadLine();
        }

        private static void ShowUsage()
        {
            System.Console.WriteLine($"Calulating for default test value of {Day}/{Month}/{Year} and Timezone Offset of {TimeZone}");
            System.Console.WriteLine("To provide a date to calculate for please provide four parameters as shown");
            System.Console.WriteLine($"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name} [int:year] [int:month] [int:day] [double:timezoneOffset] [int:lunarHijriOffsetDays]");
        }
        private static string GetPrayerTimeString(Instant instant)
        {
            var zoned = instant.InZone(DateTimeZone.ForOffset(Offset.FromTimeSpan(TimeSpan.FromHours(TimeZone))));
            return zoned.ToString("HH:mm", CultureInfo.InvariantCulture);
        }
    }
}
