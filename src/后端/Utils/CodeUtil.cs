namespace icechat.api.Utils
{
    public static class CodeUtil
    {
        private static readonly Dictionary<string, string> _codeLocal = new Dictionary<string, string>();


        public static string Get(string mail)
        {
            return _codeLocal[mail];
        }


        public static void Set(string mail,string code)
        {
            if (!_codeLocal.ContainsKey(mail))

            {

                _codeLocal.Add(mail, code);

            }

            else

            {

                _codeLocal[mail] = code;

            }
        }

        public static void Remove(string mail)
        {
            _codeLocal.Remove(mail);
        }
    }
}
