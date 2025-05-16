using System.Reflection;
using Telegram.Bot.Types;
using Telegram.NextBot.Extensions;
using Telegram.NextBot.Extensions.Collections;
using Telegram.NextBot.Hosting.DefaultServices;
using Telegram.NextBot.PollingManagement.Attributes;

namespace Telegram.NextBot.PollingManagement.Handlers
{
    public abstract class BranchingHandler<T> : PollingHandlerBase where T : class
    {
        public BranchingHandler()
        {
            if (!Update.AllTypes.IsUpdateObjectAllowed<T>())
                throw new Exception();
        }

        internal static void GetBranches(Type branchingHandlerType)
        {
            IEnumerable<Tuple<MethodInfo, IEnumerable<PollingFilterAttributeBase>>> methods = branchingHandlerType
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(method => Tuple(method, method.GetCustomAttributes<PollingFilterAttributeBase>()))
                .Where(tuple => tuple.Item2.Any());

            if (!methods.Any())
                throw new Exception();


        }

        internal sealed override IHandlerContainer CreateContainer(UpdateHandlerSession session, HandlerDataDictionary data)
        {
            return new BranchingHandlerContainer();
        }

        internal sealed override Task ExecuteInternal(IHandlerContainer container, CancellationToken cancellationToken)
        {
            // TODO: Branches logic
            return Task.CompletedTask;
        }

        private static Tuple<T1, T2> Tuple<T1, T2>(T1 item1, T2 item2) => new Tuple<T1, T2>(item1, item2);
    }
}
