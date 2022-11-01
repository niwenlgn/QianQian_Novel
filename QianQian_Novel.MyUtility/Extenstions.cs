using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.MyUtility
{
    public static class Extenstions
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="camelCase"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, bool camelCase = false)
        {
            if (obj == null)
                return string.Empty;
            var setting = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            return camelCase ? JsonConvert.SerializeObject(obj, setting) : JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T? ParseJson<T>(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(s);
        }

        /// <summary>
        /// 转换时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ToUnixStamp(this DateTime dt)
        {
            return (long)(dt.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        /// <summary>
        /// 时间戳转Datetime
        /// </summary>
        /// <param name="unixStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long unixStamp)
        {
            return new DateTime(1970, 1, 1).AddHours(8).AddMilliseconds(unixStamp);
        }
    }
}
