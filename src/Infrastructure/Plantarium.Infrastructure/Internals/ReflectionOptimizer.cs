// -----------------------------------------------------------------------
// <copyright file="ReflectionOptimizer.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Internals
{
    using System;
    using System.Reflection;

    /// <summary>
    /// The reflection optimizer.
    /// </summary>
    internal static class ReflectionOptimizer
    {
        /// <summary>
        /// Creates the weakly typed object getter delegate.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The object getter delegate.</returns>
        public static Func<object, object> CreateObjectGetterDelegate(MethodInfo method)
        {
            return (model) => method.Invoke(model, null);
        }

        /// <summary>
        /// Creates the strongly typed object getter delegate.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The object getter delegate.</returns>
        public static Func<T, object> CreateObjectGetterDelegate<T>(MethodInfo method)
        {
            return (Func<T, object>)(object)CreateObjectGetterDelegate(method);
        }

        /// <summary>
        /// Creates the weakly typed object setter delegate.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The object setter delegate.</returns>
        public static Action<object, object> CreateObjectSetterDelegate(MethodInfo method)
        {
            return (model, value) => method.Invoke(model, new object[] { value });
        }

        /// <summary>
        /// Creates the strongly typed object setter delegate.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The object setter delegate.</returns>
        public static Action<T, object> CreateObjectSetterDelegate<T>(MethodInfo method)
        {
            return (Action<T, object>)(object)CreateObjectSetterDelegate(method);
        }
    }
}
