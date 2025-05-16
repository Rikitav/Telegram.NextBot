using System.Reflection;
using System.Runtime.CompilerServices;

namespace Telegram.NextBot.Extensions
{
    public static class CastExtensions
    {
        private const string ImplicitCastMethodName = "op_Implicit";
        private const string ExplicitCastMethodName = "op_Explicit";

        public static bool CanCast<T>(this Type baseType)
            => GetInnerCastMethods(baseType, typeof(T), ImplicitCastMethodName).Any() || GetInnerCastMethods(baseType, typeof(T), ExplicitCastMethodName).Any();

        public static bool CanCast<T>(this object obj)
            => obj.GetType().CanCast<T>();

        public static T Cast<T>(this object obj)
        {
            try
            {
                return (T)obj;
            }
            catch (InvalidCastException exc)
            {
                if (GetInnerCastMethods(obj.GetType(), typeof(T), ImplicitCastMethodName).Any())
                    return obj.Cast<T>(ImplicitCastMethodName);

                if (GetInnerCastMethods(obj.GetType(), typeof(T), ExplicitCastMethodName).Any())
                    return obj.Cast<T>(ExplicitCastMethodName);

                throw exc;
            }
        }

        private static T Cast<T>(this object obj, string castMethodName)
        {
            MethodInfo methodInfo = GetInnerCastMethods(obj.GetType(), typeof(T), castMethodName).SingleOrDefault()
                ?? throw new InvalidCastException(string.Format("No method to cast {0} to {1}", obj.GetType().FullName, typeof(T).FullName));

            return (T)methodInfo.Invoke(null, [obj]);
        }

        private static IEnumerable<MethodInfo> GetInnerCastMethods(Type objectType, Type targetCastType, string castMethodName)
        {
            return objectType.GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(mi => mi.Name == castMethodName && mi.ReturnType == targetCastType)
                .Where(mi =>
                {
                    ParameterInfo paramInfo = mi.GetParameters().FirstOrDefault();
                    return paramInfo != null && paramInfo.ParameterType == objectType;
                });
        }
    }
}
