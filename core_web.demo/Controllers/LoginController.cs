using System;
using System.Security.Claims;
using System.Threading.Tasks;
using core_web.demo.Common;
using core_web.demo.Dao;
using core_web.demo.Data.Dto;
using core_web.demo.Data.Entity;
using core_web.demo.Data.Req;
using core_web.demo.Remote;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace core_web.demo.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<CommonResult> MpLogin([FromBody]LoginReq req)
        {
            //已登录直接跳过
            if (HttpContext.IsAuthenticated()) return new CommonResult();
            //微信获取session_key
            var session = await WeixinRemote.GetSessionKey(req.Code);
            if (session?.ErrCode != 0) return CommonResult.CreateError(1, "");
            //数据库中获取微信校验过的身份，没有则新增
            var user = new User { OpenId = session.OpenId, UnionId = session.UnionId };
            var userDao = new UserDao();
            user = userDao.GetLoginUser(user);
            //写入cookie中
            await SetCookie(user);
            return new CommonResult();
        }

        [NonAction]
        private async Task SetCookie(User user)
        {
            if (HttpContext.IsAuthenticated())
                return;

            var serializerSettings = new JsonSerializerSettings();
            var value = JsonConvert.SerializeObject(user, serializerSettings);
            var claims = new[] { new Claim("id", value) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            });
        }
    }
}
