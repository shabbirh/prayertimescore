using System;
using NodaTime;
using PrayerTimes.Library.Helpers;
using PrayerTimes.Library.Enumerations;
using PrayerTimes.Library.Models;
using PrayerTimes.Library.Calculators;
using System.Globalization;

namespace PrayerTimes.Console
{
    internal static class Program
    {
        private static int _year = 2022;
        private static int _month = 5;
        private static int _day = 31;
        private static double _timeZone = 1.0;
        private static int _lunarHijriOffset = -1;
        private static double _latitude = 52.655386693734208;
        private static double _longitude = -1.1239285417252036;
        private static double _altitude = 0.0;
        private static readonly CalculationMethodPreset _calculationMethod = CalculationMethodPreset.IthnaAshari;
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
                    _year = int.Parse(args[1]);
                    _month = int.Parse(args[2]);
                    _day = int.Parse(args[3]);
                    _timeZone = double.Parse(args[4]);
                    _lunarHijriOffset = int.Parse(args[5]);
                    _latitude = double.Parse(args[6]);
                    _longitude = double.Parse(args[7]);
                    _altitude = double.Parse(args[8]);
                }
                else
                {
                    ShowUsage();
                }
            }

            var when = Instant.FromUtc(_year, _month, _day, 0, 0);

            var settings = new PrayerCalculationSettings();
            settings.CalculationMethod.SetCalculationMethodPreset(when, _calculationMethod);
            var geo = new Geocoordinate(_latitude, _longitude, _altitude);

            var prayer = Prayers.On(when, settings, geo, _timeZone);
            var solarDate = prayertimescore.PrayerTimes.Library.Calender.Calendar.ConvertToPersian(when.ToDateTimeUtc());
            var lunarDate = prayertimescore.PrayerTimes.Library.Calender.Calendar.ConvertToIslamic(when.ToDateTimeUtc().AddDays(_lunarHijriOffset));
            var gregorianDate = when.ToDateTimeUtc();

            var solarDateString = $" {solarDate.ToString("english_day")} {solarDate.ToString("english_month")} {solarDate.ToString("english_year")}";
            var lunarDateString = $" {lunarDate.ToString("english_day")} {lunarDate.ToString("english_month")} {lunarDate.ToString("english_year")}";

            System.Console.WriteLine($"Prayer Times at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for {gregorianDate:D} (Solar: {solarDateString} / Lunar: {lunarDate.ToString("english_formatted")}):");
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
            var current = Prayer.Now(settings, geo, _timeZone, SystemClock.Instance);
            System.Console.WriteLine($"Current prayer at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for {gregorianDate:D}:");
            System.Console.WriteLine($"{current.Type} - {GetPrayerTimeString(current.Time)}");

            // Generate next prayer time
            var next = Prayer.Next(settings, geo, _timeZone, SystemClock.Instance);
            System.Console.WriteLine($"Next prayer at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for {gregorianDate:D}:");
            System.Console.WriteLine($"{next.Type} - {GetPrayerTimeString(next.Time)}");

            // Generate later prayer time
            var later = Prayer.Later(settings, geo, _timeZone, SystemClock.Instance);
            System.Console.WriteLine($"Later prayer at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for {gregorianDate:D}:");
            System.Console.WriteLine($"{later.Type} - {GetPrayerTimeString(later.Time)}");

            // Generate after later prayer time
            var afterLater = Prayer.AfterLater(settings, geo, _timeZone, SystemClock.Instance);
            System.Console.WriteLine($"After later prayer at [{geo.Latitude}, {geo.Longitude}, {geo.Altitude}] for {gregorianDate:D}:");
            System.Console.WriteLine($"{afterLater.Type} - {GetPrayerTimeString(afterLater.Time)}");

            System.Console.WriteLine();
            System.Console.WriteLine("Press any key to exit");
            System.Console.ReadLine();
        }

        private static void ShowUsage()
        {
            System.Console.WriteLine($"Calculating for default test value of {_day}/{_month}/{_year} and Timezone Offset of {_timeZone}{Environment.NewLine}For geolocation:");
            System.Console.WriteLine($"Latitude: {_latitude}");
            System.Console.WriteLine($"Longitude: {_longitude}");
            System.Console.WriteLine($"Altitude: {_altitude}");
            System.Console.WriteLine("To provide a date to calculate for please provide four parameters as shown");
            System.Console.WriteLine();
            System.Console.WriteLine($"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name} [int:year] \\{Environment.NewLine}\t[int:month] \\{Environment.NewLine}\t[int:day] \\{Environment.NewLine}\t[double:timeZoneOffset] \\{Environment.NewLine}\t[int:lunarHijriOffsetDays] \\{Environment.NewLine}\t[double:latitude] \\{Environment.NewLine}\t[double:longitude] \\{Environment.NewLine}\t[double:altitude]");
            System.Console.WriteLine();
            System.Console.WriteLine(
                "year/month/day: these are integers representing the year (four digits), month (two digits), day (two digits)");
            System.Console.WriteLine($"timeZoneOffset: this is the timezone offset; for example,{Environment.NewLine}for the UK in BST, it would be 1.0, but during GMT it would be 0.0 and so on.{Environment.NewLine}Bear in mind this should NOT be in hours and minutes; but as a true decimal.{Environment.NewLine}So, for example, an offset of +3 hours and 30 minutes, would be +3.50 and NOT +3.30.{Environment.NewLine}bear in mind offsets can be positive or negative, so be sure to provide that also.");
            System.Console.WriteLine($"lunarHijriOffsetDays: this is the number of days (positive or negative){Environment.NewLine}that need to be offset to get the correct Lunar date{Environment.NewLine}(lunar date is generally based on the moon sighting;{Environment.NewLine}sometimes the calculation for a given lunar date is off by 1 or 2{Environment.NewLine}days (either side)");
            System.Console.WriteLine("latitude/longitude/altitude: these are the coordinates of the location you want to get the prayer times for");
            System.Console.WriteLine();
            System.Console.WriteLine();
        }
        private static string GetPrayerTimeString(Instant instant)
        {
            var zoned = instant.InZone(DateTimeZone.ForOffset(Offset.FromTimeSpan(TimeSpan.FromHours(_timeZone))));
            return zoned.ToString("HH:mm", CultureInfo.InvariantCulture);
        }
    }
}
