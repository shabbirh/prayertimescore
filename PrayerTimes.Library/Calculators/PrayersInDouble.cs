namespace PrayerTimes.Library.Calculators
{
    /// <summary>
    ///     Prayer times data for a single day in floating point format.
    /// </summary>
    internal class PrayersInDouble
    {
        /// <summary>
        ///     Imsak prayer time in floating point format.
        /// </summary>
        internal double Imsak { get; set; }

        /// <summary>
        ///     Fajr prayer time in floating point format.
        /// </summary>
        internal double Fajr { get; set; }

        /// <summary>
        ///     Sunrise time in floating point format.
        /// </summary>
        internal double Sunrise { get; set; }

        /// <summary>
        ///     Dhuha prayer time in floating point format.
        /// </summary>
        internal double Dhuha { get; set; }

        /// <summary>
        ///     Dhuhr prayer time in floating point format.
        /// </summary>
        internal double Dhuhr { get; set; }

        /// <summary>
        ///     Asr prayer time in floating point format.
        /// </summary>
        internal double Asr { get; set; }

        /// <summary>
        ///     Sunset time in floating point format.
        /// </summary>
        internal double Sunset { get; set; }

        /// <summary>
        ///     Maghrib prayer time in floating point format.
        /// </summary>
        internal double Maghrib { get; set; }

        /// <summary>
        ///     Isha prayer time in floating point format.
        /// </summary>
        internal double Isha { get; set; }

        /// <summary>
        ///     Midnight time in floating point format.
        /// </summary>
        internal double Midnight { get; set; }
    }
}