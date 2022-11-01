using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using QianQian_Novel.Model.Basic;
using QianQian_Novel.Model.Enum;
using QianQian_Novel.MyUtility;

namespace QianQian_Novel.Controllers
{
    [Route("api/u/[action]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {

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

        [HttpPost]
        public async Task<object> Test1([FromBody] JObject jobj)
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

        [HttpDelete]
        public async Task<object> Test2([FromBody] JObject jobj,[FromQuery] string id)
        {
            var a = new BaseResponse<object>
            {
                Code = BaseStatusCode.Success,
                Data = new
                {
                    a = $"传入的数据为: {jobj.ToJson()}",
                    id = id
                },
                Msg = "测试方法2"
            };
            return await Task.FromResult(a);
        }
    }
}
