using System.Text.RegularExpressions;

namespace icechat.api.Utils
{
    public class NumberUtils
    {
        public static long ParseToLongOrDefault(string str)
        {
            if (string.IsNullOrEmpty(str) || !Regex.IsMatch(str, @"^\d+$"))
            {
                return -1;
            }

            if (long.TryParse(str, out long result))
            {
                return result;
            }
            else
            {
                return -1;
            }
        }
    }
}
