using System;
using Xunit;
using prayertimescore.PrayerTimes.Lib;
using prayertimescore.PrayerTimes.Lib.Enums;
using prayertimescore.PrayerTimes.Lib.Models;
using GeoTimeZone;
using TimeZoneConverter;

namespace prayertimescore.PrayerTimes.Tests
{
    public class PrayerTimeTests
    {

        [Fact]
        public void TestTimeForUTC()
        {
            var latitude = 51.5073509;
            var longitude = -0.1277583;
            var dateTime = new DateTime(2021, 2, 18);
            var calculator = new PrayerTimesCalculator(latitude, longitude)
            {
                CalculationMethod = CalculationMethods.Jafari,
                AsrJurusticMethod = AsrJuristicMethods.Shafii
            };

            string tz = TimeZoneLookup.GetTimeZone(latitude, longitude).Result;
            TimeZoneInfo timeZoneInfo = TZConvert.GetTimeZoneInfo(tz);

            var locationTime = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, timeZoneInfo);
            var tzOffset = timeZoneInfo.BaseUtcOffset.TotalHours;
            if (locationTime.IsDaylightSavingTime())
            {
                tzOffset += 1.0;
            }

            Times prayerTimes = calculator.GetPrayerTimes(locationTime, tzOffset);

            Assert.Equal(new TimeSpan(5, 35, 0), prayerTimes.Fajr);
            Assert.Equal(new TimeSpan(12, 15, 0), prayerTimes.Dhuhr);
            Assert.Equal(new TimeSpan(17, 37, 0), prayerTimes.Maghrib);
        }

        [Fact]
        public void TestTimeForTehran()
        {
            var latitude = 35.69439;
            var longitude = 51.42151;
            var dateTime = new DateTime(2021, 2, 18);
            var calculator = new PrayerTimesCalculator(latitude, longitude)
            {
                CalculationMethod = CalculationMethods.Jafari,
                AsrJurusticMethod = AsrJuristicMethods.Shafii
            };

            string tz = TimeZoneLookup.GetTimeZone(latitude, longitude).Result;
            TimeZoneInfo timeZoneInfo = TZConvert.GetTimeZoneInfo(tz);

            var locationTime = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, timeZoneInfo);
            var tzOffset = timeZoneInfo.BaseUtcOffset.TotalHours;
            if (locationTime.IsDaylightSavingTime())
            {
                tzOffset += 1.0;
            }

            Times prayerTimes = calculator.GetPrayerTimes(locationTime, tzOffset);

            Assert.Equal(new TimeSpan(5, 35, 0), prayerTimes.Fajr);
            Assert.Equal(new TimeSpan(12, 18, 0), prayerTimes.Dhuhr);
            Assert.Equal(new TimeSpan(18, 2, 0), prayerTimes.Maghrib);

        }
    }
}