using System.Net;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using webApp.Data;

namespace webApp.OAuth;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class LoginRequire : Attribute, IAuthorizationFilter
{
    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        Microsoft.Extensions.Primitives.StringValues token = "";
        context.HttpContext.Request.Headers.TryGetValue("token", out token);
        if (await isValidToken(token.FirstOrDefault(),
                context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork)) as IUnitOfWork))
        {
            return;
        }
        else
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.MethodNotAllowed;
            context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>()!.ReasonPhrase =
                "Not Authorized";
            context.Result = new JsonResult("NotAuthorized")
            {
                Value = new
                {
                    Status = "Error",
                    Message = "Not Authorized ."
                },
            };
        }
    }

    private async Task<bool> isValidToken(string? token, IUnitOfWork? unitOfWork)
    {
        if (token != null && unitOfWork != null)
        {
            if ((await unitOfWork.Token.FirstOrDefaultAsync(t=>t.token == token)) != null)
            {
                return true;
            }
        }
        return false;
    }
}