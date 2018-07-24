using System;
using AutoMapper;

namespace FWS.Generic.Framework.Helpers
{
    public static class AutoMapperExtensions
    {
        public static T MapTo<T>(this object source)
        {
            if (source != null)
                return Mapper.Map<T>(source);

            if (typeof(T).IsClass)
                return default(T);

            throw new ArgumentNullException(nameof(source),
                "Source can not be null when target type is not a reference type.");
        }
    }
}