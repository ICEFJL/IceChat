using System.Text;

namespace icechat.api.Utils
{
    public class RandomUtil
    {
        private static readonly Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string RandomString(int length)
        {
            StringBuilder stringBuilder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }
            return stringBuilder.ToString();
        }

        public static string RandomNumbers(int length)
        {
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                sb.Append(random.Next(10)); // 生成0到9之间的随机数
            }
            return sb.ToString();
        }
    }
}
