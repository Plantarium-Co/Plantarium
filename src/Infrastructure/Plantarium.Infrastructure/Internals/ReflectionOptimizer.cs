// -----------------------------------------------------------------------
// <copyright file="ReflectionOptimizer.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Internals
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// The reflection optimizer.
    /// </summary>
    internal static class ReflectionOptimizer
    {
        /// <summary>
        /// Caches the property setters.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="properties">The properties.</param>
        /// <returns>The object property setters cache.</returns>
        public static Dictionary<string, Action<T, object>> CachePropertySetters<T>(IEnumerable<string> properties) where T : class, new()
        {
            var propertySettersCache = new Dictionary<string, Action<T, object>>();

            foreach (var property in properties)
            {
                var methodInfo = typeof(T).GetProperty(property)?.GetSetMethod();

                if (methodInfo != null)
                {
                    var setter = CreateObjectSetterDelegate<T>(methodInfo);
                    propertySettersCache.Add(property, setter);
                }
            }

            return propertySettersCache;
        }

        /// <summary>
        /// Creates the object setter delegate.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The object setter delegate.</returns>
        public static Action<T, object> CreateObjectSetterDelegate<T>(MethodInfo method)
        {
            MethodInfo openImpl = typeof(ReflectionOptimizer).GetMethod(nameof(CreateObjectSetterDelegateImpl));
            MethodInfo closedImpl = openImpl.MakeGenericMethod(typeof(T), method.GetParameters()[0].ParameterType);
            return (Action<T, object>)closedImpl.Invoke(null, new object[] { method });
        }

        /// <summary>
        /// Creates the object setter delegate implementation.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TParam">The type of the parameter.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The casted object setter delegate.</returns>
        public static Action<TSource, object> CreateObjectSetterDelegateImpl<TSource, TParam>(MethodInfo method)
        {
            object tdelegate = Delegate.CreateDelegate(typeof(Action<TSource, TParam>), null, method);
            Action<TSource, TParam> call = (Action<TSource, TParam>)tdelegate;
            return delegate(TSource source, object parameter) { call(source, (TParam)parameter); };
        }
    }
}
