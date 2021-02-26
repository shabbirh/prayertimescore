using System;
using System.Collections.Generic;
using System.Linq;

namespace prayertimescore.PrayerTimes.Library.Calender
{
    public sealed class LunarDate
    {
        public int[] ArrayType { set; get; }
        public int DayOfWeek { set; get; }

        public bool IsLeapYear => new System.Globalization.HijriCalendar().IsLeapYear(ArrayType[0]);

        private readonly string[] _lunarMonths = { "محرم", "صفر", "ربیع الاول", "ربیع الثانی", "جمادی الاولی", "جمادی الثانیة", "رجب", "شعبان", "رمضان", "شوال", "ذی القعده", "ذی الحجّه" };

        private readonly string[] _lunarMonthsEnglish = { "Muharram", "Safar", "Rabi al-Awwal", "Rabi ath-Thani", "Jamadi al-Ula", "Jamadi ath-Thani", "Rajab", "Shabaan", "Shahr Ramadhan", "Shawwal", "Dhul Qada", "Dhul Hijjah" };
        private readonly string[] _lunarWeekDays = { "السبت", "الاحد", "الاثنین", "الثلاثا", "الاربعا", "الخمیس", "الجمعه" };
        private readonly string[] _weekDays = { "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه" };

        public new string ToString()
        {
            var str1 = ArrayType[1].ToString();
            var str2 = ArrayType[2].ToString();
            if (ArrayType[1] < 10)
                str1 = ArrayType[1].ToString().Insert(0, "0");
            if (ArrayType[2] < 10)
                str2 = ArrayType[2].ToString().Insert(0, "0");

            return $"{ArrayType[0]}/{str1}/{str2}";
        }

        public string ToString(string dateFormatSpecifier)
        {
            string dateFormat = dateFormatSpecifier switch
            {
                "english_day" => ArrayType[2].ToString(),
                "english_month" => _lunarMonthsEnglish[ArrayType[1] - 1],
                "english_year" => ArrayType[0].ToString(),
                "english_formatted" => $"{ArrayType[2]} {_lunarMonthsEnglish[ArrayType[1] - 1]} {ArrayType[0]}",
                "M" => ConvertToArabic(ArrayType[2]) + " " + _lunarMonths[ArrayType[1] - 1] + " " +
                                                ConvertToArabic(ArrayType[0]),
                "D" => _weekDays[DayOfWeek] + " " + ConvertToArabic(ArrayType[2]) + " " +
_lunarMonths[ArrayType[1] - 1] + " " + ConvertToArabic(ArrayType[0]),
                "N" => _lunarWeekDays[DayOfWeek] + " " + ConvertToArabic(ArrayType[2]) + " " +
_lunarMonths[ArrayType[1] - 1] + " " + ConvertToArabic(ArrayType[0]),
                "H" => $"{ConvertToArabic(ArrayType[0])} / {ConvertToArabic(ArrayType[1])} / {ConvertToArabic(ArrayType[2])}",
                _ => $"{ConvertToArabic(ArrayType[0])} / {ConvertToArabic(ArrayType[1])} / {ConvertToArabic(ArrayType[2])}",
            };
            return dateFormat;
        }

        private static string ConvertToArabic(int number)
        {
            var num = number.ToString();
            var strOut = string.Empty;
            var nLen = num.Length;
            if (nLen == 0)
                return num;
            for (var i = 0; i < nLen; i++)
            {
                var ch = num[i];
                if ((48 <= ch) && (ch <= 57))
                {
                    ch = (char)(ch + 1584);
                }
                strOut += ch;
            }
            return strOut; //1632
        }
    }
}