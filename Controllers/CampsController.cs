using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppCore.Infrastructure;

namespace WebAppCore.Controllers
{
    public class CampsController : Controller
    {
        private readonly ILogger <CampsController> _logger;
        private readonly ICampRepository _repo;

        public CampsController(ILogger <CampsController> logger, ICampRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_repo.GetAllCamps());
        }
    }
}
