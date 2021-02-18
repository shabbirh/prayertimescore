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

            Assert.Equal(new TimeSpan(5, 35, 0), prayerTimes.Fajr);

        }
    }
}