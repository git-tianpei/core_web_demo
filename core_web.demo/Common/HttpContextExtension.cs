using System.Linq;
using core_web.demo.Data.Entity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace core_web.demo.Common
{
    public static class HttpContextExtension
    {
        public static User GetUser(this HttpContext context)
        {
            if (context.User?.Identity?.IsAuthenticated == true)
            {
                var value = context.User.Claims.First(x => x.Type == "id").Value;
                return JsonConvert.DeserializeObject<User>(value, new JsonSerializerSettings());
            }
            return null;
        }

        public static bool IsAuthenticated(this HttpContext context)
        {
            return context.User?.Identity?.IsAuthenticated == true;
        }
    }
}
