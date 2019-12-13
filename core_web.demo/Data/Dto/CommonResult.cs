namespace core_web.demo.Data.Dto
{
    public class CommonResult
    {
        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public static CommonResult CreateError(int code, string msg)
        {
            return new CommonResult() { ErrCode = code, ErrMsg = msg };
        }
    }
}
