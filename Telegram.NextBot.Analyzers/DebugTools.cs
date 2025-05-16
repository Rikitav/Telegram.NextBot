using System.Diagnostics;

namespace Telegram.NextBot.Analyzers
{
    internal static class DebugTools
    {
        [DebuggerStepThrough]
        public static void AttachStudioDebugger()
        {
#if DEBUG
            if (!Debugger.IsAttached)
                Debugger.Launch();
#endif
        }
    }
}
