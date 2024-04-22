using Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadersController(IReaderService readerService) : ControllerBase
    {
        private readonly IReaderService _readerService = readerService;

        [HttpGet]
        public IActionResult GetAll()
        {
            var readers = _readerService.GetAll();
            return Ok(readers);
        }
    }
}
