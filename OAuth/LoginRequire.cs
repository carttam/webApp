using System.Net;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using webApp.Controllers;
using webApp.Data;
using webApp.Models;

namespace webApp.OAuth;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class LoginRequire : System.Attribute, IAuthorizationFilter
{
    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        Microsoft.Extensions.Primitives.StringValues token = "";
        context.HttpContext.Request.Headers.TryGetValue("token", out token);
        var controller = context.HttpContext.RequestServices.GetService(typeof(ControllerBase));
        User? user = await isValidToken(token.FirstOrDefault(),
            context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork)) as IUnitOfWork);
        if (user != null)
        {
            context.HttpContext.Items.Add("User",user);
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

    private async Task<User?> isValidToken(string? token, IUnitOfWork? unitOfWork)
    {
        if (token != null && unitOfWork != null)
        {
            Token? tkn = await unitOfWork.Token.FirstOrDefaultAsync(t=>t.token == token);
            if (tkn != null)
            {
                return tkn.User;
            }
        }
        return null;
    }
}