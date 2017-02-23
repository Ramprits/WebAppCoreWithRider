using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppCore.Infrastructure;
using WebAppCore.Models.CampModels;

namespace WebAppCore.Controllers
{
    public class CampsController : Controller
    {
        private readonly ILogger <CampsController> _logger;
        private readonly ICampRepository _repo;
        private readonly IMapper _mapper;

        public CampsController(ILogger <CampsController> logger,
            ICampRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var camp = _repo.GetAllCamps();
            var result = _mapper.Map <IEnumerable <CampModel>>(camp);
            return View(result);
        }
    }
}
