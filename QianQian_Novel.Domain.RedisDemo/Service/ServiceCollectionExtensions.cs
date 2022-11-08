using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QianQian_Novel.PostgreSQL;
using QianQian_Novel.Redis;

namespace QianQian_Novel.Domain.RedisDemo.Service
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<RedisService>();
            services.AddScoped<DBService>();
        }

        public static void AddRepository(this IServiceCollection services, string pg_Master)
        {
            services.AddScoped<BaseRedisRepository>();
            services.AddScoped<HashRepository>();
            services.AddScoped<ListRepository>();
            services.AddScoped<SetRepository>();
            services.AddScoped<ZSetRepository>();
            services.AddScoped<StringRepository>();
            services.AddDbContext<QianContext>(option => option.UseNpgsql(pg_Master));
        }
    }
}
