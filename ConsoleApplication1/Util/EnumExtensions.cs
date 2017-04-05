using ConsoleApplication5.Sorting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ConsoleApplication5.Util
{
    public class EnumDescription<T>
    {
        public T Value { get; set; }
        public string Description { get; set; }
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = GetAttributeField<DescriptionAttribute>(value);
            return (field == null) ? value.ToString() : field.Description;
        }

        public static IEnumerable<EnumDescription<T>> GetEnumValuesDescriptions<T>()
        {
            return GetEnumValuesDescriptions<T, T>(t => t);
        }

        public static IEnumerable<EnumDescription<V>> GetEnumValuesDescriptions<T, V>(Func<T, V> value)
        {
            return from val in Enum.GetValues(typeof(T)).Cast<T>()
                   let description = ((Enum)(object)val).GetDescription()
                   select new EnumDescription<V>
                   {
                       Value = value(val),
                       Description = description
                   };
        }

        public static T GetAttributeField<T>(this Enum value)
            where T : System.Attribute
        {
            return GetAttributeField<T>(value.GetType().GetField(value.ToString()));
        }

        public static IAttribute Input(this Sensor i)
        {
            return i.GetAttributeField<IAttribute>();
        }

        public static QAttribute Output(this Output i)
        {
            return i.GetAttributeField<QAttribute>();
        }

        public static T GetAttributeField<T>(this MemberInfo pInfo)
            where T : System.Attribute
        {
            T[] attributes =
                (T[])pInfo.GetCustomAttributes(
                    typeof(T),
                    false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0];
            else return null;
        }
    }
}