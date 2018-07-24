using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using FWS.Generic.Framework.Annotations;

namespace FWS.Generic.Framework.Helpers
{
    public static class EnumHelper
    {
        public static List<T> EnumToList<T>() where T : struct, IConvertible
        {
            Type enumType = typeof(T);

            // Can't use type constraints on value types, so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);

            var enumValList = new List<T>(enumValArray.Length);

            enumValList.AddRange(from int val in enumValArray select (T) Enum.Parse(enumType, val.ToString()));

            return enumValList;
        }

        public static T EnumFromDescription<T>(string description) where T : struct // enum 
        {
            if (string.IsNullOrEmpty(description) || !typeof(T).IsEnum)
                throw new Exception("Type given must be an Enum");

            T result = default(T);

            try
            {
                foreach (T value in Enum.GetValues(typeof(T)))
                {
                    var fieldInfo = value.GetType().GetField(value.ToString());
                    var attributes = (TextAttribute[])fieldInfo.GetCustomAttributes(typeof(TextAttribute), false);

                    if (attributes.Length > 0)
                    {
                        if (description == attributes[0].Text)
                        {
                            result = value;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Throw the exception away and simply return the default value
            }

            return result;
        }
        
        /// <summary>
        /// Gets the <see cref="AssociatedClassNameAttribute "/> of an <see cref="Enum"/> type value.
        /// </summary>
        /// <param name="value">The <see cref="Enum"/> type value.</param>
        /// <returns>A string containing the name of class associated with the <see cref="Enum"/>.</returns>
        public static string GetAssociatedClassName(Enum value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            var description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            var attributes = (AssociatedClassNameAttribute[])fieldInfo.GetCustomAttributes(typeof(AssociatedClassNameAttribute), false);

            if (attributes.Length > 0)
            {
                description = attributes[0].ClassName;
            }
            return description;
        }

        /// <summary>
        /// Converts the <see cref="Enum"/> type to an <see cref="IList"/> compatible object.
        /// </summary>
        /// <param name="type">The <see cref="Enum"/> type.</param>
        /// <returns>An <see cref="IList"/> containing the enumerated type value and description.</returns>
        public static IList ToList(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            var list = new ArrayList();
            var enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
                list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value, true)));

            return list;
        }

        public static IEnumerable<T> AsEnumerable<T>()
        {
            var enumValues = Enum.GetValues(typeof(T));
            return from object value in enumValues select (T)value;
        }
        
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static string GetDescription(this Enum enumVal, bool returnToStringIfNull = false)
        {
            var descriptionAttribute = enumVal.GetAttributeOfType<DescriptionAttribute>();
            return descriptionAttribute == null ? (returnToStringIfNull ? enumVal.ToString() : null) : descriptionAttribute.Description;
        }
        
        public static string GetBriefDescription(this Enum enumVal, bool returnToStringIfNull = false)
        {
            var descriptionAttribute = enumVal.GetAttributeOfType<BriefDescriptionAttribute>();
            return descriptionAttribute == null ? (returnToStringIfNull ? enumVal.ToString() : null) : descriptionAttribute.Description;
        }
        
        /// <summary>
        /// If there is a complete type match between both lists 
        /// </summary>
        public static bool ListsAreEqual<T>(List<T> list1, List<T> list2)
             where T : struct, IConvertible
        {
            return list1.All(list2.Contains)
                && list2.All(list1.Contains);
        }

        /// <summary>
        /// Returns items in list1 that are not in list2
        /// </summary>
        public static List<T> MissingFrom<T>(this List<T> list1, List<T> list2)
             where T : struct, IConvertible
        {
            return list1.Where(i => !list2.Contains(i)).ToList();
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }


        public static IEnumerable<T> ToEnumerable<T>(this string s) where T : struct, IConvertible
        {
            if (string.IsNullOrEmpty(s))
                return null;

            var result = new List<T>();
            var entries = s.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var entry in entries)
            {
                if (Enum.TryParse<T>(entry, true, out var en))
                {
                    result.Add(en);
                }
            }
            return result;
        }
    }
}