using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using QianQian_Novel.Domain.RedisDemo.Service;
using QianQian_Novel.Model.Basic;
using QianQian_Novel.Model.Enum;
using QianQian_Novel.Model.UserManagement;
using QianQian_Novel.MyUtility;

namespace QianQian_Novel.Controllers
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [Route("api/u/[action]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        readonly RedisService _redisService;
        
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="redisService"></param>
        public UserManagementController(RedisService redisService)
        {
            _redisService = redisService;
        }
        /// <summary>
        /// 测试Get请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> Test()
        {
            var a = new BaseResponse<object>
            {
                Code = BaseStatusCode.Success,
                Data = new
                {
                    a = "1",
                    b = 2,
                    c = new int[] { 3, 4, 5 }
                },
                Msg = "测试方法"
            };
            return await Task.FromResult(a);
        }

        /// <summary>
        /// 测试Post请求
        /// </summary>
        /// <param name="jobj">验证信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> Test1([FromBody] LoginRequest jobj)
        {
            var a = new BaseResponse<object>
            {
                Code = BaseStatusCode.Success,
                Data = new
                {
                    a = $"传入的数据为: {jobj.ToJson()}"
                },
                Msg = "测试方法1"
            };
            return await Task.FromResult(a);
        }

        /// <summary>
        /// 测试Delete请求
        /// </summary>
        /// <param name="jobj">验证信息</param>
        /// <param name="id">关联ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<object> Test2([FromBody] LoginRequest jobj,[FromQuery] string id)
        {
            var res = await _redisService.DeleteKey(id);
            var a = new BaseResponse<object>
            {
                Code = BaseStatusCode.Success,
                Data = new
                {
                    a = $"传入的数据为: {jobj.ToJson()}",
                    id = id
                },
                Msg = res ? "保存成功" : "保存失败"
            };
            return await Task.FromResult(a);
        }

        /// <summary>
        /// 测试Put请求
        /// </summary>
        /// <param name="jobj">验证信息</param>
        /// <param name="id">关联ID</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<object> Test3([FromBody] LoginRequest jobj, [FromQuery] string id)
        {
            try
            {

                var res = await _redisService.StringSet(id, jobj.ToJson());
                var a = new BaseResponse<object>
                {
                    Code = BaseStatusCode.Success,
                    Data = new
                    {
                        a = $"传入的数据为: {jobj.ToJson()}",
                        id = id
                    },
                    Msg = res ? "保存成功" : "保存失败"
                };
                return await Task.FromResult(a);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
