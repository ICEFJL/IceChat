using System.Text;
using System.Security.Cryptography;

namespace icechat.api.Utils
{
    public class SHAUtil
    {
        public const string KEY_SHA = "SHA";
        public const string ALGORITHM = "SHA-256";

        /// <summary>
        /// SHA加密字符串
        /// </summary>
        /// <param name="content">要加密的字符串</param>
        /// <returns>加密后的十六进制字符串</returns>
        public static string SHAEncrypt(string content)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] sha_byte = sha1.ComputeHash(Encoding.UTF8.GetBytes(content));
                StringBuilder hexValue = new StringBuilder();
                foreach (byte b in sha_byte)
                {
                    string toHexString = b.ToString("x2");
                    hexValue.Append(toHexString);
                }
                return hexValue.ToString();
            }
        }

        /// <summary>
        /// SHA-256加密字符串
        /// </summary>
        /// <param name="sourceStr">要加密的字符串</param>
        /// <returns>加密后的十六进制字符串</returns>
        public static string SHA256Encrypt(string sourceStr)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(sourceStr));
                return GetDigestStr(hashBytes);
            }
        }

        /// <summary>
        /// 将字节数组转换为十六进制字符串
        /// </summary>
        /// <param name="origBytes">原始字节数组</param>
        /// <returns>十六进制字符串</returns>
        private static string GetDigestStr(byte[] origBytes)
        {
            StringBuilder stb = new StringBuilder();
            foreach (byte b in origBytes)
            {
                stb.Append(b.ToString("x2"));
            }
            return stb.ToString();
        }
    }
}
