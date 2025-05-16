namespace Telegram.NextBot.PollingManagement.Attributes
{
    [Flags]
    public enum FilterModifier
    {
        None = 1,
        OrNext = 2,
        Inverse = 4,
    }
}
