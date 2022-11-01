using StackExchange.Redis;

namespace QianQian_Novel.Redis
{
    public class SetRepository : BaseRedisRepository
    {
        public SetRepository(IDatabase redis) : base(redis)
        {
        }

        public async Task<long> Len(string key)
        {
            var len = await _redis.SetLengthAsync(key);
            return len;
        }

        public async Task<RedisValue[]> GetAll(string key)
        {
            var values = await _redis.SetMembersAsync(key);
            return values;
        }

        public async Task<RedisValue[]> Get(string key, int pcs = 1) 
        {
            var values = await _redis.SetRandomMembersAsync(key, pcs);
            return values;
        }

        public async Task<bool> AnyValue(string key, object value)
        {
            var exist = await _redis.SetContainsAsync(key, (RedisValue)value);
            return exist;
        }

        public async Task<bool> RemoveValue(string key, object value)
        {
            var done = await _redis.SetRemoveAsync(key, (RedisValue)value);
            return done;
        }

        public async Task<RedisValue> Pop(string key)
        {
            var value = await _redis.SetPopAsync(key);
            return value;
        }

    }
}
