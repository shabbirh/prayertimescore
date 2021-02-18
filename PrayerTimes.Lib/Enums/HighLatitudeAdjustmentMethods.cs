using System;
using System.Collections.Generic;
using System.Linq;

namespace prayertimescore.PrayerTimes.Lib.Enums
{
    public enum HighLatitudeAdjustmentMethods
    {
        None = 0,    // No adjustment
        MidNight = 1,    // middle of night
        OneSeventh = 2,    // 1/7th of night
        AngleBased = 3,    // angle/60th of night
    }
}