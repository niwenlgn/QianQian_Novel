using QianQian_Novel.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.Domain.RedisDemo.Service
{
    public class RedisService
    {
        StringRepository _redis_str { get; set; }
        public RedisService(StringRepository stringRepository)
        {
            _redis_str = stringRepository;
        }

        public async Task<bool> StringSet(string key, string value, long expires = TimeSpan.TicksPerHour)
        {
            var done = await _redis_str.Set(key, value, expires);
            return done;
        }

        public async Task<string> StringGet(string key)
        {
            var value = await _redis_str.Get(key);
            return value;
        }

        public async Task<bool> DeleteKey(string key)
        {
            var done = await _redis_str.Remove(key);
            return done;
        }
    }
}
