using AutoMapper;
using QianQian_Novel.Entity.Entity;
using QianQian_Novel.Model.UserManagement;
using QianQian_Novel.MyUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.Domain.RedisDemo.Service
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<BaseUser, UserInfo>();
            #region 公共
            CreateMap<long, DateTime?>().ConvertUsing(source => source == 0 ? null : new DateTime?(source.ToDateTime()));
            CreateMap<DateTime?, long>().ConvertUsing(source => source == null ? 0 : source.Value.ToUnixStamp());
            CreateMap<long, DateTime>().ConvertUsing(source => source.ToDateTime());
            CreateMap<DateTime, long>().ConvertUsing(source => source.ToUnixStamp());
            CreateMap<Guid, string>().ConvertUsing(source => source.ToString());
            CreateMap<Guid?, string>().ConvertUsing(source => source == null ? "" : source.ToString());
            CreateMap<string, Guid>().ConvertUsing(source => source == "" ? Guid.Empty : new Guid(source));
            CreateMap<string, Guid?>().ConvertUsing(source => source == "" ? null : new Guid(source));
            CreateMap<string, string>().ConvertUsing(source => source ?? "");
            CreateMap<int?, int>().ConvertUsing(source => source ?? 0);
            #endregion
        }
    }
}
