using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppCore.Infrastructure;

namespace WebAppCore.Controllers
{
    [Route("api/camps")]
    public class CampsController : Controller
    {
        private readonly ILogger <CampsController> _logger;
        private readonly ICampRepository _repo;

        public CampsController(ILogger <CampsController> logger, ICampRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public IActionResult Index() => View(_repo.GetAllCamps());


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var camp = _repo.GetAllCamps();
                if (camp == null) return BadRequest("This is bad requiest ");
                return Ok(camp);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
