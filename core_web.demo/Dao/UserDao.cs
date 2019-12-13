using System;
using System.Linq;
using core_web.demo.Data.Entity;
using Dapper;
using MySql.Data.MySqlClient;

namespace core_web.demo.Dao
{
    public class UserDao : MysqlBase
    {
        public User GetUserByOpenId(string openId)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var sql = "select * from wx_user where openid = @openId";
                return conn.Query<User>(sql, new { openId }).SingleOrDefault();
            }
        }

        public User AddUser(User user)
        {
            user.CreateTime = DateTime.Now;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var sql = "insert into wx_user (openid,unionid,createtime) values (@openid,@unionid,@createtime)";
                if (conn.Execute(sql, user) > 0)
                {
                    return GetUserByOpenId(user.OpenId);
                }
                return null;
            }
        }

        public User GetLoginUser(User user)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var oldUser = GetUserByOpenId(user.OpenId);
                if (oldUser != null) return oldUser;
                user.CreateTime = DateTime.Now;
                var sql = "insert into wx_user (openid,unionid,createtime) values (@openid,@unionid,@createtime)";
                if (conn.Execute(sql, user) > 0)
                {
                    return GetUserByOpenId(user.OpenId);
                }
                return null;
            }
        }
    }
}
