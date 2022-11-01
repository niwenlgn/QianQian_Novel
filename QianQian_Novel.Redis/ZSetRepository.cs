using StackExchange.Redis;

namespace QianQian_Novel.Redis
{
    public class ZSetRepository : BaseRedisRepository
    {
        public ZSetRepository(IDatabase redis) : base(redis)
        {
        }

    }
}
