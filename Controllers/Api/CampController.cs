using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppCore.Entities;
using WebAppCore.Infrastructure;

namespace WebAppCore.Controllers.Api
{
    [Route("api/Camp")]
    public class CampController : Controller
    {
        readonly ILogger <CampController> _logger;
        private readonly ICampRepository _repo;

        public CampController(ILogger <CampController> logger,
            ICampRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var cam = _repo.GetAllCamps();
                if (cam == null)
                    return NotFound();
                return Ok(cam);
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
                return Ok(camp);
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
                _repo.Add(model);
                if (_repo.SaveAll())
                {
                    var newLink = Url.Link("CampGet", new {id = model.Id});
                    if (newLink != null) return Created(newLink, model);
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
        public IActionResult Put(int id, [FromBody] Camp model)
        {
            try
            {
                var oldCamp = _repo.GetCamp(id);
                if (oldCamp == null) return NotFound($"Not found with {(Camp) null}");

                oldCamp.Name = model.Name ?? oldCamp.Name;
                oldCamp.Description = model.Description ?? oldCamp.Description;
                oldCamp.Length = model.Length > 0 ? model.Length : oldCamp.Length;
                oldCamp.Location = model.Location ?? oldCamp.Location;
                oldCamp.EventDate = model.EventDate ?? oldCamp.EventDate;
                if (_repo.SaveAll())
                {
                    return Ok(oldCamp);
                }

                return Ok();
            }
            catch (Exception)
            {
                _logger.LogError("Failed to execute PUT");
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                _logger.LogError("Failed to execute DELETE");
                return BadRequest();
            }
        }
    }
}
