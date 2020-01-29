using System;
using System.ComponentModel;
using System.Reflection;

namespace BowlingCommon
{
    public static class EnumHelper
    {
        /// <summary>
        /// Get the Description annotation for the specific enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetDescription<T>(T source)
        {
            Type type = source.GetType();
            FieldInfo fieldInfo = type.GetField(source.ToString());

            if (fieldInfo == null)
            {
                return source.ToString();
            }
            DescriptionAttribute descriptionAttribute = fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            string description = (descriptionAttribute == null) ?
                source.ToString() :
                descriptionAttribute.Description;
            return description;
        }
    }
}
