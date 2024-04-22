using Library.Services;
using Library.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController(ILoanService loanService) : ControllerBase
    {
        private readonly ILoanService _loanService = loanService;

        [HttpPost("register")]
        public ActionResult<ServiceResult<bool>> RegisterLoan(RegisterLoanDto loanDto)
        {
            var result = _loanService.RegisterLoan(loanDto.BookId, loanDto.ReaderId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("return/{id}")]
        public ActionResult<ServiceResult<bool>> ReturnBook(int id)
        {
            var result = _loanService.ReturnBook(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }

    }
}
