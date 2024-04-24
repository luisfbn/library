using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IBookService bookService) : ControllerBase
    {
        private readonly IBookService _bookService = bookService;


        [HttpPost]
        public IActionResult AddBook(BookDto bookDto)
        {
            var result = _bookService.AddBook(bookDto);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var result = _bookService.DeleteBook(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var result = _bookService.GetAllBooks();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.ErrorMessage);
        }

    }
}
