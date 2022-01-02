using Microsoft.AspNetCore.Mvc;
using webApp.Data;
using webApp.Models;
using webApp.OAuth;
using webApp.Request;

namespace webApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LoginRequire]
    public class DiscountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment Environment;
        private readonly ILogger<DiscountController> _logger;

        public DiscountController(IUnitOfWork unitOfWork, IWebHostEnvironment environment, ILogger<DiscountController> logger)
        {
            _unitOfWork = unitOfWork;
            Environment = environment;
            _logger = logger;
        }

        // Get Discount /api/[controller] POST
        [HttpPost]
        public async Task<DisCountCode?> Post(DiscountRequest req)
        {
            return await _unitOfWork.DisCountCode.FirstOrDefaultAsync(d=>d.code == req.d_code);
        }
    }
}