namespace core_web.demo.Data.Dto
{
    public class WeixinSession
    {
        public string OpenId { get; set; }

        public string SessionKey { get; set; }

        public string UnionId { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
