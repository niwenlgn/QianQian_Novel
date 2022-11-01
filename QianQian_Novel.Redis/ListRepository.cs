using StackExchange.Redis;

namespace QianQian_Novel.Redis
{
    public class ListRepository : BaseRedisRepository
    {
        public ListRepository(IDatabase redis) : base(redis)
        {
        }
    }
}
