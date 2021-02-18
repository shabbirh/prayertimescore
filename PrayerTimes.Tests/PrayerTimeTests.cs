using System;
using Xunit;
using prayertimescore.PrayerTimes.Lib;
using prayertimescore.PrayerTimes.Lib.Enums;
using prayertimescore.PrayerTimes.Lib.Models;


namespace prayertimescore.PrayerTimes.Tests
{
    public class PrayerTimeTests
    {

        [Fact]
        public void TestTimeForUK()
        {
            var calculator = new PrayerTimesCalculator(51.5073509, -0.1277583)
            {
                CalculationMethod = CalculationMethods.Jafari,
                AsrJurusticMethod = AsrJuristicMethods.Hanafi
            };

            Times prayerTimes = calculator.GetPrayerTimes(DateTime.Now, 1);


            Assert.Equal(new TimeSpan(5, 33, 0), prayerTimes.Fajr);
            Assert.Equal(new TimeSpan(7, 15, 0), prayerTimes.Sunrise);
            Assert.Equal(new TimeSpan(12, 18, 0), prayerTimes.Dhuhr);
            Assert.Equal(new TimeSpan(15, 31, 0), prayerTimes.Asr);
            Assert.Equal(new TimeSpan(17, 45, 0), prayerTimes.Maghrib);
            Assert.Equal(new TimeSpan(18, 51, 0), prayerTimes.Isha);


        }
    }
}