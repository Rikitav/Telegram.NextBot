using System.Collections.ObjectModel;

namespace Telegram.NextBot.Extensions
{
    public static class ColletionsExtensions
    {
        public static ReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector)
        {
            Dictionary<TKey, TValue> dictionary = source.ToDictionary(keySelector);
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }

        public static IEnumerable<TValue> ForEach<TValue>(this IEnumerable<TValue> source, Action<TValue> action)
        {
            foreach (TValue value in source)
                action.Invoke(value);

            return source;
        }

        public static IEnumerable<TResult> SelectValues<TValue, TResult>(this IEnumerable<TValue> source, Func<TValue, TResult?> predicate)
        {
            foreach (TValue value in source)
            {
                TResult? gotValue = predicate.Invoke(value);
                if (gotValue != null)
                    yield return gotValue;
            }
        }
    }
}
