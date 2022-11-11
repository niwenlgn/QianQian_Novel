using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QianQian_Novel.Domain.RedisDemo.Service;
using QianQian_Novel.Helper;
using QianQian_Novel.Model.Basic;
using QianQian_Novel.Model.Enum;
using QianQian_Novel.Model.UserManagement;
using QianQian_Novel.MyUtility;
using QianQian_Novel.MyUtility.AttributeRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QianQian_Novel.Controllers
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [Route("api/u/[action]")]
    [Authorize]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        readonly RedisService _redisService;
        readonly DBService _dbService;
        readonly JWTHelper _jwtHelper;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="redisService"></param>
        /// <param name="dbService"></param>
        /// <param name="jwtHelper"></param>
        public UserManagementController(RedisService redisService, DBService dbService, JWTHelper jwtHelper)
        {
            _redisService = redisService;
            _dbService = dbService;
            _jwtHelper = jwtHelper;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginRequest dto)
        {
            var loginRes = await _dbService.Login(dto);
            if (loginRes.Code != BaseStatusCode.Success || loginRes.Data is null)
            {
                return new BaseResponse()
                {
                    Code = loginRes.Code,
                    Msg = loginRes.Msg
                };
            }
            var auths = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, loginRes.Data.UserName),
                new Claim(ClaimTypes.NameIdentifier, loginRes.Data.Userid.ToString()),
                new Claim(ClaimTypes.Name, loginRes.Data.UserName)
            };
            var token = _jwtHelper.TokenGenerator(auths);
            return new BaseResponse<object>()
            {
                Code = loginRes.Code,
                Msg = loginRes.Msg,
                Data = new
                {
                    token = $"Bearer {new JwtSecurityTokenHandler().WriteToken(token)}",
                    expiration = token.ValidTo
                }
            };
        }

        /// <summary>
        /// 测试Get请求
        /// </summary>
        /// <returns></returns>
        [Hidden]
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
        public async Task<object> Test2([FromBody] LoginRequest jobj, [FromQuery] string id)
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

        /// <summary>
        /// 测试pg数据库
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> Test4()
        {
            var res = await _dbService.GetUser();
            return new BaseResponse
            {
                Code = BaseStatusCode.Success,
                Msg = res
            };
        }

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> FindUser(long userID)
        {
            var res = await _dbService.FindUsers(userID);
            return new BaseResponse
            {
                Code = BaseStatusCode.Success,
                Msg = res
            };
        }
    }
}
