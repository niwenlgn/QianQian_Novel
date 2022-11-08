using Microsoft.EntityFrameworkCore;
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
        public DBService(QianContext db)
        {
            _db = db;
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
    }
}
