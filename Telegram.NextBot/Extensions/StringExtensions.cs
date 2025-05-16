namespace Telegram.NextBot.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SliceBy(this string source, int length)
        {
            for (int start = 0; start < source.Length; start += length)
            {
                int tillEnd = source.Length - start;
                int toSlice = tillEnd < length ? tillEnd : length;

                ReadOnlySpan<char> chunk = source.AsSpan().Slice(start, toSlice);
                yield return chunk.ToString();
            }
        }
    }
}
