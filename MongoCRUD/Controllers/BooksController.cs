using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoCRUD.Models;
using MongoCRUD.Services;

namespace MongoCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        public async Task<List<Books>> Get()
        {
            return await _booksService.GetAllAsync();
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> Get(string id)
        {
            var book = await _booksService.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost]
        public async Task<IActionResult> Post(Books books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _booksService.CreateAsync(books);

            var bookList = await _booksService.GetAllAsync();
            return Ok(bookList);
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Books newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var book = await _booksService.GetByIdAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            newBook.Id = book.Id;

            await _booksService.UpdateAsync(id, book);

            var bookList = await _booksService.GetAllAsync();
            return Ok(bookList);
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _booksService.GetByIdAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _booksService.DeleteAsync(id);

            var bookList = await _booksService.GetAllAsync();
            return Ok(bookList);
        }
    }
}
