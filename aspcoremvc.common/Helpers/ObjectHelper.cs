using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Helpers
{
    public static class ObjectHelper
    {
        // Bu method mevcut custom nesne kopyasını oluşturarak aynı türden yeni bir object yaratır.
        public static object CloneObject(this object o)
        {
            var t = o.GetType();
            var properties = t.GetProperties();
            var p = t.InvokeMember("", BindingFlags.CreateInstance, null, o, null);

            foreach (var pi in properties.Where(pi => pi.CanWrite))
            {
                pi.SetValue(p, pi.GetValue(o, null), null);
            }

            return p;
        }

        public static T CloneObjectV2<T>(this object o)
            where T : class
        {
            Type t = o.GetType();
            PropertyInfo[] properties = t.GetProperties();
            Object p = t.InvokeMember("", BindingFlags.CreateInstance, null, o, null);

            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanWrite)
                {
                    pi.SetValue(p, pi.GetValue(o, null), null);
                }
            }

            return p as T;
        }

        public static string JsonEncode<T>(this T source)
        {
            return source.JsonEncode<T>(true);
        }

        public static string JsonEncode<T>(this T source, bool useRef)
        {
            var serialized = useRef
                ? JsonConvert.SerializeObject(source, Formatting.Indented, new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    })
                : JsonConvert.SerializeObject(source, Formatting.None);

            //var serialized = JsonConvert.SerializeObject(source, Formatting.None);
            return serialized;
        }

        public static T JsonDecode<T>(this string encoded)
        {
            return JsonConvert.DeserializeObject<T>(encoded);
        }

        public static object JsonDecode(this string encoded)
        {
            return JsonConvert.DeserializeObject(encoded);
        }

        public static T CloneObj<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static decimal Round100(this decimal source)
        {
            var round = Math.Round(source / 100) * 100;
            return round;
        }

        public static decimal Round100(this int source)
        {
            return source.Round100();
        }

        public static string FullDateTR(this DateTime date)
        {
            return date.ToString("yyyMMdd HHH:mm:ss");
        }

        public static string DateTr(this DateTime date)
        {
            return date.ToString("yyyMMdd");
        }
      
        public static string DateForLinq(this DateTime date)
        {
            return $"DateTime({date.ToString("yyy,MM,dd")})";
        }

        public static string PrepareFuzzy(this string str)
        {
            return str.ToLowerInvariant()
                .Replace("/", " ")
                .Replace("\\", " ")
                .Replace("-", " ")
                .Replace(".", " ")
                .Replace("serisi", " ")
                .Replace("series", " ")
                .Trim();
        }

        public static string FixSql(this string str)
        {
            return str.Replace("'", "''");
        }

        public static string EmptyIfNull(this string str)
        {
            return str ?? "";
        }

        public static int AsInt32(this bool item)
        {
            return item ? 1 : 0;
        }

        public static string TypeName(this object obj)
        {
            var type = obj.GetType();
            if (type.DeclaringType == null)
                return type.Name;

            return TypeName(type.DeclaringType) + "." + type.Name;
        }

        public static string Capitalize(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var newstr = string.Empty;
            var words = str.Split(" ");

            foreach (var word in words)
            {
                if (string.IsNullOrEmpty(word))
                {
                    continue;
                }
                newstr += char.ToUpper(word[0]) + word.Substring(1).ToLower() + " ";
            }

                

            newstr = newstr.Substring(0, newstr.Length - 1);
            return newstr;
        }

        public static string CapitalizeInvariant(this string str)
        {        
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var newstr = string.Empty;
            var words = str.Split(" ");

            foreach (var word in words)
            {

                if (string.IsNullOrEmpty(word))
                {
                    continue;
                }
                newstr += char.ToUpperInvariant(word[0]) + word.Substring(1).ToLowerInvariant() + " ";
            }

           
            newstr = newstr.Substring(0, newstr.Length - 1);
            return newstr;

        }

        public static bool IsPrimitiveType(Type type)
        {
            var types = new List<Type>
            {
                typeof(bool),
                typeof(bool?),
                typeof(short),
                typeof(short?),
                typeof(int),
                typeof(int?),
                typeof(long),
                typeof(long?),
                typeof(decimal),
                typeof(decimal?),
                typeof(float),
                typeof(float?),
                typeof(double),
                typeof(double?),
                typeof(DateTime),
                typeof(DateTime?),
                typeof(string)
            };

            return types.Contains(type);
        }
    }
}