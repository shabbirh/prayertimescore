using System.ComponentModel.DataAnnotations;

namespace PrayerTimes.Library.Enumerations
{
    /// <summary>
    ///     Represents the prayer times midnight method.
    /// </summary>
    public enum Midnight
    {
        /// <summary>
        ///     Standard method.
        /// </summary>
        [Display(Name = "Standard")]
        Standard,

        /// <summary>
        ///     Jafari method.
        /// </summary>
        [Display(Name = "Jafari")]
        Jafari
    }
}