using System;
using System.Collections.Generic;

namespace TwitterSafari.Models
{
    /// <summary>
    /// A simple service container implementation, singleton only
    /// </summary>
    public static class ServiceContainer
    {
        private static readonly Dictionary<Type, Lazy<object>> services = new Dictionary<Type, Lazy<object>>();

        /// <summary>
        /// Register the specified service with an instance
        /// </summary>
        public static void Register<T>(T service)
        {
            services[typeof(T)] = new Lazy<object>(() => service);
        }

        /// <summary>
        /// Register the specified service for a class with a default constructor
        /// </summary>
        public static void Register<T>() where T : new()
        {
            services[typeof(T)] = new Lazy<object>(() => new T());
        }

        /// <summary>
        /// Register the specified service with a callback to be invoked when requested
        /// </summary>
        public static void Register<T>(Func<T> function)
        {
            services[typeof(T)] = new Lazy<object>(() => function());
        }

        /// <summary>
        /// Register the specified service with an instance
        /// </summary>
        public static void Register(Type type, object service)
        {
            services[type] = new Lazy<object>(() => service);
        }

        /// <summary>
        /// Register the specified service with a callback to be invoked when requested
        /// </summary>
        public static void Register(Type type, Func<object> function)
        {
            services[type] = new Lazy<object>(function);
        }

        /// <summary>
        /// Resolves the type, throwing an exception if not found
        /// </summary>
        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// Resolves the type, throwing an exception if not found
        /// </summary>
        public static object Resolve(Type type)
        {
            Lazy<object> lazy;
            if (services.TryGetValue(type, out lazy))
            {
                return lazy.Value;
            }
            else
            {
                throw new KeyNotFoundException(string.Format("Service not found for type '{0}'", type));
            }
        }

        /// <summary>
        /// Resolves the type, returning a bool if found
        /// </summary>
        public static bool TryResolve<T>(out T service)
        {
            object value;
            bool success = TryResolve(typeof(T), out value);
            service = (T)value;
            return success;
        }

        /// <summary>
        /// Resolves the type, returning a bool if found
        /// </summary>
        public static bool TryResolve(Type type, out object service)
        {
            Lazy<object> lazy;
            if (services.TryGetValue(type, out lazy))
            {
                service = lazy.Value;
                return true;
            }
            else
            {
                service = null;
                return false;
            }
        }

        /// <summary>
        /// Mainly for testing, clears the entire container
        /// </summary>
        public static void Clear()
        {
            services.Clear();
        }

        /// <summary>
        /// Returns the service, or registers a new one if not found
        /// </summary>
        public static T ResolveOrRegister<T>(Func<T> function)
        {
            var type = typeof(T);
            Lazy<object> lazy;
            if (!services.TryGetValue(type, out lazy))
            {
                services[type] =
                    lazy = new Lazy<object>(() => function());
            }
            return (T)lazy.Value;
        }
    }
}