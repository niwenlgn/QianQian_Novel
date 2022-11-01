using StackExchange.Redis;

namespace QianQian_Novel.Redis
{
    public class StringRepository: BaseRedisRepository
    {
        public StringRepository(IDatabase redis): base(redis)
        {
        }


        public async Task<string> Get(string key)
        {
            var value = await _redis.StringGetAsync(key);
            return value.ToString();
        }

        public async Task<List<string>> MGet(params string[] keys)
        {
            var values = await _redis.StringGetAsync(keys.Select(c => new RedisKey(c)).ToArray());
            return values.Select(c => c.ToString()).ToList();
        }

        public async Task<bool> Set(string key, string value, long expires = TimeSpan.TicksPerHour)
        {
            var done = await _redis.StringSetAsync(key, new RedisValue(value), new TimeSpan(expires));
            return done;
        }

        public async Task<long> Len(string key)
        {
            var len = await _redis.StringLengthAsync(key);
            return len;
        }
    }
}
