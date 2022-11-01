using StackExchange.Redis;

namespace QianQian_Novel.Redis
{
    public class HashRepository : BaseRedisRepository
    {
        public HashRepository(IDatabase redis) : base(redis)
        {
        }
    }
}
