using Microsoft.AspNetCore.Mvc;
using webApp.Data;
using webApp.Models;
using webApp.OAuth;

namespace webApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LoginRequire]
    public class SellController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment Environment;
        private readonly ILogger<SellController> _logger;

        public SellController(IUnitOfWork unitOfWork, IWebHostEnvironment environment, ILogger<SellController> logger)
        {
            _unitOfWork = unitOfWork;
            Environment = environment;
            _logger = logger;
        }

        // Get User Sells /api/[controller] POST
        [HttpPost]
        public async Task<IEnumerable<Sell?>> Post()
        {
            User user = HttpContext.Items["User"] as User;
            return await _unitOfWork.Sell.WhereAsync(s=>s.User == user);
        }
    }
}