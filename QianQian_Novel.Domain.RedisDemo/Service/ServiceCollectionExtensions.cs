using Microsoft.Extensions.DependencyInjection;
using QianQian_Novel.Redis;

namespace QianQian_Novel.Domain.RedisDemo.Service
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<RedisService>();
        }

        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<BaseRedisRepository>();
            services.AddScoped<HashRepository>();
            services.AddScoped<ListRepository>();
            services.AddScoped<SetRepository>();
            services.AddScoped<ZSetRepository>();
            services.AddScoped<StringRepository>();
        }
    }
}
