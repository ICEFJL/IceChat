using System.Text.RegularExpressions;

namespace icechat.api.Utils
{
    public static class RegexUtils
    {
        public static class RegexPatterns
        {
            /// <summary>
            /// 手机号正则
            /// </summary>
            public const string PHONE_REGEX = @"^1([38][0-9]|4[579]|5[0-3,5-9]|6[6]|7[0135678]|9[89])\d{8}$";

            /// <summary>
            /// 邮箱正则
            /// </summary>
            public const string EMAIL_REGEX = @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";

            /// <summary>
            /// 密码正则。4~32位的字母、数字、下划线
            /// </summary>
            public const string PASSWORD_REGEX = @"^\w{4,32}$";

            /// <summary>
            /// 验证码正则, 6位数字或字母
            /// </summary>
            public const string VERIFY_CODE_REGEX = @"^[a-zA-Z\d]{6}$";
        }
        /// <summary>
        /// 是否是无效手机格式
        /// </summary>
        /// <param name="phone">要校验的手机号</param>
        /// <returns>true:符合，false：不符合</returns>
        public static bool IsPhoneInvalid(string phone)
        {
            return IsMatch(phone, RegexPatterns.PHONE_REGEX);
        }

        /// <summary>
        /// 是否是无效邮箱格式
        /// </summary>
        /// <param name="email">要校验的邮箱</param>
        /// <returns>true:符合，false：不符合</returns>
        public static bool IsEmailInvalid(string email)
        {
            return IsMatch(email, RegexPatterns.EMAIL_REGEX);
        }

        /// <summary>
        /// 是否是无效验证码格式
        /// </summary>
        /// <param name="code">要校验的验证码</param>
        /// <returns>true:符合，false：不符合</returns>
        public static bool IsCodeInvalid(string code)
        {
            return IsMatch(code, RegexPatterns.VERIFY_CODE_REGEX);
        }

        /// <summary>
        /// 校验是否不符合正则格式
        /// </summary>
        /// <param name="str">要校验的字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>true:不符合，false：符合</returns>
        private static bool IsMatch(string str, string regex)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            return Regex.IsMatch(str, regex);
        }
    }
}
