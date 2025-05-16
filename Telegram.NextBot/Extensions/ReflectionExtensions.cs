using System.Reflection;
using System.Text;
using Telegram.NextBot.PollingManagement.Handlers;

namespace Telegram.NextBot.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool IsHandlerType(this Type type)
            => typeof(PollingHandlerBase).IsAssignableFrom(type);

        public static bool HasParameterlessCtor(this Type type)
            => type.GetConstructors().Any(ctor => ctor.GetParameters().Length == 0);

        public static object InvokeGenericMethod(this MethodInfo methodInfo, object? obj, Type[] genericParameters, object[] parameters)
            => methodInfo.MakeGenericMethod(genericParameters).Invoke(obj, parameters);

        public static T InvokeGenericMethod<T>(this object obj, string methodName, Type[] genericParameters, object[] parameters)
            => (T)obj.GetType().GetMethod(methodName).InvokeGenericMethod(obj, genericParameters, parameters);

        public static IEnumerable<PropertyInfo> EnumerateObjectProperties(this object obj)
            => obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
    }
}
