using Microsoft.OpenApi.Models;
using QianQian_Novel.MyUtility.AttributeRepository;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace QianQian_Novel.Filter
{
    /// <summary>
    /// swagger拦截器
    /// 拦截类与方法,使其不在swagger中显示
    /// </summary>
    public class HiddenApiFilter : IDocumentFilter
    {
        /// <summary>
        /// 拦截逻辑
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var item in context.ApiDescriptions)
            {
                if (item.TryGetMethodInfo(out MethodInfo methodInfo) && methodInfo.ReflectedType is not null && item.RelativePath is not null)
                {
                    if (methodInfo.ReflectedType.CustomAttributes.Any(t => t.AttributeType == typeof(HiddenAttribute))
                    || methodInfo.CustomAttributes.Any(t => t.AttributeType == typeof(HiddenAttribute)))
                    {
                        var key = "/" + item.RelativePath.TrimEnd('/');
                        if (key.Contains('?'))
                        {
                            int idx = key.IndexOf("?", StringComparison.Ordinal);
                            key = key[..idx];
                        }
                        if (swaggerDoc.Paths.ContainsKey(key))
                        {
                            swaggerDoc.Paths.Remove(key);
                        }
                    }
                }
            }
        }
    }
}
