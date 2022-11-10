using Microsoft.OpenApi.Models;
using QianQian_Novel.MyUtility.AttributeRepository;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace QianQian_Novel.Filter
{
    /// <summary>
    /// swagger拦截器
    /// 拦截属性,使其不在swagger中显示
    /// </summary>
    public class HiddenFieldFilter : ISchemaFilter
    {
        /// <summary>
        /// 拦截逻辑
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }
            var name = context.Type.FullName;
            var excludedProperties = context.Type.GetProperties();
            foreach (var property in excludedProperties)
            {
                var attribute = property.GetCustomAttribute<HiddenFieldAttribute>();
                if (attribute != null
                    && schema.Properties.ContainsKey(ToLowerStart(property.Name)))
                {
                    schema.Properties.Remove(ToLowerStart(property.Name));
                }
            };
        }

        static string ToLowerStart(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return source;
            }
            var start = source.Substring(0, 1);
            return $"{start.ToLower()}{source.Substring(1, source.Length - 1)}";
        }
    }
}
