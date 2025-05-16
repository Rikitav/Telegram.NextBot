namespace Telegram.NextBot.Extensions.Collections
{
    public class HandlerDataDictionary(Dictionary<string, object?>? data = null)
    {
        private readonly Dictionary<string, object?> _data = data ?? [];

        public IEnumerable<object?> Values => _data.Values;

        public IEnumerable<string> Keys => _data.Keys;

        public void SetDataValue(string key, object? value)
        {
            if (!_data.ContainsKey(key))
            {
                _data.Add(key, value);
                return;
            }

            _data[key] = value;
        }

        public D? GetDataValue<D>(string key)
        {
            if (!_data.TryGetValue(key, out object? value))
                return default(D);

            return (D?)value;
        }

        public bool ContainsKey(string key)
        {
            return _data.ContainsKey(key);
        }

        public bool ContainsValue(object value)
        {
            return _data.ContainsValue(value);
        }
    }
}
