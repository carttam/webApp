using Microsoft.AspNetCore.Mvc;
using webApp.Data;
using webApp.Manager;
using Object = webApp.Models.Object;

namespace webApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment Environment;
        private readonly ILogger<ObjectController> _logger;

        public ObjectController(IUnitOfWork unitOfWork, IWebHostEnvironment environment,
            ILogger<ObjectController> logger)
        {
            _unitOfWork = unitOfWork;
            Environment = environment;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Object>> Get()
        {
            return await _unitOfWork.Object.AllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Object?> Get(int id)
        {
            return await _unitOfWork.Object.FindByIDAsync(id);
        }

        [HttpPost]
        public async Task<JsonResult> Post([FromForm] Object Object, IFormFile file)
        {
            string message = "Bad Request .";
            int status = 400;
            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.Object.AddAsync(Object, file, Environment);
                    await _unitOfWork.SaveAsync();
                    message = "Object Create Successfully .";
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
                Object? Object = await _unitOfWork.Object.FindByIDAsync(id) ?? null;
                if (Object != null)
                {
                    _unitOfWork.Object.Remove(Object, Environment);
                    await _unitOfWork.SaveAsync();
                    message = "Object Remove Successfully .";
                    status = 200;
                }
                else
                {
                    message = "Object Dose not Exist .";
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
        public async Task<IActionResult> Put([FromForm] Object Object, int id, IFormFile file)
        {
            if (Object.ObjectID != id)
            {
                return new BadRequestResult();
            }

            try
            {
                return await _unitOfWork.Object.UpdateAsync(Object, id, file, Environment);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
    }
}