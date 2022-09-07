namespace GeoChatter.Model.Delegates
{
    /// <summary>
    /// General purpose delegates
    /// </summary>
    public static class GeneralDelegates
    {
        /// <summary>
        /// Get given object as a double
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public delegate bool GetAsDouble(object obj, out double value);
    }
}
