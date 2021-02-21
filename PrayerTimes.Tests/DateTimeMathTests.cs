using Xunit;
using PrayerTimes.Library.Helpers;

namespace PrayerTimes.Tests
{
    public class DateTimeMathTests
    {
        [Fact]
        //Test fix hour -36 to +12.
        public void TestFixHourNegative36()
        {
            Assert.Equal(12.0, DateTimeMath.FixHour(-36.0), 5);
        }

        [Fact]
        //Test fix hour -10 to +14.
        public void TestFixHourNegative10()
        {
            Assert.Equal(14.0, DateTimeMath.FixHour(-10.0), 5);
        }

        [Fact]
        //Test fix hour +10 should stay +10.
        public void TestFixHourPositive10()
        {
            Assert.Equal(10.0, DateTimeMath.FixHour(10.0), 5);
        }

        [Fact]
        //Test fix hour +27 to +3.
        public void TestFixHourPositive27()
        {
            Assert.Equal(3.0, DateTimeMath.FixHour(27.0), 5);
        }

        [Fact]
        //Test compute duration from hour 10 to 23 = 13.
        public void TestComputeDurationFrom10To23()
        {
            Assert.Equal(13.0, DateTimeMath.ComputeDuration(23.0, 10.0), 5);
        }
    }
}