using core_web.demo.Config;

namespace core_web.demo.Dao
{
    public class MysqlBase
    {
        protected readonly string ConnectionString;

        public MysqlBase()
        {
            ConnectionString = EnvironmentSetting.Get("dbconnection");
        }
    }
}
