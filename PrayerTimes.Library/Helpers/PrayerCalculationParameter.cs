using System.ComponentModel.DataAnnotations;
using PrayerTimes.Library.Enumerations;

namespace PrayerTimes.Library.Helpers
{
    public class PrayerCalculationParameter
    {

        /// <summary>
        ///     Create new <see cref="PrayerCalculationParameter" />.
        /// </summary>
        public PrayerCalculationParameter(double value, PrayerCalculationParameterType type)
        {
            Value = value;
            Type = type;
        }

        /// <summary>
        ///     Gets the value of the prayer times calculation parameter.
        /// </summary>
        [Display(Name = "Calculation Value")]
        public double Value { get; internal set; }

        /// <summary>
        ///     Gets the type of the prayer times calculation parameter.
        /// </summary>
        [Display(Name = "Calculation Parameter Type")]
        public PrayerCalculationParameterType Type { get; internal set; }
    }
}