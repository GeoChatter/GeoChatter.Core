using GeoChatter.Core.Common.Extensions;
using GeoChatter.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeoChatter.Helpers
{
    /// <summary>
    /// Utility methods for event handlers
    /// </summary>
    public static class AttributeDiscovery
    {
        private static T GetEventHandlerAttribute<T>(MethodInfo method)
        {
            return (T)method
                .GetCustomAttributes(false)
                .FirstOrDefault(attr => attr.GetType().IsAssignableFrom(typeof(T)));
        }

        /// <summary>
        /// Return list of methods with which contain <typeparamref name="T"/> attribute alongside attributes themselves with matching list indices
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="type">Type to search for methods</param>
        /// <param name="attributes">Attribute list instance to return</param>
        /// <param name="searchStatic">Wheter methods searched are static</param>
        /// <returns></returns>
        public static List<MethodInfo> GetMethodsWithAttribute<T>(Type type, out List<T> attributes, bool searchStatic = false)
        {
            if (type == null)
            {
                attributes = new List<T>();
                return new List<MethodInfo>();
            }

            List<MethodInfo> methods;
            BindingFlags flags = BindingFlags.NonPublic;
            if (!searchStatic)
            {
                flags |= BindingFlags.Instance;
            }
            else
            {
                flags |= BindingFlags.Static;
            }

            methods = type.GetMethods(flags)
                .Where(method => GetEventHandlerAttribute<T>(method) != null)
                .ToList();

            attributes = methods
                .Select(method => GetEventHandlerAttribute<T>(method))
                .ToList();


            return methods;
        }

        private static object[] EventHandlerMethod(Type handlerType, object methodSource, MethodInfo method)
        {
            return Delegate.CreateDelegate(handlerType, methodSource, method, true).ToSingleArgumentArray();
        }

        /// <summary>
        /// Adds event handlers found in <paramref name="fromMethodSource"/> instance and adds them to their respective events defined for <paramref name="toTargetInstance"/> instance
        /// <para>Methods must be <see langword="private"/> and non-<see langword="static"/></para>
        /// </summary>
        /// <param name="fromMethodSource">Instance to search for events in</param>
        /// <param name="toTargetInstance">Instance to add events to</param>
        /// <param name="searchStatic">TODO: FIX, IT DOES NOT WORK. Wheter to search for static methods instead of instance ones</param>
        /// <exception cref="ArgumentNullException"><paramref name="fromMethodSource"/> or <paramref name="toTargetInstance"/> was null</exception>
        public static void AddEventHandlers(object fromMethodSource, object toTargetInstance, bool searchStatic = false)
        {
            GCUtils.ThrowIfNull(fromMethodSource, nameof(fromMethodSource));
            GCUtils.ThrowIfNull(toTargetInstance, nameof(toTargetInstance));

            List<MethodInfo> methods = GetMethodsWithAttribute(fromMethodSource.GetType(), out List<DiscoverableEventAttribute> attributes, searchStatic);

            Type targetType = toTargetInstance.GetType();

            int index = 0;

            attributes.ForEach(attr =>
            {
                // Get event name from method
                if (string.IsNullOrWhiteSpace(attr.EventName))
                {
                    attr.EventName = methods[index].GetEventNameFromName();
                }
                index++;
            });

            for (int i = 0; i < methods.Count; i++)
            {
                EventInfo eventInfo = targetType.GetEvent(attributes[i].EventName);
                if (eventInfo != null)
                {
                    eventInfo.GetAddMethod().Invoke(toTargetInstance, EventHandlerMethod(eventInfo.EventHandlerType, fromMethodSource, methods[i]));
                }
            }
        }


        /// <summary>
        /// Removes event handlers found in <paramref name="fromMethodSource"/> instance and removes them from their respective events defined for <paramref name="toTargetInstance"/> instance
        /// <para>Methods must be <see langword="private"/> and non-<see langword="static"/></para>
        /// </summary>
        /// <param name="fromMethodSource">Instance to search for events in</param>
        /// <param name="toTargetInstance">Instance to remove events from</param>
        /// <param name="nonStatic">Wheter to search for static methods instead of instance ones</param>
        /// <exception cref="ArgumentNullException"><paramref name="fromMethodSource"/> or <paramref name="toTargetInstance"/> was null</exception>
        public static void RemoveEventHandlers(object fromMethodSource, object toTargetInstance, bool nonStatic = false)
        {
            GCUtils.ThrowIfNull(fromMethodSource, nameof(fromMethodSource));
            GCUtils.ThrowIfNull(toTargetInstance, nameof(toTargetInstance));

            List<MethodInfo> methods = GetMethodsWithAttribute(fromMethodSource.GetType(), out List<DiscoverableEventAttribute> attributes);

            Type targetType = toTargetInstance.GetType();

            int index = 0;

            attributes.ForEach(attr =>
            {
                // Get event name from method
                if (string.IsNullOrWhiteSpace(attr.EventName))
                {
                    attr.EventName = methods[index].GetEventNameFromName();
                }
                index++;
            });

            for (int i = 0; i < methods.Count; i++)
            {
                EventInfo eventInfo = targetType.GetEvent(attributes[i].EventName);
                if (eventInfo != null)
                {
                    eventInfo.GetRemoveMethod().Invoke(toTargetInstance, EventHandlerMethod(eventInfo.EventHandlerType, fromMethodSource, methods[i]));
                }
            }
        }
    }
}
