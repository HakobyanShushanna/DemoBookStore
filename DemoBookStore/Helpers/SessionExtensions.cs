using Newtonsoft.Json;

namespace DemoBookStore.Helpers
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value) //List<BookModel>
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? GetObject<T>(this ISession session, string key) //List<BookModel>
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}