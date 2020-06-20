// -----------------------------------------------------------------------
// <copyright file="ReflectionUtilities.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Internals
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using static Plantarium.Infrastructure.Internals.ReflectionOptimizer;

    /// <summary>
    /// The reflection utilities.
    /// </summary>
    internal static class ReflectionUtilities
    {
        /// <summary>
        /// The default binding flags.
        /// </summary>
        private static readonly BindingFlags DefaultFlags = BindingFlags.Public | BindingFlags.Instance;

        /// <summary>
        /// The ignore case binding flags.
        /// </summary>
        private static readonly BindingFlags IgnoreCaseFlags = DefaultFlags | BindingFlags.IgnoreCase;

        /// <summary>
        /// Caches the strongly typed property getters.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The object property getters cache.</returns>
        public static Dictionary<string, Func<T, object>> CachePropertyGetters<T>()
        {
            var propertyGettersCache = new Dictionary<string, Func<T, object>>();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var methodInfo = property?.GetGetMethod();

                if (methodInfo != null)
                {
                    var getter = CreateObjectGetterDelegate<T>(methodInfo);
                    propertyGettersCache.Add(property.Name, getter);
                }
            }

            return propertyGettersCache;
        }

        /// <summary>
        /// Caches the weakly typed property getters.
        /// </summary>
        /// <param name="model">The object model.</param>
        /// <returns>The object property getters cache.</returns>
        public static Dictionary<string, Func<object, object>> CachePropertyGetters(object model)
        {
            var propertyGettersCache = new Dictionary<string, Func<object, object>>();
            var properties = model.GetType().GetProperties();

            foreach (var property in properties)
            {
                var methodInfo = property?.GetGetMethod();

                if (methodInfo != null)
                {
                    var getter = CreateObjectGetterDelegate(methodInfo);
                    propertyGettersCache.Add(property.Name, getter);
                }
            }

            return propertyGettersCache;
        }

        /// <summary>
        /// Caches the strongly typed property getters against a list of required property names.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="propertyNames">The property names.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns>The object property getters cache.</returns>
        public static Dictionary<string, Func<T, object>> CachePropertyGetters<T>(IEnumerable<string> propertyNames, bool ignoreCase = false)
        {
            var propertyGettersCache = new Dictionary<string, Func<T, object>>();
            var flags = ignoreCase ? IgnoreCaseFlags : DefaultFlags;

            foreach (var propertyName in propertyNames)
            {
                var methodInfo = typeof(T).GetProperty(propertyName, flags)?.GetGetMethod();

                if (methodInfo != null)
                {
                    var getter = CreateObjectGetterDelegate<T>(methodInfo);
                    propertyGettersCache.Add(propertyName, getter);
                }
            }

            return propertyGettersCache;
        }

        /// <summary>
        /// Caches the strongly typed property setters.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The object property setters cache.</returns>
        public static Dictionary<string, Action<T, object>> CachePropertySetters<T>()
        {
            var propertySettersCache = new Dictionary<string, Action<T, object>>();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var methodInfo = property?.GetSetMethod();

                if (methodInfo != null)
                {
                    var setter = CreateObjectSetterDelegate<T>(methodInfo);
                    propertySettersCache.Add(property.Name, setter);
                }
            }

            return propertySettersCache;
        }

        /// <summary>
        /// Caches the weakly typed property setters.
        /// </summary>
        /// <param name="model">The object model.</param>
        /// <returns>The object property setters cache.</returns>
        public static Dictionary<string, Action<object, object>> CachePropertySetters(object model)
        {
            var propertySettersCache = new Dictionary<string, Action<object, object>>();
            var properties = model.GetType().GetProperties();

            foreach (var property in properties)
            {
                var methodInfo = property?.GetSetMethod();

                if (methodInfo != null)
                {
                    var setter = CreateObjectSetterDelegate(methodInfo);
                    propertySettersCache.Add(property.Name, setter);
                }
            }

            return propertySettersCache;
        }

        /// <summary>
        /// Caches the strongly typed property setters against a list of required property names.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="propertyNames">The property names.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns>The object property setters cache.</returns>
        public static Dictionary<string, Action<T, object>> CachePropertySetters<T>(IEnumerable<string> propertyNames, bool ignoreCase = false)
        {
            var propertySettersCache = new Dictionary<string, Action<T, object>>();
            var flags = ignoreCase ? IgnoreCaseFlags : DefaultFlags;

            foreach (var propertyName in propertyNames)
            {
                var methodInfo = typeof(T).GetProperty(propertyName, flags)?.GetSetMethod();

                if (methodInfo != null)
                {
                    var setter = CreateObjectSetterDelegate<T>(methodInfo);
                    propertySettersCache.Add(propertyName, setter);
                }
            }

            return propertySettersCache;
        }
    }
}
