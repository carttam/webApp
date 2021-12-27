using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApp.Data;
using webApp.Models;

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

        [HttpGet("{id}")]
        public async Task<User?> Get(int id)
        {
            return await _unitOfWork.User.FindByIDAsync(id);
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
            try
            {
                return await _unitOfWork.User.UpdateAsync(user, id);
            }
            catch (Exception e)
            {
                return new BadRequestResult();
            }
        }
    }
}