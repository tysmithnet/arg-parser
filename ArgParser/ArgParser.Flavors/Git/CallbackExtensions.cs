using System;

namespace ArgParser.Flavors.Git
{
    public static class CallbackExtensions
    {
        public static Action<object> ToBaseCallback<T>(this Action<T> callback)
        {
            return o =>
            {
                if (o is T casted)
                    callback(casted);
            };
        }

        public static Action<object, string> ToBaseCallback<T>(this Action<T, string> callback)
        {
            return (o,s) =>
            {
                if (o is T casted)
                    callback(casted, s);
            };
        }

        public static Action<object, string[]> ToBaseCallback<T>(this Action<T, string[]> callback)
        {
            return (o, s) =>
            {
                if (o is T casted)
                    callback(casted, s);
            };
        }
    }
}