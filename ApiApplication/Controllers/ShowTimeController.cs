using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Database.Query;
using ApiApplication.ViewModels.Models;
using ApiApplication.ViewModels.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowTimeController : BaseController
    {
        private readonly IShowtimesRepository _iShowtimesRepository;
        private readonly ILogger<BaseController> _logger;

        public ShowTimeController(IShowtimesRepository iShowtimesRepository, ILogger<BaseController> logger) : base(logger)
        {
            _iShowtimesRepository = iShowtimesRepository;
            _logger = logger;
        }

        //[Authorize(Policy = "Read")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCollectionQuery query)
        {
            return Ok(await _iShowtimesRepository.GetCollection(query));
        }

        [Authorize(Policy = "Write")]
        [HttpPost]
        public async Task<IActionResult> Post(ShowtimeModel model)
        {
            if(model == null)
                return BadRequest(new ResponseModel() { Message = "General Error", Success = false });

            return Ok(await _iShowtimesRepository.Add(model));
        }

        [Authorize(Policy = "Write")]
        [HttpPut]
        public async Task<IActionResult> Put(ShowtimeModel model)
        {
            if (model == null)
                return BadRequest(new ResponseModel() { Message = "General Error", Success = false });

            return Ok(await _iShowtimesRepository.Update(model));
        }

        [Authorize(Policy = "Write")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new ResponseModel() { Message = "General Error", Success = false });

            await _iShowtimesRepository.Delete(id);

            return Ok();
        }

        [Authorize(Policy = "Write")]
        [HttpPatch]
        public IActionResult Patch()
        {
            throw new Exception();
        }
    }
}
