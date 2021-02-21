using System;

namespace PrayerTimes.Library.Helpers
{
    /// <summary>
    ///     Provides methods for date and time related computations.
    /// </summary>
    internal static class DateTimeMath
    {
        /// <summary>
        ///     Fix the double precision floating point hour representation to a value between 0 to 24.
        /// </summary>
        /// <param name="hour">
        ///     Hour in floating point to be fixed.
        /// </param>
        /// <returns>
        ///     Hour in floating point fixed between 0 to 24.
        /// </returns>
        internal static double FixHour(double hour)
        {
            var newHour = hour - (24.0 * Math.Floor(hour / 24.0));
            if (newHour >= 0.0)
            {
                return newHour;
            }

            return newHour + 24.0;
        }

        /// <summary>
        ///     Compute the duration between the given starting hour and ending hour.
        /// </summary>
        /// <param name="end">
        ///     Ending hour in floating point.
        /// </param>
        /// <param name="start">
        ///     Starting hour in floating point.
        /// </param>
        /// <returns>
        ///     Duration between the given starting hour and ending hour in floating point.
        /// </returns>
        internal static double ComputeDuration(double end, double start)
        {
            return FixHour(end - start);
        }
    }
}