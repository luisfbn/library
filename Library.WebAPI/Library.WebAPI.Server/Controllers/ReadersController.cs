using Library.Services;
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
        public ActionResult<ServiceResult<bool>> GetAll()
        {
            var result = _readerService.GetAll();
            
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.ErrorMessage);
        }
    }
}
