using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QianQian_Novel.Model.Basic;
using QianQian_Novel.Model.UserManagement;
using QianQian_Novel.MyUtility;
using QianQian_Novel.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.Domain.RedisDemo.Service
{
    public class DBService
    {
        private readonly QianContext _db;

        private readonly IMapper _mapper;
        public DBService(QianContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<string> GetUser()
        {
            var users = await _db.BaseUsers.ToListAsync();
            var res = users.ToJson();
            return res;
        }

        public async Task<string> FindUsers(long userID)
        {
            var user = await _db.BaseUsers.FindAsync(userID);
            var res = user?.ToJson() ?? "";
            return res;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<BaseResponse<UserInfo>> Login(LoginRequest dto)
        {
            var success = false;
            Entity.Entity.BaseUser? user = null;
            var account = dto.Account ?? "";
            var pwd = dto.Password ?? "";
            if (long.TryParse(account, out long userid))
                user = await _db.BaseUsers.FirstOrDefaultAsync(c => c.Userid == userid);
            else
                user = await _db.BaseUsers.FirstOrDefaultAsync(c => (c.Userid == userid || c.UserName == account) && c.Password == pwd.EncodePassword());
            if (user is not null)
            {
                if (string.Equals(user.Password, string.Empty))
                {
                    user.Password = pwd.EncodePassword();
                    success = await _db.SaveChangesAsync() > 0;
                }
                success = string.Equals(user.Password, pwd.EncodePassword());
            }
            return new BaseResponse<UserInfo>
            {
                Code = success ? Model.Enum.BaseStatusCode.Success : Model.Enum.BaseStatusCode.Error,
                Data = _mapper.Map<UserInfo>(user),
                Msg = ""
            };
        }
    }
}
