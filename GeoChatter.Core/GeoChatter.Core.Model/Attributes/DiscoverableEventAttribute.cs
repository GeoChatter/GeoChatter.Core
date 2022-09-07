using System;

namespace GeoChatter.Model.Attributes
{
    /// <summary>
    /// Method with this attribute will be marked to be added as a handler to the event with the name <see cref="EventName"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class DiscoverableEventAttribute : Attribute
    {
        /// <summary>
        /// Event name to use
        /// <para>If not provided, method this attribute is added is used for name checks</para>
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// This constructor assumes you named the method {InstanceName}_On{<see cref="EventName"/>}
        /// </summary>
        public DiscoverableEventAttribute()
        {
            EventName = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventName"></param>
        public DiscoverableEventAttribute(string eventName)
        {
            EventName = eventName;
        }
    }
}
