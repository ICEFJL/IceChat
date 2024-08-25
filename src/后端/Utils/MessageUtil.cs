namespace icechat.api.Utils
{
    public static class MessageUtil
    {
        public static string GetMessage(bool isSystem, string user, IEnumerable<string> friends)
        {
            // Implement message formatting
            return $"System: {isSystem}, User: {user}, Friends: {string.Join(", ", friends)}";
        }
    }
}
