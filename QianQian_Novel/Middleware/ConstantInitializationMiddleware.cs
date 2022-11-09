namespace QianQian_Novel.Middleware
{
    /// <summary>
    /// 常量配置初始化中间件
    /// </summary>
    public class ConstantInitializationMiddleware
    {

        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="next"></param>
        public ConstantInitializationMiddleware(IConfiguration configuration, RequestDelegate next)
        {
            _configuration = configuration;
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            MyUtility.UserEncodeUtil.Init(_configuration.GetSection("AppSettings:MachineKey").Value);

            await _next(context);
        }
    }

    /// <summary>
    /// 常量配置初始化中间件
    /// </summary>
    public static class ConstantInitializationExtensions
    {
        /// <summary>
        /// 常量配置初始化
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder ConstantInitialization(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ConstantInitializationMiddleware>();
        }
    }
}
