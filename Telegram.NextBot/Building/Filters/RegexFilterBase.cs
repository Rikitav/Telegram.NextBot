using System.Text.RegularExpressions;
using Telegram.NextBot.PollingManagement.Filters;

namespace Telegram.NextBot.Building.Filters
{
    public abstract class RegexFilterBase<T> : IFilter<T> where T : class
    {
        private readonly Func<T, string?> getString;
        private readonly Regex regex;

        public MatchCollection? Matches { get; private set; }
        public Dictionary<string, object> Data { get; } = [];

        protected RegexFilterBase(Func<T, string?> getString, Regex regex)
        {
            this.getString = getString;
            this.regex = regex;
        }

        protected RegexFilterBase(Func<T, string?> getString, string pattern, RegexOptions regexOptions = default, TimeSpan matchTimeout = default)
        {
            this.getString = getString;
            regex = new Regex(pattern, regexOptions, matchTimeout);
        }

        public bool CanPass(FilterExecutionContext<T> context)
        {
            string? text = getString.Invoke(context.Input);

            /*
            if (string.IsNullOrEmpty(text))
                return false;
            */

            Matches = regex.Matches(text);
            return Matches.Count > 0;
        }
    }
}
