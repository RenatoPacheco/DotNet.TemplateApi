using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore.API.Template.Site.Extensions
{
    public static class ModelStateExtensions
    {
        public static string DisplayName(this Type type, string modelName)
        {
            string nameAttribute = string.Empty;
            DisplayAttribute attribute = null;

            var property = type?.GetProperty(modelName);

            if (!object.Equals(property, null))
            {
                attribute = type.GetProperty(modelName).GetCustomAttribute<DisplayAttribute>();
            }

            if (!object.Equals(attribute, null))
            {
                nameAttribute = !object.Equals(attribute, null) ? attribute.Name : modelName;
            }

            return nameAttribute ?? modelName;
        }
    }
}