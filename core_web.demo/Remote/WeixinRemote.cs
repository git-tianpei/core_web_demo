using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using core_web.demo.Config;
using core_web.demo.Data.Dto;
using Newtonsoft.Json;

namespace core_web.demo.Remote
{
    public class WeixinRemote
    {
        public static async Task<WeixinSession> GetSessionKey(string code)
        {
            if (string.IsNullOrEmpty(code)) return null;
            var appId = EnvironmentSetting.Get("appid");
            var secret = EnvironmentSetting.Get("secret");
            string url = $"https://api.weixin.qq.com/sns/jscode2session?appid={appId}&secret={secret}&js_code={code}&grant_type=authorization_code";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.StatusCode != HttpStatusCode.OK) return null;
                    var body = await response.Content.ReadAsStringAsync();
                    var settings = new JsonSerializerSettings();
                    return JsonConvert.DeserializeObject<WeixinSession>(body, settings);
                }
            }
        }
    }
}
