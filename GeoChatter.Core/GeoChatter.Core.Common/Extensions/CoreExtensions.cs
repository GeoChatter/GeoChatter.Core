using GeoChatter.Helpers;
using GeoChatter.Model.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
#if !DEBUG
using System.Runtime.CompilerServices;
#endif

namespace GeoChatter.Core.Common.Extensions
{
    /// <summary>
    /// Float-int byte conversions
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct FloatInt : IEquatable<FloatInt>
    {
        /// <summary>
        /// Float value
        /// </summary>
        [FieldOffset(0)] public float f;
        /// <summary>
        /// Integer value
        /// </summary>
        [FieldOffset(0)] public int i;

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is FloatInt fi && fi.f == f;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <inheritdoc/>
        public static bool operator ==(FloatInt left, FloatInt right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(FloatInt left, FloatInt right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public bool Equals(FloatInt other)
        {
            return this == other;
        }
    }

    /// <summary>
    /// Extension methods to be used across projects
    /// </summary>
    public static class CoreExtensions
    {
        /// <summary>
        /// Default culture for extension methods
        /// </summary>
        public static CultureInfo DEFAULTCULTURE { get; } = CultureInfo.InvariantCulture;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IEnumerable<T> BuildOrderBy<T>(this IEnumerable<T> source, params Tuple<string, ListSortDirection>[] properties)
            where T : ISortableModel
        {
            if (properties == null || properties.Length == 0)
            {
                return source;
            }

            Type typeOfT = typeof(T);

            Type t = typeOfT;

            IOrderedEnumerable<T> result = null;
            bool thenBy = false;

            foreach (var item in properties
                .Select(prop => new { PropertyInfo = t.GetProperty(prop.Item1), prop.Item2 }))
            {
                ParameterExpression oExpr = Expression.Parameter(typeOfT, "o");
                PropertyInfo propertyInfo = item.PropertyInfo;
                Type propertyType = propertyInfo.PropertyType;
                bool isAscending = item.Item2 == ListSortDirection.Ascending;

                if (thenBy)
                {
                    ParameterExpression prevExpr = Expression.Parameter(typeof(IOrderedEnumerable<T>), "prevExpr");
                    Func<IOrderedEnumerable<T>, IOrderedEnumerable<T>> expr1 = Expression.Lambda<Func<IOrderedEnumerable<T>, IOrderedEnumerable<T>>>(
                        Expression.Call(
                            (isAscending ? thenByMethod : thenByDescendingMethod).MakeGenericMethod(typeOfT, propertyType),
                            prevExpr,
                            Expression.Lambda(
                                typeof(Func<,>).MakeGenericType(typeOfT, propertyType),
                                Expression.MakeMemberAccess(oExpr, propertyInfo),
                                oExpr)
                            ),
                        prevExpr)
                        .Compile();

                    result = expr1(result);
                }
                else
                {
                    ParameterExpression prevExpr = Expression.Parameter(typeof(IEnumerable<T>), "prevExpr");
                    Func<IEnumerable<T>, IOrderedEnumerable<T>> expr1 = Expression.Lambda<Func<IEnumerable<T>, IOrderedEnumerable<T>>>(
                        Expression.Call(
                            (isAscending ? orderByMethod : orderByDescendingMethod).MakeGenericMethod(typeOfT, propertyType),
                            prevExpr,
                            Expression.Lambda(
                                typeof(Func<,>).MakeGenericType(typeOfT, propertyType),
                                Expression.MakeMemberAccess(oExpr, propertyInfo),
                                oExpr)
                            ),
                        prevExpr)
                        .Compile();

                    result = expr1(source);
                    thenBy = true;
                }
            }
            return result;
        }

        private static MethodInfo orderByMethod =
            MethodOf(() => Enumerable.OrderBy(default, default(Func<object, object>)))
                .GetGenericMethodDefinition();

        private static MethodInfo orderByDescendingMethod =
            MethodOf(() => Enumerable.OrderByDescending(default, default(Func<object, object>)))
                .GetGenericMethodDefinition();

        private static MethodInfo thenByMethod =
            MethodOf(() => Enumerable.ThenBy(default, default(Func<object, object>)))
                .GetGenericMethodDefinition();

        private static MethodInfo thenByDescendingMethod =
            MethodOf(() => Enumerable.ThenByDescending(default, default(Func<object, object>)))
                .GetGenericMethodDefinition();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <returns></returns>
        private static MethodInfo MethodOf<T>(Expression<Func<T>> method)
        {
            MethodCallExpression mce = (MethodCallExpression)method.Body;
            MethodInfo mi = mce.Method;
            return mi;
        }

        /// <summary>
        /// Get integer as float using its bits
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static float GetBitsAsFloat(this int i)
        {
            return new FloatInt() { i = i }.f;
        }

        /// <summary>
        /// Get float as integer using its bits
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static int GetBitsAsInt(this float f)
        {
            return new FloatInt() { f = f }.i;
        }

        /// <summary>
        /// Escape backslash and quotation marks for strings to be used in serialized JSON formatted strings
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EscapeJSON(this string str)
        {
            return str?.ReplaceDefault("\\", "\\\\")?.ReplaceDefault("\"", "\\\"");
        }

        /// Limit value of <paramref name="d"/> within integer value bounds
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int LimitAsInt(this double d)
        {
            if (double.IsNaN(d))
            {
                return 0;
            }
            else if (double.IsInfinity(d))
            {
                return int.MaxValue;
            }
            else if (double.IsNegativeInfinity(d))
            {
                return int.MinValue;
            }
            else if (d <= int.MinValue)
            {
                return int.MinValue;
            }
            else if (d >= int.MaxValue)
            {
                return int.MaxValue;
            }
            else
            {
                return Convert.ToInt32(d);
            }
        }

        /// <summary>
        /// Limit value of <paramref name="d"/> within float value bounds
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static float LimitAsFloat(this double d)
        {
            if (double.IsNaN(d))
            {
                return float.NaN;
            }
            else if (double.IsInfinity(d))
            {
                return float.PositiveInfinity;
            }
            else if (double.IsNegativeInfinity(d))
            {
                return float.NegativeInfinity;
            }
            else if (d <= float.MinValue)
            {
                return float.MinValue;
            }
            else if (d >= float.MaxValue)
            {
                return float.MaxValue;
            }
            else
            {
                return (float)d;
            }
        }

        /// <summary>
        /// Limit value of <paramref name="l"/> within integer value bounds
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public static int LimitAsInt(this long l)
        {
            if (l <= long.MinValue)
            {
                return int.MinValue;
            }
            else if (l >= long.MaxValue)
            {
                return int.MaxValue;
            }
            else
            {
                return (int)l;
            }
        }

        /// <summary>
        /// Return a detailed summary of <paramref name="exception"/> to be logged
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="maxDepth">Max depth for inner exception summaries</param>
        /// <returns></returns>
        public static string Summarize(this Exception exception, int maxDepth = 3)
        {
            if (exception == null)
            {
                return string.Empty;
            }

            string sum = $"<{exception.GetType().Name}>: {exception.Message} Trace: {exception.StackTrace}".Trim();

            int i = 1;
            while (exception.InnerException != null && i <= maxDepth)
            {
                exception = exception.InnerException;
                sum += $"\n{new string('-', 16)}\n<{exception.GetType().Name}>(depth: {i++}): {exception.Message} Trace: {exception.StackTrace}".Trim();
            }

            return sum;
        }

        /// <summary>
        /// Get a double in range [<paramref name="lowerBound"/>, <paramref name="upperBound"/>)
        /// </summary>
        /// <param name="gen"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static double GetDouble([NotNull] this Random gen, double lowerBound, double upperBound)
        {
            if (lowerBound > upperBound)
            {
                (upperBound, lowerBound) = (lowerBound, upperBound);
            }
            return gen.NextDouble() * (upperBound - lowerBound) + lowerBound;
        }

        /// <summary>
        /// Cast <see langword="int"/> to <see langword="float"/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static float CastAsFloat(this int i)
        {
            return Convert.ToSingle(i);
        }
        /// <summary>
        /// Cast <see langword="long"/> to <see langword="float"/>
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static float CastAsFloat(this long l)
        {
            return Convert.ToSingle(l);
        }

        /// <summary>
        /// Cast <see langword="int"/> to <see langword="double"/>
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static double CastAsDouble(this int i)
        {
            return Convert.ToDouble(i);
        }
        /// <summary>
        /// Cast <see langword="long"/> to <see langword="double"/>
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static double CastAsDouble(this long l)
        {
            return Convert.ToDouble(l);
        }
        /// <summary>
        /// Cast <see langword="double"/> to <see langword="float"/>
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static float CastAsFloat(this double d)
        {
            if (d <= float.MaxValue)
            {
                if (d >= float.MinValue)
                {
                    return (float)d;
                }
                return float.MinValue;
            }
            return float.MaxValue;
        }
        /// <summary>
        /// Cast <see langword="double"/> to <see langword="int"/>
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static int CastAsInt(this double d)
        {
            if (d <= int.MaxValue)
            {
                if (d >= int.MinValue)
                {
                    return Convert.ToInt32(d);
                }
                return int.MinValue;
            }
            return int.MaxValue;
        }

        /// <summary>
        /// Cast <see langword="double"/> to <see langword="long"/>
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static long CastAsLong(this double d)
        {
            if (d <= long.MaxValue)
            {
                if (d >= long.MinValue)
                {
                    return Convert.ToInt64(d);
                }
                return long.MinValue;
            }
            return long.MaxValue;
        }
        /// <summary>
        /// <see langword="true"/> if <paramref name="str"/> can be parsed as <see langword="int"/>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool CanBeInt([NotNull] this string str)
        {
            return str.TryParseIntDefault(out int _);
        }
        /// <summary>
        /// Try parsing <paramref name="str"/> to <see langword="int"/> <paramref name="result"/> with <see cref="DEFAULTCULTURE"/>. Returns <see langword="true"/> on success, otherwise <see langword="false"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="result"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool TryParseIntDefault([NotNull] this string str, out int result)
        {
            return int.TryParse(str, NumberStyles.Any, DEFAULTCULTURE, out result);
        }

        /// <summary>
        /// <see langword="true"/> if <paramref name="str"/> can be parsed as <see langword="long"/>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool CanBeLong([NotNull] this string str)
        {
            return str.TryParseLongDefault(out long _);
        }
        /// <summary>
        /// Try parsing <paramref name="str"/> to <see langword="long"/> <paramref name="result"/> with <see cref="DEFAULTCULTURE"/>. Returns <see langword="true"/> on success, otherwise <see langword="false"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="result"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool TryParseLongDefault([NotNull] this string str, out long result)
        {
            return long.TryParse(str, NumberStyles.Any, DEFAULTCULTURE, out result);
        }

        /// <summary>
        /// <see langword="true"/> if <paramref name="str"/> can be parsed as <see langword="float"/>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool CanBeFloat([NotNull] this string str)
        {
            return str.TryParseFloatDefault(out float _);
        }
        /// <summary>
        /// Try parsing <paramref name="str"/> to <see langword="float"/> <paramref name="result"/> with <see cref="DEFAULTCULTURE"/>. Returns <see langword="true"/> on success, otherwise <see langword="false"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="result"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool TryParseFloatDefault([NotNull] this string str, out float result)
        {
            return float.TryParse(str, NumberStyles.Any, DEFAULTCULTURE, out result);
        }

        /// <summary>
        /// <see langword="true"/> if <paramref name="str"/> can be parsed as <see langword="double"/>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool CanBeDouble([NotNull] this string str)
        {
            return str.TryParseDoubleDefault(out double _);
        }
        /// <summary>
        /// Try parsing <paramref name="str"/> to <see langword="double"/> <paramref name="result"/> with <see cref="DEFAULTCULTURE"/>. Returns <see langword="true"/> on success, otherwise <see langword="false"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="result"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool TryParseDoubleDefault([NotNull] this string str, out double result)
        {
            return double.TryParse(str, NumberStyles.Any, DEFAULTCULTURE, out result);
        }

        /// <summary>
        /// Parse and return <paramref name="str"/> to <see langword="int"/> safely. If unsuccesful, returns <paramref name="defaultValue"/>.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static int ParseAsInt([NotNull] this string str, int defaultValue = 0)
        {
            if (int.TryParse(str, NumberStyles.Any, DEFAULTCULTURE, out int result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// Parse and return <paramref name="str"/> to <see langword="long"/> safely. If unsuccesful, returns <paramref name="defaultValue"/>.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static long ParseAsLong([NotNull] this string str, long defaultValue = 0L)
        {
            if (long.TryParse(str, NumberStyles.Any, DEFAULTCULTURE, out long result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// Parse and return <paramref name="str"/> to <see langword="float"/> safely. If unsuccesful, returns <paramref name="defaultValue"/>.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static float ParseAsFloat([NotNull] this string str, float defaultValue = float.NaN)
        {
            if (float.TryParse(str, NumberStyles.Any, DEFAULTCULTURE, out float result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// Parse and return <paramref name="str"/> to <see langword="double"/> safely. If unsuccesful, returns <paramref name="defaultValue"/>.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static double ParseAsDouble([NotNull] this string str, double defaultValue = double.NaN)
        {
            if (double.TryParse(str, NumberStyles.Any, DEFAULTCULTURE, out double result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// Parse and return <paramref name="str"/> to <see langword="int"/> unsafely.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static int ParseAsIntUnsafe([NotNull] this string str)
        {
            return int.Parse(str, NumberStyles.Any, DEFAULTCULTURE);
        }

        /// <summary>
        /// Parse and return <paramref name="str"/> to <see langword="long"/> unsafely.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static long ParseAsLongUnsafe([NotNull] this string str)
        {
            return long.Parse(str, NumberStyles.Any, DEFAULTCULTURE);
        }

        /// <summary>
        /// Parse and return <paramref name="str"/> to <see langword="float"/> unsafely.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static float ParseAsFloatUnsafe([NotNull] this string str)
        {
            return float.Parse(str, NumberStyles.Any, DEFAULTCULTURE);
        }

        /// <summary>
        /// Parse and return <paramref name="str"/> to <see langword="double"/> unsafely.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static double ParseAsDoubleUnsafe([NotNull] this string str)
        {
            return double.Parse(str, NumberStyles.Any, DEFAULTCULTURE);
        }

        /// <summary>
        /// Get value of property named <paramref name="propertyName"/> of <paramref name="o"/> token or null.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
#nullable enable
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static JToken? GetValueDefault([NotNull] this JObject o, string? propertyName)
        {
            GCUtils.ThrowIfNullEmptyOrWhiteSpace(propertyName, nameof(propertyName));

            return o.GetValue(propertyName, StringComparison.Ordinal);
        }
#nullable restore

        /// <summary>
        /// Convert <paramref name="obj"/>'s <see cref="ToStringDefault(object, string)"/> with hexadecimal number format of given amount of digits
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static string ToHex(this object obj, int digits = 2)
        {
            GCUtils.ThrowIfNull(obj, nameof(obj));

            if (digits < 1)
            {
                digits = 1;
            }

            return obj.ToStringDefault($"X{digits}");
        }

        /// <summary>
        /// Replace given substrings <paramref name="oldValue"/> with <paramref name="newValue"/> in <paramref name="obj"/> using <see cref="StringComparison.InvariantCulture"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static string ReplaceDefault([NotNull] this string obj, string oldValue, string newValue = null)
        {
            if (oldValue == null || oldValue == newValue)
            {
                return obj;
            }

            newValue ??= string.Empty;
            return obj.Replace(oldValue, newValue, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// If <paramref name="obj"/> contains <paramref name="value"/> compared via <see cref="StringComparison.InvariantCulture"/>, returns <see langword="true"/>, otherwise <see langword="false"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool ContainsDefault([NotNull] this string obj, char value)
        {
            return obj.Contains(value, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// If <paramref name="obj"/> contains <paramref name="value"/> compared via <see cref="StringComparison.InvariantCulture"/>, returns <see langword="true"/>, otherwise <see langword="false"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool ContainsDefault([NotNull] this string obj, string value)
        {
            if (value == null)
            {
                return false;
            }

            return obj.Contains(value, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Check if <paramref name="obj"/> starts with sub-string <paramref name="value"/> using <see cref="DEFAULTCULTURE"/> and case-sensitive
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool StartsWithDefault([NotNull] this string obj, string value)
        {
            if (value == null)
            {
                return false;
            }

            return obj.StartsWith(value, false, DEFAULTCULTURE);
        }

        /// <summary>
        /// Check if <paramref name="obj"/> ends with sub-string <paramref name="value"/> using <see cref="DEFAULTCULTURE"/> and case-sensitive
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool EndsWithDefault([NotNull] this string obj, string value)
        {
            if (value == null)
            {
                return false;
            }

            return obj.EndsWith(value, false, DEFAULTCULTURE);
        }

        /// <summary>
        /// Get <paramref name="obj"/>'s string representation using <see cref="DEFAULTCULTURE"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format">Any formatter string</param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static string ToStringDefault(this object obj, string format = null)
        {
            if (obj is double d)
            {
                return d.ToStringDefault(format);
            }
            else if (obj is long l)
            {
                return l.ToStringDefault(format);
            }
            else if (obj is int i)
            {
                return i.ToStringDefault(format);
            }
            else if (obj is float f)
            {
                return f.ToStringDefault(format);
            }
            else if (obj is string s)
            {
                return s;
            }
            else if (obj is bool bo)
            {
                return bo ? "true" : "false";
            }
            else if (obj is char c)
            {
                return c.ToStringDefault();
            }
            else if (obj is byte b)
            {
                return b.ToStringDefault(format);
            }
            else if (obj is decimal dc)
            {
                return dc.ToStringDefault(format);
            }
            else if (obj is DateTime dt)
            {
                return dt.ToStringDefault(format);
            }
            else if (obj is null)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// Get string representation using <see cref="DEFAULTCULTURE"/> and string <paramref name="format"/>
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="format"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static string ToStringDefault(this decimal dc, string format = null)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                return dc.ToString(DEFAULTCULTURE);
            }
            return dc.ToString(format, DEFAULTCULTURE);
        }
        /// <summary>
        /// Get string representation using <see cref="DEFAULTCULTURE"/> and string <paramref name="format"/>
        /// </summary>
        /// <param name="b"></param>
        /// <param name="format"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static string ToStringDefault(this byte b, string format = null)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                return b.ToString(DEFAULTCULTURE);
            }
            return b.ToString(format, DEFAULTCULTURE);
        }
        
        /// <summary>
        /// Get string representation using <see cref="DEFAULTCULTURE"/> and string <paramref name="format"/>
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static string ToStringDefault(this DateTime dt, string format = null)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                return dt.ToString(DEFAULTCULTURE);
            }
            return dt.ToString(format, DEFAULTCULTURE);
        }
        /// <summary>
        /// Get string representation using <see cref="DEFAULTCULTURE"/>
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static string ToStringDefault(this char c)
        {
            return c.ToString(DEFAULTCULTURE);
        }
        /// <summary>
        /// Get string representation using <see cref="DEFAULTCULTURE"/> and string <paramref name="format"/>
        /// </summary>
        /// <param name="i"></param>
        /// <param name="format"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static string ToStringDefault(this int i, string format = null)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                return i.ToString(DEFAULTCULTURE);
            }
            return i.ToString(format, DEFAULTCULTURE);
        }
        /// <summary>
        /// Get string representation using <see cref="DEFAULTCULTURE"/> and string <paramref name="format"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="format"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static string ToStringDefault(this long str, string format = null)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                return str.ToString(DEFAULTCULTURE);
            }
            return str.ToString(format, DEFAULTCULTURE);
        }
        /// <summary>
        /// Get string representation using <see cref="DEFAULTCULTURE"/> and string <paramref name="format"/>
        /// </summary>
        /// <param name="d"></param>
        /// <param name="format"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static string ToStringDefault(this double d, string format = null)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                return d.ToString(DEFAULTCULTURE);
            }
            return d.ToString(format, DEFAULTCULTURE);
        }
        /// <summary>
        /// Get string representation using <see cref="DEFAULTCULTURE"/> and string <paramref name="format"/>
        /// </summary>
        /// <param name="f"></param>
        /// <param name="format"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static string ToStringDefault(this float f, string format = null)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                return f.ToString(DEFAULTCULTURE);
            }
            return f.ToString(format, DEFAULTCULTURE);
        }
        /// <summary>
        /// Format using <see cref="string.Format(IFormatProvider, string, object[])"/> with <see cref="DEFAULTCULTURE"/>, <paramref name="str"/>, and given <paramref name="args"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static string FormatDefault([NotNull] this string str, params string[] args)
        {
            return string.Format(DEFAULTCULTURE, str, args);
        }

        private static Random randPick { get; } = new Random();

        /// <summary>
        /// Return a random element from <paramref name="source"/>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static TSource RandomPick<TSource>([NotNull] this IEnumerable<TSource> source)
        {
            if (!source.Any())
            {
                return default;
            }
            else
            {
                return source.ElementAt(randPick.Next(source.Count()));
            }
        }

        /// <summary>
        /// LINQ style foreach
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static IEnumerable<TSource> ForEach<TSource>([NotNull] this IEnumerable<TSource> source, Action<TSource> action)
        {
            GCUtils.ThrowIfNull(action, nameof(action), "ForEach function can not be null.");

            foreach (TSource item in source)
            {
                action(item);
            }
            return source;
        }

        /// <summary>
        /// LINQ style foreach
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static IDictionary<TKey, TValue> ForEach<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> source, Action<KeyValuePair<TKey, TValue>> action)
        {
            GCUtils.ThrowIfNull(action, nameof(action), "ForEach function can not be null.");

            foreach (KeyValuePair<TKey, TValue> pair in source)
            {
                action(pair);
            }
            return source;
        }

        /// <summary>
        /// Map given enumerable with given function
        /// </summary>
        /// <typeparam name="TSource">Source type in enumerable</typeparam>
        /// <typeparam name="TResult">Result type in new mapped enumerable</typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static IEnumerable<TResult> Map<TSource, TResult>([NotNull] this IEnumerable<TSource> source, Func<TSource, TResult> func)
        {
            GCUtils.ThrowIfNull(func, nameof(func), "Mapping function can not be null.");

            IList<TResult> result = new List<TResult>();
            foreach (TSource item in source)
            {
                result.Add(func(item));
            }
            return result;
        }

        /// <summary>
        /// Try adding given key and value
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns><see langword="true"/> if key was not found and added, otherwise <see langword="false"/></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool TryAdd<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> dict, TKey key, TValue val)
        {
            if (dict.ContainsKey(key))
            {
                return false;
            }
            dict.Add(key, val);
            return true;
        }

        /// <summary>
        /// Try adding given pair 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="pair"></param>
        /// <returns><see langword="true"/> if pair is added, otherwise <see langword="false"/></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static bool TryAdd<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> dict, KeyValuePair<TKey, TValue> pair)
        {
            if (dict.ContainsKey(pair.Key))
            {
                return false;
            }
            dict.Add(pair);
            return true;
        }

        /// <summary>
        /// Create a dictionary from given pairs. Repeated keys are ignored
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="keyValuePairs"></param>
        /// <param name="keycomparer"></param>
        /// <returns></returns>
#if !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
#endif
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs, IEqualityComparer<TKey> keycomparer = null)
        {
            IDictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>(keycomparer);
            keyValuePairs.ForEach(pair => dict.TryAdd(pair));
            return dict;
        }

        /// <summary>
        /// Match <paramref name="method"/> name as an event handler method name: [\w\d_]+_{EventName} and return EventName in the match
        /// <para>If name doesn't match, <paramref name="method"/><c>.Name</c> is returned</para>
        /// <para>Example matching name for event OnMyEvent: <c>SomeInstance_OnMyEvent</c></para>
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string GetEventNameFromName([NotNull] this MethodInfo method)
        {
            string[] splt = method.Name.Split('_');

            if (splt.Length == 1)
            {
                return method.Name;
            }

            return string.Join("_", splt, 1, splt.Length - 1);
        }

        /// <summary>
        /// Return <paramref name="deleg"/> in an array for keeping things clean while invoking methods by reflection
        /// </summary>
        /// <param name="deleg"></param>
        /// <returns></returns>
        public static object[] ToSingleArgumentArray([NotNull] this Delegate deleg)
        {
            return new object[] { deleg };
        }
    }
}
