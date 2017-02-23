using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppCore.Entities;
using WebAppCore.Infrastructure;
using WebAppCore.Models.CampModels;

namespace WebAppCore.Controllers.Api
{
    [Route("api/Camp")]
    public class CampController : Controller
    {
        readonly ILogger <CampController> _logger;
        private readonly ICampRepository _repo;
        private readonly IMapper _mapper;

        public CampController(ILogger <CampController> logger,
            ICampRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var cam = _repo.GetAllCamps();
                if (cam == null)
                    return NotFound();
                return Ok(_mapper.Map <IEnumerable <CampModel>>(cam));
            }
            catch (Exception)
            {
                _logger.LogError("Failed to execute GET");
                return BadRequest();
            }
        }


        [HttpGet("{Id}", Name = "CampGet")]
        public IActionResult Get(int id, bool includeSpeakers = false)
        {
            try
            {
                var camp = includeSpeakers ? _repo.GetCampWithSpeakers(id) : _repo.GetCamp(id);
                if (camp == null) return NotFound();
                return Ok(_mapper.Map <CampModel>(camp));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Camp model)
        {
            try
            {
                if (_mapper != null)
                {
                    var camp = _mapper.Map <Camp>(model);
                    _repo.Add(camp);
                    if (_repo.SaveAll())
                    {
                        var newLink = Url.Link("CampGet", new {id = model.Id});
                        if (newLink != null)
                            return Created(newLink, _mapper.Map <CampModel>(camp));
                    }
                }
            }
            catch (Exception)
            {
                _logger.LogError("Failed to execute Post");
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CampModel model)
        {
            try
            {
                var oldCamp = _repo.GetCamp(id);
                if (oldCamp == null) return NotFound($"Not found with {(Camp) null}");

                if (model != null) _mapper.Map(model, oldCamp);
                if (_repo.SaveAll())
                {
                    return Ok(_mapper.Map <CampModel>(oldCamp));
                }
            }
            catch (Exception)
            {
                _logger.LogError("Failed to execute PUT");
                return BadRequest();
            }
            return BadRequest("Failed to execute PUT");
        }


//        [HttpPut("moniker}")]
//        public IActionResult Put(string moniker, [FromBody] CampModel model)
//        {
//            try
//            {
//                var oldCamp = _repo.GetCampByMoniker(moniker);
//                if (oldCamp == null) return NotFound($"Not found with {(Camp) null}");
//
//                _mapper.Map(model, oldCamp);
//                if (_repo.SaveAll())
//                {
//                    return Ok(_mapper.Map <CampModel>(oldCamp));
//                }
//
//                return Ok();
//            }
//            catch (Exception)
//            {
//                _logger.LogError("Failed to execute PUT");
//                return BadRequest();
//            }
//        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleteCamp = _repo.GetCamp(id);
                if (deleteCamp == null) return NotFound();
                _repo.Delete(deleteCamp);
                if (_repo.SaveAll())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                _logger.LogError("Failed to execute DELETE");
                return BadRequest();
            }

            return BadRequest();
        }
    }
}
