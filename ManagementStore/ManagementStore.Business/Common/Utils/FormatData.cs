using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Business.Common.Utils
{
    public static class FormatData
    {
        public static string FormatUserName(string UserName)
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                return "#" + UserName.Trim().ToLower() + "#";
            }
            else return "";

        }
        public static string RemoveFormatUserName(string UserName)
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                return UserName = UserName.Substring(1, UserName.Length - 2);

            }
            else return "";

        }
        public static List<string> FormatStringToList(string StringToList)
        {
            List<string> ListFormat = StringToList.Split(new char[] { ',', ';' }).ToList();

            List<string> RList = ListFormat.Distinct().ToList();

            return RList;
        }

        public static List<string> FormatStringToListNodistinct(string StringToList)
        {
            List<string> ListFormat = StringToList.Split(new char[] { ',', ';' }).ToList();

            //List<string> RList = ListFormat.Distinct().ToList();

            return ListFormat;
        }

        public static string FormatListToString(List<string> List)
        {
            List<string> TList = List.Distinct().ToList();

            var t = string.Join(",", TList);

            return t;
        }
        public static string FormatListToStringNoDistinct(List<string> List)
        {
            //List<string> TList = List.Distinct().ToList();

            var t = string.Join(",", List);

            return t;
        }

        public static List<Tkey> Distinct<Tkey>(List<Tkey> TList)
        {
            List<Tkey> RList = TList.Distinct<Tkey>().ToList();

            return RList;
        }

        public static List<string> Distinct(List<string> TList)
        {
            var RList = TList.Distinct().ToList();

            return RList;
        }

        public static List<string> AddRangeDistinct(List<string> TSource, List<string> TDestination)
        {
            List<string> TList = TDestination.Concat(TSource).ToList();

            TList = TList.Distinct().ToList();

            return TList;

        }

        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {


            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;


        }
        public static List<DateTime> EachMonth(DateTime start, DateTime end)
        {
            List<DateTime> TList = new List<DateTime>();
            TList.Add(start);
            int StartDay = start.Day;
            if (start > end)
            {
                return TList;
            }
            else
            {
                while (start <= end)
                {
                    start = start.AddMonths(1);
                    int StartYear = start.Year;
                    int StartMonth = start.Month;
                    try
                    {
                        DateTime i = new DateTime(StartYear, StartMonth, StartDay);
                        if (i != null)
                        {
                            TList.Add(i);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return TList;
            }

        }

        public static List<DateTime> EachYear(DateTime start, DateTime end)
        {
            List<DateTime> TList = new List<DateTime>();
            int StartYear = start.Year;
            int StartMonth = start.Month;
            int StartDay = start.Day;

            int EndYear = end.Year;

            if (start > end)
            {
                return TList;
            }
            else
            {
                for (int i = StartYear; i <= EndYear; i++)
                {
                    try
                    {
                        DateTime temp = new DateTime(i, StartMonth, StartDay);
                        if (temp != null)
                        {
                            TList.Add(temp);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return TList;
            }
        }



        //public static IEnumerable<DateTime> EachDayOfWeek(DateTime from, DateTime thru)
        //{
        //    IEnumerable<DateTime> t = EachDay(from, thru);
        //    int diff = from.DayOfWeek;


        //}
        public static string RemoveUnicode(string text)
        {
            text = text.ToLower();
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",
            " - ", "- ", " -", " ", "/", "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "{", "}", "\\", "|", ":", "'", "<", ">", ",", ".", "?"};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",
            "-","-","-","-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-"};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }
    }
}
