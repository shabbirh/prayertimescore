namespace PrayerTimes.Library.Exception
{
    /// <summary>
    ///     Contains error information thrown by prayer times calculator methods.
    /// </summary>
    public class PrayerCalculationException : System.Exception
    {
        /// <summary>
        ///     Create new <see cref="PrayerCalculationException" />.
        /// </summary>
        /// <param name="message">
        ///     Error message.
        /// </param>
        public PrayerCalculationException(string message) : base(message) { }

        public PrayerCalculationException()
        {
        }

        public PrayerCalculationException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}