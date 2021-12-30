using Fithub.API.Interfaces;
using Fithub.API.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Fithub.API.Helpers
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService service)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var authInput = new AuthData() { Authorization = token };
                var authOutput = await service.AuthorizeAsync(authInput);

                if (authOutput != null)
                    context.Items["User"] = authOutput.Authorization;
            }

            await _next(context);
        }
    }
}
