using System;

namespace core_web.demo.ioc
{
    public class TestIocImpl : ITestIoc
    {
        public string NowTimeString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
