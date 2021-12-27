using Microsoft.AspNetCore.Mvc;
using webApp.Data;
using Attribute = webApp.Models.Attribute;

namespace webApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment Environment;
        private readonly ILogger<AttributeController> _logger;

        public AttributeController(IUnitOfWork unitOfWork, IWebHostEnvironment environment,
            ILogger<AttributeController> logger)
        {
            _unitOfWork = unitOfWork;
            Environment = environment;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Attribute>> Get()
        {
            return await _unitOfWork.Attribute.AllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Attribute?> Get(int id)
        {
            return await _unitOfWork.Attribute.FindByIDAsync(id);
        }

        [HttpPost]
        public async Task<JsonResult> Post(Attribute attribute)
        {
            string message = "Bad Request .";
            int status = 400;
            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.Attribute.AddAsync(attribute);
                    await _unitOfWork.SaveAsync();
                    message = "Attribute Create Successfully .";
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
                Attribute? attribute = await _unitOfWork.Attribute.FindByIDAsync(id) ?? null;
                if (attribute != null)
                {
                    _unitOfWork.Attribute.Remove(attribute);
                    await _unitOfWork.SaveAsync();
                    message = "Attribute Remove Successfully .";
                    status = 200;
                }
                else
                {
                    message = "Attribute Dose not Exist .";
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
        public async Task<IActionResult> Put(Attribute attribute, int id)
        {
            if (attribute.AttributeID != id)
            {
                return new BadRequestResult();
            }
            try
            {
                return await _unitOfWork.Attribute.UpdateAsync(attribute, id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
    }
}