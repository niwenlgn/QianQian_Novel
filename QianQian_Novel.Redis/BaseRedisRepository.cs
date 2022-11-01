using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.Redis
{
    public class BaseRedisRepository
    {
        protected readonly IDatabase _redis;

        public BaseRedisRepository(IDatabase redis)
        {
            _redis = redis;
        }

        /// <summary>
        /// 查询键是否存在于redis中
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> Any(string key)
        {
            var exist = await _redis.KeyExistsAsync(key);
            return exist;
        }

        /// <summary>
        /// 删除制定key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> Remove(string key)
        {
            var done = await _redis.KeyDeleteAsync(key);
            return done;
        }
    }
}
