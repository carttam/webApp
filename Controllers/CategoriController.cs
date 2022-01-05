using Microsoft.AspNetCore.Mvc;
using webApp.Data;
using webApp.Models;
using webApp.OAuth;

namespace webApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment Environment;
        private readonly ILogger<CategoriController> _logger;

        public CategoriController(IUnitOfWork unitOfWork, IWebHostEnvironment environment, ILogger<CategoriController> logger)
        {
            _unitOfWork = unitOfWork;
            Environment = environment;
            _logger = logger;
        }

        // Get Categories /api/[controller] POST
        [HttpPost]
        public async Task<IEnumerable<Categori?>> Post()
        {
            return await _unitOfWork.Categori.AllAsync();
        }
    }
}