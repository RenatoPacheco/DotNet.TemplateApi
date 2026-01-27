using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System;
using TemplateApi.Api.Controllers.Common;

namespace TemplateApi.Api.Extensions
{
    public static class TypeExt
    {
        public static string ModelName(this Type type, string modelName)
        {
            string nameAttribute = string.Empty;
            DisplayAttribute attribute = null;

            var property = type?.GetProperty(modelName);

            if (!object.Equals(property, null))
                attribute = type.GetProperty(modelName).GetCustomAttribute<DisplayAttribute>();

            if (!object.Equals(attribute, null))
                nameAttribute = !object.Equals(attribute, null) ? attribute.Name : modelName;

            return nameAttribute ?? modelName;
        }

        public static bool IsApi(this Type type)
        {
            return typeof(BaseApiController).IsAssignableFrom(type);
        }

        public static bool IsMvc(this Type type)
        {
            return typeof(BaseMvcController).IsAssignableFrom(type);
        }
    }
}