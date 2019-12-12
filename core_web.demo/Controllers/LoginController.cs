using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace core_web.demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> Login()
        {
            if (HttpContext.User?.Identity?.IsAuthenticated == true)
            {
                return HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            }


            var claims = new[] { new Claim("id", "123") };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            });
            return "login success";
        }

        [HttpGet]
        public string GetUserId()
        {
            return HttpContext.User?.Identity?.IsAuthenticated == true ? HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value : null;
        }
    }
}
