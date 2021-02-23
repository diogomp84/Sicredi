using System;
using System.Collections.Generic;

namespace AutoSicredi.Extensions
{
    public static class DataExtensios
    {
        public static string TrimLineChars(this string text)
        {
            const string CR = @"\n";
            const string LF = @"\r";

            return text.Replace(CR, " ").Replace(LF, " ").Trim().RemoveExtraWhiteSpace();
        }
        public static string RemoveExtraWhiteSpace(this string text)
        {
            return string.Join(" ", text.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        public static List<string> ConvertToList(string series)
        {
            List<string> listStr = new List<string>();
            foreach (string serie in series.Trim().Split(','))
            {
                var str = RemoveExtraWhiteSpace(serie);

                listStr.Add(str);
            }
            return listStr;
        }
    }
}
