using Microsoft.AspNetCore.Mvc.Filters;

namespace QianQian_Novel.Filter
{
    /// <summary>
    /// 请求过滤器
    /// </summary>
    public class PermissionRequiredAttribute : ActionFilterAttribute
    {

        public PermissionRequiredAttribute()
        {

        }
    }
}
