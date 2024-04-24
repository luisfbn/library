using Library.Services;
using Library.Services.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Library.WebAPI.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LoanController(ILoanService loanService) : ControllerBase
    {
        private readonly ILoanService _loanService = loanService;

        [HttpPost("register")]
        public ActionResult<ServiceResult<bool>> RegisterLoan(RegisterLoanDto loanDto)
        {
            var result = _loanService.RegisterLoan(loanDto.BookId, loanDto.ReaderId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("return/{id}")]
        public ActionResult<ServiceResult<bool>> ReturnBook(int id)
        {
            var result = _loanService.ReturnBook(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

    }


}
