using Microsoft.EntityFrameworkCore;
using QianQian_Novel.Model.Basic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.PostgreSQL.helper
{
    public static class QueryExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, List<OrderByField> orderByFileds)
        {
            var parameter = Expression.Parameter(typeof(T), "o");
            for (int i = 0; i < orderByFileds.Count; i++)
            {
                var property = typeof(T).GetProperty(orderByFileds[i].Field, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
                if (property == null)
                    continue;

                //创建一个访问属性的表达式
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);

                if (orderByFileds[i].OrderBy == OrderByField.OrderType.Default)
                    continue;

                string orderName;
                if (i > 0)
                    orderName = orderByFileds[i].OrderBy == OrderByField.OrderType.Desc ? "ThenByDescending" : "ThenBy";
                else
                    orderName = orderByFileds[i].OrderBy == OrderByField.OrderType.Desc ? "OrderByDescending" : "OrderBy";

                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), orderName, new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
                query = query.Provider.CreateQuery<T>(resultExp);
            }
            return query;
        }

        public static IEnumerable<dynamic> SqlQueryDynamic(this DbContext db, string sql, params SqlParameter[] parameters)
        {
            using (var cmd = db.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql;

                if (cmd.Connection!.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                foreach (var p in parameters)
                {
                    var dbParameter = cmd.CreateParameter();
                    dbParameter.DbType = p.DbType;
                    dbParameter.ParameterName = p.ParameterName;
                    dbParameter.Value = p.Value;
                    cmd.Parameters.Add(dbParameter);
                }

                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var row = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                        {
                            row.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
                        }

                        yield return row;
                    }
                }
            }
        }

        public async static Task<object> ExecSql(this DbContext db, string sql, params SqlParameter[] parameters)
        {
            var connection = db.Database.GetDbConnection();
            using (var cmd = connection.CreateCommand())
            {
                await db.Database.OpenConnectionAsync();
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();

                return parameters[parameters.Length - 1].Value;
            }
        }
    }
}
