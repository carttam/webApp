using Microsoft.AspNetCore.Mvc;
using webApp.Data;
using webApp.Manager;
using webApp.Models;
using webApp.OAuth;
using webApp.Request;

namespace webApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment Environment;
        private readonly ILogger<UserController> _logger;

        public UserController(IUnitOfWork unitOfWork, IWebHostEnvironment environment, ILogger<UserController> logger)
        {
            _unitOfWork = unitOfWork;
            Environment = environment;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _unitOfWork.User.AllAsync();
        }

        [HttpPost("GetInfo")]
        [LoginRequire]
        public async Task<User?> Post([FromHeader]string token)
        {
            return (await _unitOfWork.Token.FirstOrDefaultAsync(t => t.token == token))?.User;
        }

        [HttpPost("Login")]
        public async Task<JsonResult> Post(LoginRequest request)
        {
            string message = "Bad Request .";
            int status = 400;
            Token? token = null;
            try
            {
                token = await UserManager.loginAsync(request.username, request.password, _unitOfWork);
                if (token != null)
                    message = "Login Successfully .";
                else
                    message = "Username Or Password is Wrong .";
                status = 200;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return new JsonResult(new
            {
                status = status,
                message = message,
                token = token?.token
            });
        }

        [HttpPost]
        public async Task<JsonResult> Post(User user)
        {
            string message = "Bad Request .";
            int status = 400;
            if (ModelState.IsValid)
            {
                try
                {
                    user.password = UserManager.makeHashPassword(user.password);
                    await _unitOfWork.User.AddAsync(user);
                    await _unitOfWork.SaveAsync();
                    message = "User Create Successfully .";
                    status = 200;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }
        
            return new JsonResult(new
            {
                status = status,
                message = message
            });
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            string message = "Bad Request .";
            int status = 400;
            try
            {
                User? user = await _unitOfWork.User.FindByIDAsync(id) ?? null;
                if (user != null)
                {
                    _unitOfWork.User.Remove(user);
                    await _unitOfWork.SaveAsync();
                    message = "User Remove Successfully .";
                    status = 200;
                }
                else
                {
                    message = "User Dose not Exist .";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return new JsonResult(new
            {
                status = status,
                message = message
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(User user, int id)
        {
            if (user.UserID != id)
            {
                return new BadRequestResult();
            }

            try
            {
                return await _unitOfWork.User.UpdateAsync(user, id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
    }
}