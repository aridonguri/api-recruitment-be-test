using ApiApplication.ViewModels.Models;
using ApiApplication.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ImdbStatusModel imdbStatus;

        public StatusController(ImdbStatusModel imdbStatus)
        {
            this.imdbStatus = imdbStatus;
        }

        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok(imdbStatus);
        }
    }
}
