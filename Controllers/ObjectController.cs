using Microsoft.AspNetCore.Mvc;
using webApp.Data;
using webApp.Manager;
using webApp.OAuth;
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

        // Get Image /api/[controller]/image/{imgName} GET
        [HttpGet("image/{imgName}")]
        public IActionResult Get(string imgName)
        {
            return File(System.IO.File.ReadAllBytes(Path.Combine(Environment.ContentRootPath,$"img/{imgName}")),"image/jpeg");
        }

        // Get All Objects /api/[controller] GET
        [HttpGet]
        public async Task<IEnumerable<Object>> Get()
        {
            return await _unitOfWork.Object.AllAsync();
        }
        
        // Get Paginated Objects /api/[controller]/{page} GET
        [HttpGet("{page}")]
        public async Task<JsonResult> GetP(int page)
        {
            PaginatedList<Object> paginatedList =
                await PaginatedList<Object>.CreateAsync(_unitOfWork.Object.AllQuery(), page);
            return new JsonResult(new
            {
                data = paginatedList,
                page = paginatedList.PageIndex,
                hasNext = paginatedList.HasNextPage,
                hasPrev = paginatedList.HasPreviousPage,
                totalPAge = paginatedList.TotalPages,
            });
        }

        // Get Single Object /api/[controller]/single/{id} GET
        [HttpGet("single/{id}")]
        public async Task<Object?> Get(int id)
        {
            return await _unitOfWork.Object.FindByIDAsync(id);
        }

        // Insert Object /api/[controller]/(File) POST
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

        // Delete Object /api/[controller]/{id} DELETE
        [HttpDelete("{id}")]
        [LoginRequire]
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

        // Update Object /api/[controller]/{id} PUT
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